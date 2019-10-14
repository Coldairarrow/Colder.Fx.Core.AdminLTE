using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Util
{
    /// <summary>
    /// Random随机数帮助类
    /// </summary>
    public static class RandomHelper
    {
        private static readonly Random _random  = new Random();

        /// <summary>
        /// 下一个随机数
        /// </summary>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        /// <summary>
        /// 下一个随机值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="source">值的集合</param>
        /// <returns></returns>
        public static T Next<T>(IEnumerable<T> source)
        {
            return source.ToList()[Next(0, source.Count())];
        }

        /// <summary>
        /// 获取不重复的随机值集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public static List<T> GetValuesNoRepeat<T>(IEnumerable<T> source, int count)
        {
            if (source.Count() < count)
                throw new Exception("选择数量不能大于集合数量!");

            var _source = new List<T>(source).Distinct().ToList();
            List<T> resList = new List<T>();
            for (int i = 0; i < count; i++)
            {
                resList.Add(GetNewValue());
            }

            return resList;

            T GetNewValue()
            {
                while (true)
                {
                    var theValue = Next(_source);
                    if (!resList.Any(x => x.ToJson() == theValue.ToJson()))
                    {
                        _source.Remove(theValue);

                        return theValue;
                    }
                }
            }
        }
    }
}