using Castle.DynamicProxy;
using Coldairarrow.Util;

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
            Logger.Info(_logType, $"修改{_dataName}:{obj.GetPropertyValue(_nameField)?.ToString()}");
        }

        public override void OnActionExecuting(IInvocation invocation)
        {

        }
    }
}
