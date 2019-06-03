using Coldairarrow.Util;
using System;
using static Coldairarrow.Entity.Base_SysManage.EnumType;

namespace Coldairarrow.Business
{
    public abstract class WriteDataLogAttribute : BaseFilterAttribute
    {
        public WriteDataLogAttribute(LogType logType, string dataName, string nameField)
        {
            _logType = logType;
            _dataName = dataName;
            _nameField = nameField;
        }
        protected LogType _logType { get; }
        protected string _dataName { get; }
        protected string _nameField { get; }
        protected Type _entityType { get; }
        protected IBusHelper BusHelper { get; } = AutofacHelper.GetService<IBusHelper>();
    }
}
