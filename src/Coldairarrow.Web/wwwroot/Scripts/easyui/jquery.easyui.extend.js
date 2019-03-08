(function () {
    //扩展easyui中tabs的部分方法，实现根据唯一标识id的进行相应操作；技巧：使用一个自执行的函数，激发作用域，避免这里定义的变量与系统全局变量冲突  
    var _methods = $.fn.tabs.methods;
    var _getTab = _methods.getTab;
    $.extend($.fn.tabs.methods, {
        //重写getTab方法，增加根据id获取tab（注意：这里我们可以定义任意的获取方式，不必一定使用id）  
        getTab: function (jq, which) {
            if (!which) return null;
            var tabs = jq.data('tabs').tabs;
            for (var i = 0; i < tabs.length; i++) {
                var tab = tabs[i];
                if (tab.panel("options").id == which) {
                    return tab;
                }
            }
            return _getTab(jq, which);//如果根据id无法获取，则通过easyui默认的getTab进行获取  
        },
        //重写exists方法，增加id判断
        exists: function (jq, which) {
            return this.getTab(jq, which) != null;//调用重写后的getTab方法  
        }
    });

    // extend the 'equals' rule
    $.extend($.fn.validatebox.defaults.rules, {
        equals: {
            validator: function (value, param) {
                return value == $(param[0]).val();
            },
            message: '两次输入不匹配'
        },
        mobile: {
            validator: function (value) {
                return /^(13|14|15|17|18)\d{9}$/i.test(value);
            },
            message: '手机号码格式不正确'
        }
    });
})();

/**
 * easyui datagrid formatter
 */
var Formatter = {
    date: function (value) {
        if (!value || !value.length) {
            return "";
        } else {
            return value.toDate().format("yyyy-MM-dd");
        }
    },
    month: function (value) {
        if (!value || !value.length) {
            return "";
        } else {
            return value.toDate().format("yyyy年M月");
        }
    }
};