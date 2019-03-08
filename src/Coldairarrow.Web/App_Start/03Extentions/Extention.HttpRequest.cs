namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 拓展类
    /// </summary>
    public static partial class Extention
    {
        public static bool IsAjaxRequest(this HttpRequest req)
        {
            bool result = false;

            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }
    }
}
