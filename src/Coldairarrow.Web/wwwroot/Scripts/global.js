/**
 * 扩展String
 */
(function ($) {
    /**
     * 扩展String
     */
    $.extend(String, {
        /**
         * 格式化含有{0|1...}格式标签的字符串，类似c#的format
         * 如：String.format('a{0}b',x) = axb
         * @param str
         * @return {*}
         */
        format: function (str) {
            if (!str) return null;
            var args = Array.prototype.slice.call(arguments, 1);
            return str.replace(/\{(\d+)\}/g, function (m, i) {
                return args[i];
            });
        }
    });

    /**
     * 扩展String.prototype
     */
    $.extend(String.prototype, {
        /**
         * HTML转义
         * @returns {string}
         */
        encodeHtml: function () {
            var s = this;
            if (s.length == 0) return "";
            s = s.replace(/&/g, "&gt;");
            s = s.replace(/</g, "&lt;");
            s = s.replace(/>/g, "&gt;");
            s = s.replace(/ /g, "&nbsp;");
            s = s.replace(/\'/g, "&#39;");
            s = s.replace(/\"/g, "&quot;");
            s = s.replace(/\r\n/g, "<br />");
            s = s.replace(/\n/g, "<br />");
            return s;
        },

        /**
         * HTML反转义
         * @returns {string}
         */
        decodeHtml: function () {
            var s = this;
            if (s.length == 0) return "";
            s = s.replace(/&gt;/g, "&");
            s = s.replace(/&lt;/g, "<");
            s = s.replace(/&gt;/g, ">");
            s = s.replace(/&nbsp;/g, " ");
            s = s.replace(/&#39;/g, "\'");
            s = s.replace(/&quot;/g, "\"");
            s = s.replace(/<br>/g, "\n");
            s = s.replace(/<br \/>/g, "\n");
            return s;
        },

        /**
         * 去除字符串两边的空格
         * @return {String}
         */
        trim: function () {
            return this.replace(/(^\s*)|(\s*$)/g, "");
        },

        /**
         * 格式化含有{0|1...}格式标签的字符串，类似c#的format
         * 如：'a{0}b' .format(x) = axb
         * @return {String}
         */
        format: function () {
            var args = arguments;
            return this.replace(/\{(\d+)\}/g, function (m, i) {
                return args[i];
            });
        },

        /**
         * 格式化浮点数数字,返回保留n个小数的数字结果
         * @param n             //小数点精度
         * @return {String}
         */
        toFixed: function (n) {
            return parseFloat(this).toFixed(n);
        },

        /**
         * 以0做前缀转换成固定长度的字符串
         * @param num
         * @return {String}
         */
        padZero: function (num) {
            if (this.length < num) {
                var tmp = [];
                for (var i = 0; i < num - this.length; i++) {
                    tmp.push(0);
                }
                return tmp.join('') + this;
            } else {
                return this;
            }
        },

        /**
         * 检测字符串是否为日期格式
         * 日期格式为yyyy-M-d或yyyy-MM-dd或yyyy/M/d或yyyy/MM/dd
         * @return {Boolean}
         */
        isDate: function () {
            var r = this.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
            if (r == null) return false;
            var d = new Date(r[1], r[3] - 1, r[4]);
            return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
        },

        toDate: function () {
            var temp = this.toString();
            temp = temp.replace(/-/g, "/");
            return new Date(Date.parse(temp));
        },

        /**
         * 检测字符串是否为长日期格式
         * 日期部分格式为yyyy-M-d或yyyy-MM-dd或yyyy/M/d或yyyy/MM/dd
         * 时间部分格式为hh:mm:ss或h:m:s
         * @return {Boolean}
         */
        isDateTime: function () {
            var r = this.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/);
            if (r == null) return false;
            var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);
            return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]);
        },

        /**
         * 检测字符串是否为邮件格式
         * @return {Boolean}
         */
        isMail: function () {
            var emailReg = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
            return emailReg.test(this);
        },

        /**
         * 是否已制定字符开头
         * @param prefix
         * @param offset
         * @return {Boolean}
         */
        startsWith: function (prefix, offset) {
            offset = offset || 0;
            if (offset < 0 || offset > this.length) return false;
            return this.substring(offset, offset + prefix.length) == prefix;
        },

        /**
         * 是否已制定字符结束
         * @param suffix
         * @return {Boolean}
         */
        endsWith: function (suffix) {
            return this.substring(this.length - suffix.length) == suffix;
        },

        /**
         * 截取字符串
         * @param length
         * @param suffix
         * @return {String}
         */
        cut: function (length, suffix) {
            length = length || 30;
            suffix = suffix === undefined ? "..." : suffix;
            return this.length > length ?
                this.slice(0, length - suffix.length) + suffix : this;
        },

        /**
         * 移除html标签
         * @return {String}
         */
        stripTags: function () {
            return this.replace(/<\/?[^>]+>/gi, '');
        }
    });
})(jQuery);

/**
 * extand Date object
 */
(function ($) {
    $.extend(Date, {
        /**
         * An Array of day names starting with Sunday.
         *
         * @example dayNames[0]
         * @result 'Sunday'
         *
         * @name dayNames
         * @type Array
         * @cat Plugins/Methods/Date
         */
        dayNames: ['日', '一', '二', '三', '四', '五', '六'],
        /**
         * An Array of abbreviated day names starting with Sun.
         *
         * @example abbrDayNames[0]
         * @result 'Sun'
         *
         * @name abbrDayNames
         * @type Array
         * @cat Plugins/Methods/Date
         */
        abbrDayNames: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
        /**
         * An Array of month names starting with Janurary.
         *
         * @example monthNames[0]
         * @result 'January'
         *
         * @name monthNames
         * @type Array
         * @cat Plugins/Methods/Date
         */
        monthNames: ['一月', '二月', '三月', '四月', '五月', '五月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        /**
         * An Array of abbreviated month names starting with Jan.
         *
         * @example abbrMonthNames[0]
         * @result 'Jan'
         *
         * @name monthNames
         * @type Array
         * @cat Plugins/Methods/Date
         */
        abbrMonthNames: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        /**
         * The first day of the week for this locale.
         *
         * @name firstDayOfWeek
         * @type Number
         * @cat Plugins/Methods/Date
         * @author Kelvin Luck
         */
        firstDayOfWeek: 1,
        /**
         * The format that string dates should be represented as (e.g. 'dd/mm/yyyy' for UK, 'mm/dd/yyyy' for US, 'yyyy-mm-dd' for Unicode etc).
         *
         * @name format
         * @type String
         * @cat Plugins/Methods/Date
         * @author Kelvin Luck
         */
        //format: 'dd/mm/yyyy';
        //format: 'mm/dd/yyyy';
        format: 'yyyy-mm-dd',
        //format: 'dd mmm yy';

        /**
         * Returns a new date object created from the passed String according to Date.format or false if the attempt to do this results in an invalid date object
         * (We can't simple use Date.parse as it's not aware of locale and I chose not to overwrite it incase it's functionality is being relied on elsewhere)
         *
         * @example var dtm = Date.fromString("12/01/2008");
         * dtm.toString();
         * @result 'Sat Jan 12 2008 00:00:00' // (where Date.format == 'dd/mm/yyyy'
         *
         * @name fromString
         * @type Date
         * @cat Plugins/Methods/Date
         * @author Kelvin Luck
         */
        fromString: function (str) {
            var f = Date.format;

            var d = new Date('01/01/1970');

            if (str == "") return d;

            str = str.toLowerCase();
            var matcher = '';
            var order = [];
            var r = /(dd?d?|mm?m?|yy?yy?)+([^(m|d|y)])?/g;
            var results;
            while ((results = r.exec(f)) != null) {
                switch (results[1]) {
                    case 'd':
                    case 'dd':
                    case 'm':
                    case 'mm':
                    case 'yy':
                    case 'yyyy':
                        matcher += '(\\d+\\d?\\d?\\d?)+';
                        order.push(results[1].substr(0, 1));
                        break;
                    case 'mmm':
                        matcher += '([a-z]{3})';
                        order.push('M');
                        break;
                }
                if (results[2]) {
                    matcher += results[2];
                }

            }
            var dm = new RegExp(matcher);
            var result = str.match(dm);
            for (var i = 0; i < order.length; i++) {
                var res = result[i + 1];
                switch (order[i]) {
                    case 'd':
                        d.setDate(res);
                        break;
                    case 'm':
                        d.setMonth(Number(res) - 1);
                        break;
                    case 'M':
                        for (var j = 0; j < Date.abbrMonthNames.length; j++) {
                            if (Date.abbrMonthNames[j].toLowerCase() == res) break;
                        }
                        d.setMonth(j);
                        break;
                    case 'y':
                        d.setYear(res);
                        break;
                }
            }

            return d;
        }
    });
})(jQuery);

/**
 * extand Date prototype
 */
(function ($) {
    $.extend(Date.prototype, {
        /**
         * Checks if the year is a leap year.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.isLeapYear();
         * @result true
         *
         * @name isLeapYear
         * @type Boolean
         * @cat Plugins/Methods/Date
         */
        isLeapYear: function () {
            var y = this.getFullYear();
            return (y % 4 == 0 && y % 100 != 0) || y % 400 == 0;
        },

        /**
         * Checks if the day is a weekend day (Sat or Sun).
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.isWeekend();
         * @result false
         *
         * @name isWeekend
         * @type Boolean
         * @cat Plugins/Methods/Date
         */
        isWeekend: function () {
            return this.getDay() == 0 || this.getDay() == 6;
        },

        /**
         * Check if the day is a day of the week (Mon-Fri)
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.isWeekDay();
         * @result false
         *
         * @name isWeekDay
         * @type Boolean
         * @cat Plugins/Methods/Date
         */
        isWeekDay: function () {
            return !this.isWeekend();
        },

        /**
         * Gets the number of days in the month.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getDaysInMonth();
         * @result 31
         *
         * @name getDaysInMonth
         * @type Number
         * @cat Plugins/Methods/Date
         */
        getDaysInMonth: function () {
            return [31, (this.isLeapYear() ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][this.getMonth()];
        },

        /**
         * Gets the name of the day.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getDayName();
         * @result 'Saturday'
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getDayName(true);
         * @result 'Sat'
         *
         * @param abbreviated Boolean When set to true the name will be abbreviated.
         * @name getDayName
         * @type String
         * @cat Plugins/Methods/Date
         */
        getDayName: function (abbreviated) {
            return abbreviated ? Date.abbrDayNames[this.getDay()] : Date.dayNames[this.getDay()];
        },

        /**
         * Gets the name of the month.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getMonthName();
         * @result 'Janurary'
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getMonthName(true);
         * @result 'Jan'
         *
         * @param abbreviated Boolean When set to true the name will be abbreviated.
         * @name getDayName
         * @type String
         * @cat Plugins/Methods/Date
         */
        getMonthName: function (abbreviated) {
            return abbreviated ? Date.abbrMonthNames[this.getMonth()] : Date.monthNames[this.getMonth()];
        },

        /**
         * Get the number of the day of the year.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getDayOfYear();
         * @result 11
         *
         * @name getDayOfYear
         * @type Number
         * @cat Plugins/Methods/Date
         */
        getDayOfYear: function () {
            var tempDate = new Date("1/1/" + this.getFullYear());
            return Math.floor((this.getTime() - tempDate.getTime()) / 86400000);
        },

        /**
         * Get the number of the week of the year.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.getWeekOfYear();
         * @result 2
         *
         * @name getWeekOfYear
         * @type Number
         * @cat Plugins/Methods/Date
         */
        getWeekOfYear: function () {
            return Math.ceil(this.getDayOfYear() / 7);
        },

        /**
         * Set the day of the year.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.setDayOfYear(1);
         * dtm.toString();
         * @result 'Tue Jan 01 2008 00:00:00'
         *
         * @name setDayOfYear
         * @type Date
         * @cat Plugins/Methods/Date
         */
        setDayOfYear: function (day) {
            this.setMonth(0);
            this.setDate(day);
            return this;
        },

        /**
         * Add a number of years to the date object.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.addYears(1);
         * dtm.toString();
         * @result 'Mon Jan 12 2009 00:00:00'
         *
         * @name addYears
         * @type Date
         * @cat Plugins/Methods/Date
         */
        addYears: function (num) {
            this.setFullYear(this.getFullYear() + num);
            return this;
        },

        /**
         * Add a number of months to the date object.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.addMonths(1);
         * dtm.toString();
         * @result 'Tue Feb 12 2008 00:00:00'
         *
         * @name addMonths
         * @type Date
         * @cat Plugins/Methods/Date
         */
        addMonths: function (num) {
            var tempDate = this.getDate();

            this.setMonth(this.getMonth() + num);

            if (tempDate > this.getDate())
                this.addDays(-this.getDate());

            return this;
        },

        /**
         * Add a number of days to the date object.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.addDays(1);
         * dtm.toString();
         * @result 'Sun Jan 13 2008 00:00:00'
         *
         * @name addDays
         * @type Date
         * @cat Plugins/Methods/Date
         */
        addDays: function (num) {
            //this.setDate(this.getDate() + num);
            this.setTime(this.getTime() + (num * 86400000));
            return this;
        },

        /**
         * Add a number of hours to the date object.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.addHours(24);
         * dtm.toString();
         * @result 'Sun Jan 13 2008 00:00:00'
         *
         * @name addHours
         * @type Date
         * @cat Plugins/Methods/Date
         */
        addHours: function (num) {
            this.setHours(this.getHours() + num);
            return this;
        },

        /**
         * Add a number of minutes to the date object.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.addMinutes(60);
         * dtm.toString();
         * @result 'Sat Jan 12 2008 01:00:00'
         *
         * @name addMinutes
         * @type Date
         * @cat Plugins/Methods/Date
         */
        addMinutes: function (num) {
            this.setMinutes(this.getMinutes() + num);
            return this;
        },

        /**
         * Add a number of seconds to the date object.
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.addSeconds(60);
         * dtm.toString();
         * @result 'Sat Jan 12 2008 00:01:00'
         *
         * @name addSeconds
         * @type Date
         * @cat Plugins/Methods/Date
         */
        addSeconds: function (num) {
            this.setSeconds(this.getSeconds() + num);
            return this;
        },

        /**
         * Sets the time component of this Date to zero for cleaner, easier comparison of dates where time is not relevant.
         *
         * @example var dtm = new Date();
         * dtm.zeroTime();
         * dtm.toString();
         * @result 'Sat Jan 12 2008 00:01:00'
         *
         * @name zeroTime
         * @type Date
         * @cat Plugins/Methods/Date
         * @author Kelvin Luck
         */
        zeroTime: function () {
            this.setMilliseconds(0);
            this.setSeconds(0);
            this.setMinutes(0);
            this.setHours(0);
            return this;
        },

        /**
         * Returns a string representation of the date object according to Date.format.
         * (Date.toString may be used in other places so I purposefully didn't overwrite it)
         *
         * @example var dtm = new Date("01/12/2008");
         * dtm.asString();
         * @result '12/01/2008' // (where Date.format == 'dd/mm/yyyy'
         *
         * @name asString
         * @type Date
         * @cat Plugins/Methods/Date
         * @author Kelvin Luck
         */
        asString: function (format) {
            var r = format || Date.format;
            if (r.split('mm').length > 1) { // ugly workaround to make sure we don't replace the m's in e.g. noveMber
                r = r.split('mmmm').join(this.getMonthName(false))
                    .split('mmm').join(this.getMonthName(true))
                    .split('mm').join(fx.zeroPad(this.getMonth() + 1))
            } else {
                r = r.split('m').join(this.getMonth() + 1);
            }
            r = r.split('yyyy').join(this.getFullYear())
                .split('yy').join((this.getFullYear() + '').substring(2))
                .split('dd').join(fx.zeroPad(this.getDate()))
                .split('d').join(this.getDate());
            return r;
        },

        /**
         * 对Date的扩展，将 Date 转化为指定格式的String
         * 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符，
         * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)
         * 例子：
         * (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423
         * (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18
         */
        format: function (fmt) {
            var o = {
                "M+": this.getMonth() + 1, //月份
                "d+": this.getDate(), //日
                "h+": this.getHours(), //小时
                "m+": this.getMinutes(), //分
                "s+": this.getSeconds(), //秒
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度
                "S": this.getMilliseconds() //毫秒
            };
            if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o) {
                if (new RegExp("(" + k + ")").test(fmt))
                    fmt = fmt.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : (("00" + o[k]).substr(("" + o[k]).length)));
            }
            return fmt;
        }
    });
})(jQuery);

/**
* json parser
*/
(function ($) {
    /*
    * 取表单域的所有值
    */
    $.fn.getValues = function (options) {
        var defaults = {
            checkbox: 'array',
            not: []
        };
        var $container = $(this);

        options = $.extend({}, defaults, options);

        function getValues() {
            var values = {}, ckbFields = [];
            $container.find(":input[name]").each(function () {
                var that = $(this),
                    type = that.attr("type").toLowerCase(),
                    name = that.attr("name");
                if (type == 'button' || type == 'submit') return true;
                if (!name.length) return true;
                if ($.inArray(name, options.not) >= 0) return true;

                if (type == "checkbox") {
                    if (values[name] != undefined) {
                        return true;
                    }
                    ckbFields.push(name);
                    values[name] = getCheckboxValues(name);
                } else if (type == "radio") {
                    if (values[name] != undefined) {
                        return true;
                    }
                    values[name] = getRadioValue(name);
                } else {
                    var value = that.val();
                    if (!value.length) return true;

                    if (values[name] == undefined) {
                        values[name] = value;
                    } else {
                        var arr = [];
                        arr.push(values[name]);
                        arr.push(value);

                        values[name] = arr.join(',');
                    }
                }
            });

            if (options.checkbox == 'string') {
                $.each(ckbFields, function (i, v) {
                    if ($.isArray(values[v])) values[v] = values[v].join(',');
                });
            }

            return values;
        }

        function getRadioValue(name) {
            return $container.find("input:radio[name='" + name + "']").val();
        }

        function getCheckboxValues(name) {
            return $.map($container.find("input:checkbox[name='" + name + "']:checked"), function (n) {
                return n.value;
            });
        }

        return getValues();
    };
})(jQuery);

/*
 * Desktop
 */
var Desktop = function () {
    var currentSystemMenu, innerMsgContainer, innerMsgPanel, innerMsgNum;
    var innerMsgTemplate = [
        '<li>',
            '<a href="javascript:;" class="topbar-msg-item" data-id="{{:Id}}" data-title="{{:MsgTypeText}}">',
                '<div class="topbar-msg-title">{{:MsgTitle}}</div>',
                '<div class="topbar-msg-time">{{:MsgTime}}</div>',
            '</a>',
        '</li>'
    ].join('');

    function init() {
        $(window).on('load hashchange', function () {
            var hash = location.hash.indexOf('#') === 0 ? location.hash.substring(1) : '';
            if (!hash || !hash.length) {
                return;
            }

            var arr = hash.split('|');
            var panelId = parseInt(Math.random() * 10000, 10) + 1;
            Desktop.tabs.add('_panel_r' + panelId, arr.length > 1 ? arr[1] : '', arr[0]);
        });

        initMenu();
        initInnerMsg();
    }

    function initMenu() {
        $("#menu").on("click", "a.menu_item", function () {
            if (currentSystemMenu) {
                currentSystemMenu.removeClass("menu_item_active");
            }
            currentSystemMenu = $(this);
            currentSystemMenu.addClass("menu_item_active");
        });
    }

    function initInnerMsg() {
        var wrapper = $('#topbar_innermsg');
        innerMsgPanel = wrapper.find('div.topbar-msg-panel');
        innerMsgContainer = wrapper.find('ul.topbar-msg');
        innerMsgNum = wrapper.find('span.topbar-btn-msg-num');
        btn = wrapper.find('a.topbar-btn');

        btn.on('mouseover', function () {
            btn.addClass('topbar-btn-active');
        }).on('mouseleave', function () {
            btn.removeClass('topbar-btn-active');
        });

        innerMsgPanel.on('mouseover', function () {
            btn.addClass('topbar-btn-active');
        }).on('mouseleave', function () {
            btn.removeClass('topbar-btn-active');
        });

        innerMsgContainer.on('click', 'a.topbar-msg-item', function () {
            var self = $(this),
                id = self.data('id');
            Desktop.tabs.add('_panel_innermsg_' + id, self.data('title'), '/innermsg/details/' + id);
        });

        //loadInnerMsg();
    }

    function loadInnerMsg() {
        $.ajax({
            url: '/innermsg/getlist',
            cache: false,
            data: { isread: false, pageindex: 1, pagesize: 5 },
            dataType: 'json',
            success: function (res) {
                if (res.total > 0) {
                    innerMsgNum.text(res.total).css('display', 'inline-block');
                    var tmpl = $.templates(innerMsgTemplate); 
                    var html = tmpl.render(res.rows);
                    innerMsgContainer.html(html);
                } else {
                    innerMsgNum.text('0').css('display', 'none');
                    innerMsgContainer.html('<li><span class="topbar-msg-item topbar-msg-none">暂无未读消息</span></li>');
                }

                setTimeout(loadInnerMsg, 30 * 1000);
            }
        });
    }

    return init(), {};
}();

/*
 * extend Desktop
 */
$.extend(Desktop, {
    tabs: function () {
        var $tabs;

        function init() {
            $tabs = $("#global_tabs").tabs({
                fit: true,
                border: false,
                tools: [{
                    iconCls: 'icon-reload',
                    handler: function () {
                        var tab = $tabs.tabs("getSelected");
                        //if (tab) {
                        //    tab.panel('refresh');
                        //}
                        tab.find('iframe')[0].contentWindow.location.reload(true);
                    }
                }]
            });
        }

        function add(id, title, href, options) {
            var $tab = $tabs.tabs("getTab", id);
            if ($tab) {
                var tabOpt = $tab.panel("options");
                var index = $tabs.tabs("getTabIndex", $tab);
                $tabs.tabs("select", index);
                //if (href != tabOpt.href) {
                //    $tab.panel('refresh', href);
                //}
            } else {
                var newTab = '<iframe frameborder="0" style="height:99%;width:100%" src="{0}"></iframe>'.format(href);
                options = $.extend({
                    id: id,
                    title: title,
                    //href: href,
                    content: newTab,
                    closable: true
                }, options);
                $tabs.tabs("add", options);
            }
        }

        return init(), {
            "add": add
        };
    }(),
    dialog: function () {
        var defaults = {
            modal: true,
            cache: false,
            closed: true
        };
        var $dialog;

        function init() {
            $dialog = $('<div id="global_dialog" style="padding:5px;display:none;"></div>').appendTo("body");
        }

        function open(options, id) {
            options = $.extend({}, defaults, { closed: false, content: null, openerGrid: null }, options);
            if (!id || !id.length) {
                $dialog.dialog(options).dialog("center");
            } else {
                var dialogId = id;
                if (!dialogId.startsWith("#")) {
                    dialogId = "#" + id;
                }
                var $dlg = $(dialogId);
                if (!$dlg.length) {
                    $dlg = $('<div style="display:none;" />').attr("id", id).appendTo("body");
                }
                $dlg.dialog(options).dialog("center");
            }
        }

        function close(id) {
            if (!id) {
                $dialog.dialog("close");
            } else {
                if (!id.startsWith("#")) {
                    id = "#" + id;
                }
                $(id).dialog("close");
            }
        }

        function refresh(url) {
            $dialog.dialog('refresh', url);
        }

        function options() {
            return $dialog.dialog("options");
        }

        function reloadOpenerGrid() {
            var options = $dialog.dialog("options");
            if (options.openerGrid) {
                if (!options.openerGrid.startsWith("#")) {
                    options.openerGrid = "#" + options.openerGrid;
                }
                $(options.openerGrid).datagrid("reload");
            }
        }

        return init(), {
            "open": open,
            "close": close,
            "refresh": refresh,
            "options": options,
            "reloadOpenerGrid": reloadOpenerGrid
        };
    }(),
    searchGrid: function (gridId) {
        var $wrapper = $(this).closest("div.search_wrapper");
        if (!$wrapper || !$wrapper.length) {
            return;
        }

        var params = $wrapper.getValues();
        $(gridId).datagrid("load", params);
    },
    export: function (searchContainer, formId, url) {
        var params = $(searchContainer).getValues();
        for (key in params) {
            if ($.isArray(params[key])) {
                params[key] = params[key].join(',');
            }
        }

        var $form = $('#' + formId);
        if (!$form.length) {
            $form = $('<form id="' + formId + '" method="post"></form>').appendTo('body');
        }
        $form.form('submit', {
            iframe: true,
            url: url,
            queryParams: params
        });
    }
});