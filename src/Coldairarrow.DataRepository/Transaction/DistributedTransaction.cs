using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 数据库分布式事务,跨库事务
    /// </summary>
    public class DistributedTransaction
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="one">第一个数据仓储</param>
        /// <param name="two">第二个数据仓储</param>
        /// <param name="others">其它数据仓储</param>
        public DistributedTransaction(IRepository one, IRepository two, params IRepository[] others)
        {
            if (one == null || two == null)
                throw new Exception("参数不能为null!");

            _repositorys = others.Concat(new IRepository[] { one, two }).Distinct().ToList();
        }

        #endregion

        #region 内部成员

        private Dictionary<IRepository, bool?> _successDic { get; } = new Dictionary<IRepository, bool?>();
        private List<IRepository> _repositorys { get; }
        private void SetProperty(object obj, string propertyName, object value)
        {
            obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(obj, value);
        }
        private object GetProperty(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {
            _repositorys.ForEach(aRepository =>
            {
                _successDic.Add(aRepository, null);
                (aRepository as DbRepository).BeginTransaction();
                SetProperty(aRepository, "_openedTransaction", true);
            });
        }

        /// <summary>
        /// 结束事务
        /// </summary>
        /// <returns>是否成功完成</returns>
        public bool EndTransaction()
        {
            bool isOK = true;
            foreach (var aRepository in _repositorys)
            {
                try
                {
                    aRepository.GetDbContext().SaveChanges();
                    Action _sqlTransaction = GetProperty(aRepository, "_sqlTransaction") as Action;
                    _sqlTransaction?.Invoke();
                    _successDic[aRepository] = true;
                }
                catch
                {
                    _successDic[aRepository] = false;
                    isOK = false;
                    break;
                }
            }

            _repositorys.ForEach(aRepository =>
            {
                var transaction = GetProperty(aRepository, "Transaction") as IDbContextTransaction;
                bool? success = _successDic[aRepository];
                if (isOK)
                    transaction.Commit();
                else
                {
                    if (success != null)
                        transaction.Rollback();
                }

                //释放初始化
                aRepository.GetType().GetMethod("Dispose", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(aRepository, null);
            });

            return isOK;
        }

        #endregion
    }
}
