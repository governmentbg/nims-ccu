
(function () {

    $('.work-in-progress').click(function (e) {
        e.preventDefault();
        alert($("#res_wip").val());
    });

    $('textarea:not([auto-grow])').css('overflow', 'hidden').autogrow();

    $(document).on('keydown', function (e) {
        if (e.keyCode === 27) {
            e.preventDefault(); //prevent browser from stopping animation
        }
    });

    //scroll to top:
    $(".go-to-top").click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1400, 'easeOutQuint');
    });

    //select2 with search field

    $.extend($.fn.select2.defaults, $.fn.select2.locales[window._resources.lang]);
    if ($('.select2').length != 0) {
        $(".select2").select2({
            // placeholder: window._resources.placeholder,
            // allowClear: true,
        });
    }
    //select2 without search field
    if ($('.select2-no-search').length != 0) {
        $(".select2-no-search").select2({
            minimumResultsForSearch: -1,
            // placeholder: window._resources.placeholder,
            // allowClear: true,
        });
    }
    //init popover on info icons
    if ($('.info-icon').length != 0) {
        $('.info-icon:not([info-icon])')
            .popover({
                container: 'body',
                placement: function (tip, element) {
                    var offset = $(element).offset();
                    height = $(document).outerHeight();
                    width = $(document).outerWidth();
                    vert = 0.5 * height - offset.top;
                    vertPlacement = vert > 0 ? 'top' : 'bottom';
                    horiz = 0.5 * width - offset.left;
                    horizPlacement = horiz > 0 ? 'right' : 'left';
                    placement = Math.abs(horiz) > Math.abs(vert) ? horizPlacement : vertPlacement;
                    return placement;
                }
            });
    }

    //bootstrap switch
    $('input[class="bootstrap-switch"]').bootstrapSwitch();
    $(".bootstrap-switch").click(function (e) {
        e.stopPropagation();
    });

    $("input.bootstrap-switch.lock-unlock").bootstrapSwitch({
        onText: $("#res_locked").val(),
        offText: $("#res_unlocked").val(),
        onColor: "danger",
        offColor: "success",
        size: 'small'
    });

    //bootstrap confirmation
    $('[data-toggle=confirmation]').not('.work-in-progress').confirmation({
        container: 'body',
        btnOkLabel: $("#res_yes").val(),
        btnCancelLabel: $("#res_no").val(),
        title: $("#res_confirm").val(),
        popout: true
    });

    //bootstrap confirmation
    $('[data-toggle=confirmation-no-title]').not('.work-in-progress').confirmation({
        container: 'body',
        btnOkLabel: $("#res_yes").val(),
        btnCancelLabel: $("#res_no").val(),
        popout: true
    });
    //data-btn-ok-label="Да" data-btn-ok-icon="glyphicon glyphicon-ok" data-btn-cancel-label="Не" data-btn-cancel-icon="glyphicon glyphicon-remove"

    //datepicker
    if ($('input.datepicker').length != 0) {
        //initialize datepicker
        $('input.datepicker').datepicker({
            format: "dd.mm.yyyy",
            startView: 0,
            weekStart: 1,
            language: window._resources.lang,
            autoclose: true
        });
    }

    //help page expandables
    $(".expandable > span").click(function () {
        if ($(this).next("div").is(":visible")) {
            $(this).removeClass('opened');
        }
        else {
            $(this).addClass('opened');
        }
        $(this).next("div").slideToggle();
    });

    //toggle expand of sections
    $("button.form-collapse").click(function () {
        $("div.loader").show();

        setTimeout(function () {
            $('.form-with-sections h2.section-heading').each(function () {
                if ($(this).hasClass("opened")) {
                    $(this).click();
                }
            });
            $("button.form-collapse").hide();
            $("button.form-expand").show();

            $("div.loader").delay(500).hide();
        }, 0);
    });

    $("button.form-expand").click(function () {
        $("div.loader").show();
        setTimeout(function () {
            $('.form-with-sections h2.section-heading').each(function () {
                if (!$(this).hasClass("opened")) {
                    $(this).click();
                }
            });
            $("button.form-expand").hide();
            $("button.form-collapse").show();

            $("div.loader").delay(500).hide();
        }, 0);
    });

    $(".expand-all-section").click(function () {
        setTimeout(function () {
            $("button.form-expand").click();
        }, 300);
    });

    $(".wait").click(function (e) {
        if (!$(this).is("a")
            || (e.which != 2 && !(e.shiftKey || e.altKey || e.metaKey || e.ctrlKey))) {
            showWait();
        }
    });

    $(".responsive-calendar").responsiveCalendar(
        {
            translateMonths: window._resources.monthsArray
        });

})();

var submitForm = function (selector) {
    $(selector).submit();
    setInterval(function () {
        if (navigator.appName == 'Microsoft Internet Explorer') {
            var ua = navigator.userAgent;
            var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
            if (re.exec(ua) != null) {
                var ieVersion = parseFloat(RegExp.$1);
                if (ieVersion < 11) {
                    $(selector).submit();
                }
            }
        }
    }, 2000);
}

var showWait = function () {
    $("div.loader span").html('<img src="/Content/img/loader.gif" />');
    $("div.loader #please-wait").show();
    $("div.loader").delay(1000).fadeIn();
}