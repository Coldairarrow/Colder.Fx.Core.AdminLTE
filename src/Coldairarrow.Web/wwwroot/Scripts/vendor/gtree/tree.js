/**
 *
 * Created by Administrator on 2017-01-10.
 */
(function ($) {

    function Solver(allItems, opts) {
        this.allItems = allItems;
        this.opts = opts;
    }

    Solver.prototype = {
        getId: function (node) {
            return node[this.opts.id_field];
        },
        getPid: function (node) {
            return node[this.opts.pid_field];
        },
        getSeq: function (node) {
            return node[this.opts.seq_field];
        },
        /**
         *按照seq排序
         * @param arr
         * @returns {*|Array.<T>}
         */
        sortBySeq: function (arr) {
            var _this = this;
            return arr.sort(
                function (a, b) {
                    if (_this.getSeq(a) < _this.getSeq(b)) return -1;
                    if (_this.getSeq(a) > _this.getSeq(b)) return 1;
                    return 0;
                }
            );
        },
        /**
         * 找到所有子节点
         * @param pid
         * @returns {Array}
         */
        findChildItems: function (pid) {
            var nodes = [];

            var allItems = this.allItems;

            //倒序寻找
            for (var i = allItems.length - 1; i >= 0; i--) {
                if (this.getPid(allItems[i]) == pid) {
                    nodes.push(allItems[i]);

                    //所有数组移除该元素
                    allItems.splice(i, 1);
                }
            }

            return nodes;
        },
        /**
         * 递归创建结构
         * @param rootNode
         */
        handle: function (rootNode) {
            var _this = this;

            var childNodes = [];

            var childItems = this.findChildItems(id);
            $.each(childItems, function (index, eachChild) {

                //新建节点

                //递归
                _this.handle(eachChild);

                childNodes.push(eachChild);
            });


            //有子节点的就设置子节点
            if (childNodes.length >= 1) {
                //排序
                //按照SID排序
                var arr = this.sortBySeq(childNodes);

                rootNode[this.opts.child_field] = arr;
            }

        }
    };

    $.GTree = function (settings) {

        //使用jQuery.extend 覆盖插件默认参数
        var opts = $.extend({}, $.GTree.defaults, settings);
        if (!data instanceof Array) {
            alert("GTree require an array,the param is not an array");
        }

        //解决器
        var solver = new Solver();

        //根节点
        var rootNode = {};
        rootNode[opts.id_field] = opts.root_id_field;


        solver.handle(rootNode);
        return rootNode[opts.child_field];
    };

    $.GTree.defaults = {
        id_field: "id",
        root_id_field: "0",
        pid_field: "pid",
        seq_field: "seq",
        child_field: "child",
        seq: false
    };

})(jQuery);