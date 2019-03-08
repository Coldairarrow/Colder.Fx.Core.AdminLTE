//搜索表格
function searchGrid(searchBtnObj, gridSelector) {
    var $wrapper = $(searchBtnObj).closest("div.search_wrapper");
    if (!$wrapper || !$wrapper.length) {
        return;
    }

    var params = $wrapper.getValues();
    $(gridSelector).datagrid("load", params);
}

//搜索树表格
function searchTreeGrid(searchBtnObj, gridSelector) {
    var $wrapper = $(searchBtnObj).closest("div.search_wrapper");
    if (!$wrapper || !$wrapper.length) {
        return;
    }

    var params = $wrapper.getValues();
    $(gridSelector).treegrid("load", params);
}

//加载动画
function loading(isLoading) {
    var loading = true;
    if (typeof (isLoading) != 'undefined')
        loading = isLoading;
    if (loading) {
        $('<div id="loadingMask" class="datagrid-mask"></div>')
            .css({
                display: "block",
                'z-index': 998,
                width: "100%",
                height: '100%'
            })
            .appendTo("body");
        $("<div id=\"loadingMaskMsg\" class=\"datagrid-mask-msg\"></div>")
            .html("加载中，请稍候。。。")
            .css({
                display: "block",
                'z-index': 999,
                left: ($(document.body).outerWidth(true) - 190) / 2,
                top: ($(window).height() - 45) / 2
            })
            .appendTo("body");
    } else {
        $("#loadingMask").remove();
        $("#loadingMaskMsg").remove();
    }
}

//处理请求返回数据
function accessResJson(resJson) {
    if (resJson.Success) {
        dialogMsg('操作成功！');
    } else {
        dialogError('操作失败！详情:{0}'.format(resJson.Msg));
    }
}

//显示大图
function showBigImg(url) {
    top.dialogOpen({
        id: 'ShowBigImg',
        title: '添加数据',
        width: 600,
        height: 600,
        url: rootUrl + 'Base_SysManage/Common/ShowBigImg?url={0}'.format(url),
    });
}

//获取图片构造的Html
function getImgHtml(imgs) {
    var html = '';
    (imgs || '').split(',').forEach(function (item, index) {
        if (item == '')
            return;
        var br = '';
        if (index != 0)
            br = '<br />';
        html += '{0}<a href="javascript:;" onclick="showBigImg(\'{1}\')"><img src="{1}" style="width:100px;height:100px" /></a>'.format(br, item);
    });

    return html;
}