//拓展Easyui日期控件验证规则：结束日期必须大于开始日期
(function () {
    $.extend($.fn.validatebox.defaults.rules, {
        startEnd: {
            validator: function (value, param) {
                var start = $(param[0]).datetimebox('getValue');  //获取开始时间    
                return value > start;                             //有效范围为当前时间大于开始时间    
            },
            message: '结束日期应大于开始日期!'                     //匹配失败消息  
        }
    });
})();