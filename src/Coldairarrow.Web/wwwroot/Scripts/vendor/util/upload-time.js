/**
 *上传页面停留时间
 *callBack为回调函数，返回参数为停留的毫秒数
 * @param {function(number)} callBack
 */
function uploadTime(callBack) {
    if (typeof (jQuery) == 'undefined')
        throw '缺少jQuery引入';

    var _startTime = new Date().getTime();
    var _callBack = callBack || function () { };

    function initObj() {
        $(function () {
            document.addEventListener('visibilitychange', function () {
                var visibilityState = document.visibilityState;
                if (visibilityState == 'hidden' || visibilityState == 'unloaded') {
                    callBackTime();
                }
                else if (visibilityState == 'visible') {
                    _startTime = new Date().getTime();
                }
            });

            //在关闭页面时弹出确认提示窗口
            $(window).bind('beforeunload', function () {
                callBackTime();
            });
        });
    }
    function callBackTime() {
        var time = new Date().getTime() - _startTime;
        _callBack(time);
    }

    initObj();
}