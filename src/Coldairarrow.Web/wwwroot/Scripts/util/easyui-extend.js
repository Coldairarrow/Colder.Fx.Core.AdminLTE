//打开弹框,传入参数对象
function dialogOpen(options) {
    var _options = {
        id: 'dialogDefault',
        title: '弹框',
        width: 400,
        height: 200,
        content: null,
        url: '',
        closed: false,
        cache: false,
        modal: true,
    };
    $.extend(_options, options);

    var height = _options.height.toString();
    var width = _options.width.toString();
    if (!height.contains('%')) {
        if (height > $(window).height()) {
            _options.height = '90%';
        }
    }
    if (!width.contains('%')) {
        if (width > $(window).width()) {
            _options.width = '90%';
        }
    }

    var id = _options.id;
    var src = _options.url;
    var $html = $('#{0}'.format(id));
    if ($html.length == 0)
        $html = $('<div id="{0}" style="padding:5px;display:none;"></div>'.format(id, src)).appendTo("body");

    var content = '<iframe frameborder="0" style="height:99%;width:100%" src="{0}"></iframe>'.format(src);
    $html.dialog({
        title: _options.title,
        width: _options.width,
        height: _options.height,
        content: content,
        closed: false,
        cache: false,
        modal: true
    });
}

//关闭弹框,dialogId为弹框参数id
function dialogClose(dialogId) {
    $('#' + dialogId).dialog('close');
}

//右下角显示消息
function dialogMsg(msg) {
    $.messager.show({
        title: "操作提示",
        msg: msg,
        showType: 'slide',
        timeout: 3000
    });
}

//弹出警告消息框
function dialogError(msg) {
    $.messager.alert("操作提示", msg, "error");
}

//弹出确认框
function dialogComfirm(msg, succcess, cancel) {
    var _succcess = succcess || function () { };
    var _cancel = cancel || function () { };
    $.messager.confirm("操作提示", msg, function (data) {
        if (data) {
            _succcess();
        }
        else {
            _cancel();
        }
    });
}

//初始化日期控件为年月
function init_yearMonth(id, value) {
    var db = $('#' + id);
    db.datebox({
        value: value,
        width: 100,
        editable: true,
        prompt: '选择年月',
        validType: [],
        //readonly: false,
        onShowPanel: function () {//显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
            span.trigger('click'); //触发click事件弹出月份层
            if (!tds) setTimeout(function () {//延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                tds = p.find('div.calendar-menu-month-inner td');
                tds.click(function (e) {
                    e.stopPropagation(); //禁止冒泡执行easyui给月份绑定的事件
                    var year = /\d{4}/.exec(span.html())[0]//得到年份
                        , month = parseInt($(this).attr('abbr'), 10); //月份，这里不需要+1
                    db.datebox('hidePanel')//隐藏日期对象
                        .datebox('setValue', year + '-' + month); //设置日期的值
                });
            }, 0);
            yearIpt.unbind();//解绑年份输入框中任何事件
            $(yearIpt).attr('readonly', true);//年份只读
            $(yearIpt).css('border-color', 'white');//边框去掉
        },
        parser: function (s) {
            if (!s) return new Date();
            var arr = s.split('-');
            return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
        },
        formatter: function (d) { return d.getFullYear() + '-' + (d.getMonth() + 1);/*getMonth返回的是0开始的，忘记了。。已修正*/ }
    });
    var p = db.datebox('panel'), //日期选择对象
        tds = false, //日期选择对象中月份
        aToday = p.find('a.datebox-current'),
        yearIpt = p.find('input.calendar-menu-year'),//年份输入框
        //显示月份层的触发控件
        span = aToday.length ? p.find('div.calendar-title span') ://1.3.x版本
            p.find('span.calendar-text'); //1.4.x版本
    if (aToday.length) {//1.3.x版本，取消Today按钮的click事件，重新绑定新事件设置日期框为今天，防止弹出日期选择面板
        aToday.unbind('click').click(function () {
            var now = new Date();
            db.datebox('hidePanel').datebox('setValue', now.getFullYear() + '-' + (now.getMonth() + 1));
        });
    }
}