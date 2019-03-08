namespace Coldairarrow.Util
{
    /// <summary>
    /// 日志操作类型
    /// </summary>
    public enum LoggerType
    {
        /// <summary>
        /// 使用关系型数据库记录日志,例如SQlServer、MySQL、Oracle等
        /// </summary>
        RDBMS,

        /// <summary>
        /// 使用ElasticSearch记录日志
        /// </summary>
        ElasticSearch
    }
}
