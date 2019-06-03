using Castle.DynamicProxy;
using Coldairarrow.Util;
using static Coldairarrow.Entity.Base_SysManage.EnumType;

namespace Coldairarrow.Business
{
    class DataEditLogAttribute : WriteDataLogAttribute
    {
        public DataEditLogAttribute(LogType logType, string dataName, string nameField)
            : base(logType, dataName, nameField)
        {
        }

        public override void OnActionExecuted(IInvocation invocation)
        {
            var obj = invocation.Arguments[0];
            BusHelper.WriteSysLog($"修改{_dataName}:{obj.GetPropertyValue(_nameField)?.ToString()}", _logType);
        }

        public override void OnActionExecuting(IInvocation invocation)
        {

        }
    }
}
