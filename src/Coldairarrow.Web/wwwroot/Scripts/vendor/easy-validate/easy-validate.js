/**
 * 简单表单验证插件
 * 作者:Coldairarrow
 * @param {any} selecter
 */
function easyValidate(selecter) {
    if (typeof ($) == 'undefined')
        throw '缺少jQuery插件';
    if (typeof (layer) == 'undefined')
        throw '缺少layer插件';

    var _selecter = selecter || document;
    var _rule = {
        //不为空
        requred: "^.+$",
        //邮箱
        email: "^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$"
    };

    var elementList = $(_selecter).find('[easy-validate]');
    var msg = '';
    var isValidate = true;
    elementList.each(function (index, element) {
        var config = JSON.parse($(element).attr('easy-validate'));
        var value = $(element).val();
        var rule = getRule(config.rule);
        var regex = new RegExp(rule);
        if (!regex.test(value)) {
            msg += config.errormsg + '<br />';
            isValidate = false;
        }
    });
    if (!isValidate)
        top.layer.msg(msg, { icon: 2 });
    return isValidate;

    function getRule(ruleName) {
        return _rule[ruleName] || ruleName;
    }
}
