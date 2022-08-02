/**
 * An easy tree view plugin for jQuery and Bootstrap
 * @Copyright yuez.me 2014
 * @Author yuez
 * @Version 0.1
 */
(function ($) {
    $.fn.EasyTree = function (options) {
        var defaults = {
            selectable: false,
            deletable: false,
            editable: false,
            addable: false//,
            //i18n: {
            //    collapseTip: 'Свиване',
            //    expandTip: 'Разпъване',
            //}
        };

        options = $.extend(defaults, options);

        this.each(function () {
            var easyTree = $(this);
            $.each($(easyTree).find('ul > li'), function () {
                var text;
                if ($(this).is('li:has(ul)')) {
                    var children = $(this).find(' > ul');
                    $(children).remove();
                    text = $(this).text();
                    $(this).html('<span><span class="glyphicon"></span><a href="javascript: void(0);"></a> </span>');
                    if ($(this).hasClass("active-node")) {
                        $(this).find(' > span > span').addClass('glyphicon-folder-open');
                    }
                    $(this).find(' > span > a').text(text);
                    $(this).append(children);
                }
                else {
                    text = $(this).text();
                    href = $(this).data("href");
                    if (href) {
                        $(this).html('<span><span class="glyphicon"></span><a href="' + href + '" style="font-weight: bold;"></a> </span>');
                        $(this).find(' > span > span').addClass('hidden');
                    }
                    else {
                        $(this).html('<span><span class="glyphicon"></span><a></a> </span>');
                    }
                    $(this).find(' > span > a').text(text);
                }
            });

            $(easyTree).find('li:has(ul)').addClass('parent_li').find(' > span')//.attr('title', options.i18n.collapseTip);

            // collapse or expand
            $(easyTree).delegate('li.parent_li.active-node > span', 'click', function (e) {
                var children = $(this).parent('li.parent_li').find(' > ul > li');
                if (children.is(':visible')) {
                    children.hide('fast');
                    $(this)//.attr('title', options.i18n.expandTip)
                        .find(' > span.glyphicon')
                        .addClass('glyphicon-folder-close')
                        .removeClass('glyphicon-folder-open');
                } else {
                    children.each(function () {
                        if (!$(this).hasClass("hidden-procedure")) {
                            $(this).show('fast');
                        }
                    });

                    $(this)//.attr('title', options.i18n.collapseTip)
                        .find(' > span.glyphicon')
                        .addClass('glyphicon-folder-open')
                        .removeClass('glyphicon-folder-close');
                }
                e.stopPropagation();
            });

            $(easyTree).delegate('li > span', 'click', function (e) {
                e.stopPropagation();
            });

        });
    };
})(jQuery);
