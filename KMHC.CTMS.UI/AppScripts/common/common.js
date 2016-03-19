$(document).ready(function () {

});


//根据页面内容生成右侧滚动条，并绑定相关事件
function InitFixedNaviBar() {
    var container = $('#fixed_navi_bar').html("");
    var itemsSelector = container.attr('itemsselector');
    var beginContainer = container.attr("begincontainer");
    var index = 0;
    var collaspeId = 1;
    var content = "";
    function createHtml(items, level) {
        var childrenPanel = items.children(".panel-default");
        if (childrenPanel.length == 0) {
            childrenPanel = items.find(".panel-group>.panel-default");
        }
        if (childrenPanel.length == 0) {
            childrenPanel = items.find(".panel-default");
        }

        var s = "";

        if (level == 0) {
            s = " menu-first ";
        }

        for (var j = 0; j < childrenPanel.length; j++) {
            var panelBody = $(childrenPanel[j]);
            var headPanel = panelBody.children(".panel-heading");
            headPanel.attr("navi_id", index);

            if ($(panelBody).find(".panel-default:visible").length > 0) {
                content += '<li class="nav nav-list"><div href="#collaspe' + collaspeId + '" class="nav-header' + s + ' " navi_id="' + (index++)
                    + '"><i class="icon-user-md icon-large"></i>' + headPanel.text() + '</div><ul id="collaspe' + collaspeId + '" class="nav nav-list menu-second collapse">';
                collaspeId++;
                createHtml(panelBody, level + 1);
                content += "</ul></li>";
            } else {
                content += '<li class="nav nav-list"  navi_id="' + (index++) + '"><div class="' + s + '"><i class="icon-list"></i>' + headPanel.text() + '</div></li>';
            }
        }
    }
    var items = $(itemsSelector + ':visible');
    createHtml($(beginContainer), 0);
    container.append(content);

    //页面滚动时,绑定对应的自动hover事件
    $(window).scroll(function () {
        var divs = $("div.panel-heading[navi_id]");
        var mark = document.body.scrollTop;
        var curShow = $(divs[0]).attr("navi_id");
        for (var i = 0; i < divs.length; i++) {
            var tempDiv = $(divs[i]);
            if (tempDiv.position().top - mark >= 0) {
                curShow = tempDiv.attr("navi_id");
                tempDiv.removeClass("in");
                break;
            }
        }
        $("#fixed_navi_bar").find("[navi_id]").each(function () {
            $(this).removeClass("hover");
        });

        $("#fixed_navi_bar").find(".collapse").removeClass("in");

        var cur = $("#fixed_navi_bar").find("[navi_id=" + curShow + "]");
        cur.addClass("hover");
        var parent = cur.parents("ul.collapse");
        if (parent.length > 0) {
            parent.addClass("in");
        } else {
            $(cur.attr("href")).addClass("in");
        }
    });

    //当对应导航按钮点击时,自动定位到相应的内容块
    container.find('[navi_id]').click(function () {
        var top = $(itemsSelector).filter('[navi_id="' + $(this).attr('navi_id') + '"]').position().top;
        document.documentElement.scrollTop = top;//IE
        document.body.scrollTop = top;//Chrome
        return false;
    });

}

function KMConfirm(options, fn) {
    this.msg = (options.msg == "" || options.msg == undefined) ? '是否删除当前记录?' : options.msg;//提示内容
    this.btnMsg = (options.btnMsg == "" || options.btnMsg == undefined) ? '删除' : options.btnMsg;//按钮文字

    this.init = function () {
        if ($('div#page-wrapper').find('div#KMconfirmModal').length === 0) {
            var Template = '<div class="modal fade" id="KMconfirmModal" tabindex="-1" role="dialog"> <div class="modal-dialog modal-sm"><div class="modal-content">';
            Template += '<div class="modal-header"><label><i class="fa fa-fw fa-warning"></i>操作提示</label></div> <div class="modal-body">' + this.msg + '</div><div class="modal-footer"><button type="button" data-dismiss="modal" class="btn btn-danger btn-sm" id="delete">' + this.btnMsg + '</button><button type="button" data-dismiss="modal" class="btn btn-info btn-sm">取消</button></div></div></div></div>';
            $('div#page-wrapper').append(Template);
        } else {
            $('div#KMconfirmModal').find('div.modal-body').html(this.msg);
            $('div#KMconfirmModal').find('div.modal-footer>button#delete').html(this.btnMsg);
        }

        $('#KMconfirmModal').modal({ backdrop: 'static', keyboard: false })
               .one('click', '#delete', function (e) {
                   fn(e);
               });
    };

    this.init();
};

function KMAlert(options, fn) {
    this.msg = (options.msg == "" || options.msg == undefined) ? '当前记录保存成功！' : options.msg;//提示内容
    this.btnMsg = (options.btnMsg == "" || options.btnMsg == undefined) ? '确定' : options.btnMsg;//按钮文字
    this.returnUrl = (options.returnUrl == "" || options.returnUrl == undefined) ? null : options.returnUrl;//跳转地址
    var _returnUrl = this.returnUrl;

    this.init = function () {
        if ($('div#page-wrapper').find('div#KMalertModal').length === 0) {
            var Template = '<div class="modal fade" id="KMalertModal" tabindex="-1" role="dialog"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header">';
            Template += '<label><i class="fa fa-fw fa-warning"></i>操作提示</label></div><div class="modal-body">' + this.msg + '</div><div class="modal-footer"><button type="button" data-dismiss="modal" class="btn btn-info btn-sm">' + this.btnMsg + '</button></div></div></div></div>';
            $('div#page-wrapper').append(Template);
        } else {
            $('div#KMalertModal').find('div.modal-body').html(this.msg);
            $('div#KMalertModal').find('div.modal-footer>button').html(this.btnMsg);
        }

        $('#KMalertModal').modal({ backdrop: 'static', keyboard: false });

        $('#KMalertModal').on('hidden.bs.modal', function (e) {
            if (_returnUrl != null)
            {
                location.href = _returnUrl;
            }
        })
    }

    this.init();
};