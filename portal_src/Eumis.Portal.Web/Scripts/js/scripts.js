
var onDocumentReady = function () {

    // symbols count
    $('.symbols-count').keyup(function () {
        var nameAttr = $(this).attr('name');
        changeCount(this, 'span[symbols-target="' + nameAttr + '"]');
    });
    $('.symbols-count').keyup();

    $('.work-in-progress').click(function (e) {
        e.preventDefault();
        alert($("#res_wip").val());
    });

    $('textarea:not([auto-grow])').css('overflow', 'hidden').autogrow();

    $(".wait").click(function (e) {
        if (!$(this).is("a")) {
            showWait();
        } else if (e.which != 2 && !(e.shiftKey || e.altKey || e.metaKey || e.ctrlKey)) {
            showWait();
            $(this).attr('disabled', 'disabled');
        }
    });

    $(document).on('keydown', function (e) {
        if (e.keyCode === 27) {
            e.preventDefault(); //prevent browser from stopping animation
        }
    });

    //scroll to top:
    $(".go-to-top").click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1400, 'easeOutQuint');
    });

    $.extend($.fn.select2.defaults, $.fn.select2.locales[window._eumis_options.currentCulture]);
    //select2 with search field
    if ($('.select2').length != 0) {
        $(".select2").select2({
            // dropdownAutoWidth: true,
            allowClear: true,
            placeholder: ' '
        });
    }
    //select2 without search field
    if ($('.select2-no-search').length != 0) {
        $(".select2-no-search").select2({
            minimumResultsForSearch: -1,
            // dropdownAutoWidth: true,
            allowClear: true,
            placeholder: ' '
        });
    }
    //init popover on info icons
    if ($('.info-icon').length != 0) {
        $('.info-icon:not([info-icon])')
            .popover({
                container: 'body',
                placement: function (tip, element) {
                    if ($(element).attr('sc-placement') !== undefined) {
                        return $(element).attr('sc-placement');
                    } else {
                        var offset = $(element).offset();
                        var height = $(document).outerHeight();
                        var width = $(document).outerWidth();
                        var vert = 0.5 * height - offset.top;
                        var vertPlacement = vert > 0 ? 'top' : 'bottom';
                        var horiz = 0.5 * width - offset.left;
                        var horizPlacement = horiz > 0 ? 'right' : 'left';
                        var placement = Math.abs(horiz) > Math.abs(vert) ? horizPlacement : vertPlacement;
                        return placement;
                    }
                }
            });
    }
    
    if ($('.info-icon-static').length != 0) {
        $('.info-icon-static:not([info-icon])')
            .popover({
                container: 'body'
            });
    }
    
    if ($('.info-button').length != 0) {
        $('.info-button')
            .popover({
                container: 'body',
                trigger: 'hover',
                placement: function (tip, element) {
                    if ($(element).attr('sc-placement') !== undefined) {
                        return $(element).attr('sc-placement');
                    } else {
                        var offset = $(element).offset();
                        var height = $(document).outerHeight();
                        var width = $(document).outerWidth();
                        var vert = 0.5 * height - offset.top;
                        var vertPlacement = vert > 0 ? 'top' : 'bottom';
                        var horiz = 0.5 * width - offset.left;
                        var horizPlacement = horiz > 0 ? 'right' : 'left';
                        var placement = Math.abs(horiz) > Math.abs(vert) ? horizPlacement : vertPlacement;
                        return placement;
                    }
                },
                content: function() {
                    return $(this).attr('popover-message');
                },
                template: '<div class="popover" role="tooltip"><div class="arrow"></div><div class="popover-content"></div></div>'
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

    //fix save-as-draft button on scroll
    if ($('.save-as-draft-wrapper').length != 0) {
        $(window).scroll(function () {
            if ($(window).scrollTop() < ($('.form-submit').offset().top - $(window).height())) {
                $(".save-as-draft-wrapper").addClass('fixed');
            }
            else {
                $(".save-as-draft-wrapper").removeClass('fixed');
            }
        });
    }

    //datepicker
    if ($('input.datepicker').length != 0) {
        //initialize datepicker
        $('input.datepicker').datepicker({
            format: "dd.mm.yyyyг.",
            startView: 0,
            weekStart: 1,
            language: "bg",
            autoclose: true
        });
    }

    //preview form - show/hide validation errors
    $(".validation-show-errors, .validation-hide-errors").click(function () {
        var par = $(this).parent().parent().parent();
        par.find("ul").slideToggle(function () {
            if (par.find("ul").is(":visible")) {
                par.find(".validation-show-errors").hide();
                par.find(".validation-hide-errors").show();
            }
            else {
                par.find(".validation-show-errors").show();
                par.find(".validation-hide-errors").hide();
            }
        });

    });

    $(".active-validation-error").click(function () {
        var memberName = $(this).data("memberName");
        var field = $("[name='" + memberName + "']");
        var fieldIsHidden = false;

        if (field.is("[type='hidden']")) {
            fieldIsHidden = true;

            if (field.prevAll("div.select2-container").get(0)) {
                field = field.prevAll("div.select2-container");
            }
        }

        field.closest("section[data-section]").each(function (i, e) {
            var sectionNumber = $(e).data("section");
            $(".form-with-sections h2.section-heading[data-section='" + sectionNumber + "']").each(function () {
                if (!$(this).hasClass("opened")) {
                    $(this).click();
                }
            });
        });

        if (fieldIsHidden && field.get(0)) {
            field.get(0).scrollIntoView();
        }

        field.focus();
    });

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

    //tree functions
    if ($('.easy-tree').length != 0) {
        function init() {
            $('.easy-tree').EasyTree({});
        }
        window.onload = init();

        $("button.tree-collapse").click(function () {
            $("button.tree-collapse").hide();
            $("button.tree-expand").show();
            $('.easy-tree').find('li:has(>ul:has(>li[style!="display: none;"]))').find(' > span').click();
        });
        $("button.tree-expand").click(function () {
            $("button.tree-expand").hide();
            $("button.tree-collapse").show();
            $('.easy-tree').find('li:has(>ul:has(>li[style="display: none;"]))').find(' > span').click();
        });
    }

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
};

//trigger keyUp/keyDown events for textareas
var triggerTextareasEvents = function (parent) {
    setTimeout(function () {
        var keyDownEvent = jQuery.Event("keydown");
        var keyUpEvent = jQuery.Event("keyup");
        parent.find("textarea").trigger(keyDownEvent);
        parent.find("textarea").trigger(keyUpEvent);

        var isIE10 = false;
        /*@cc_on
            if (/^10/.test(@_jscript_version)) {
                isIE10 = true;
            }
        @*/
        if (isIE10) {
            parent.find("textarea").autogrow();
        }
    }, 50);
}

//sections slide Up/Down
var triggerSectionHideShow = function () {
    $(".form-with-sections h2.section-heading, .form-with-sections h3.section-heading, .form-with-sections h4.section-heading").click(function () {
        section = $(this).data('section');
        var elementSection = $("section[data-section=" + section + "]");
        var hasActivation = $(this).attr('activation-name') != undefined && $(this).attr('broadcast-name') != undefined;
        if ($(this).hasClass("opened")) {
            elementSection.hide();
            if (hasActivation) {
                activateSection($(this).attr('activation-name'), $(this).attr('broadcast-name'), { update: false, isActive: false });
            }
        } else {
            elementSection.show();
            if (hasActivation) {
                var that = this;
                setTimeout(function () {
                    activateSection($(that).attr('activation-name'), $(that).attr('broadcast-name'), { update: false, isActive: true });
                }, 10);
            }
            triggerTextareasEvents(elementSection);
            fnPopover(elementSection);
        }
        $(this).toggleClass("opened");
    })
    .children('.bootstrap-switch').click(function (e) {
        return false;
    });
}

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

//history sub-table show/hide execution
var triggerHistoryButtonClick = function (element) {
    current = element.parents('tr').nextAll("tr.history-table").first();
    if (element.hasClass("opened")) {
        current.hide();
        current.find('div.history-table-wrapper').hide();
    }
    else {
        current.show();
        current.first().show();
        var wrapper = current.find("div.history-table-wrapper");
        wrapper.show();

        triggerTextareasEvents(wrapper);

        fnPopover(wrapper);
    }
    element.toggleClass("opened");
};

//history sub-table show/hide
var triggerHistoryButton = function () {
    $(".history-btn:not([history-btn])").off('click').on("click", function (e) {
        e.preventDefault();
        triggerHistoryButtonClick($(this));
    });
};

var waitRequested = false;
var showWait = function () {
    waitRequested = true;
    $("div.loader span").html('<img src="/Content/img/loader.gif" />');
    $("div.loader #loading").hide();
    $("div.loader #please-wait").show();
    setTimeout(function () {
        if (waitRequested) {
            $("div.loader").fadeIn();
        }
    }, 1000);
}

var hideWait = function () {
    waitRequested = false;
    $("div.loader span").empty();
    $("div.loader #loading").show();
    $("div.loader #please-wait").hide();
    $("div.loader").hide();
}

var fnPopover = function (parent) {
    parent.find(" .input-validation-error").popover({
        container: 'body',
        content: function () {
            if (typeof validationSummaryErrors !== "undefined") {

                var _self = this;

                // if child elements has .input-validation-error
                if (this.tagName === "DIV") {

                    // select2 controls
                    _self = $(this).parent().find("input.validation-error-key, select.validation-error-key").first();

                    // uploader
                    if (typeof _self === "undefined") {

                    }
                }
                else if (this.tagName === "SELECT" && $(this).attr("ng-model")) {

                    // hidden element
                    _self = $(this).parent().find(".input-validation-error[name]").first();
                }
                else if (this.tagName === "TD") {
                    _self = $(this).find("input.validation-error-key, select.validation-error-key").first();
                }

                return validationSummaryErrors[$(_self).attr("name")];
            }
        },
        placement: "top",
        trigger: "hover",
        'template': '<div class="popover popover-error" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
    });
};

var isSplashShown = function (type) {
    var route = window['_eumis_options']['session'];
    var d = jQuery.Deferred();
    $.ajax({
        type: 'GET',
        url: '/api/' + route + '/splash/isShown?type=' + type,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (result) {
            d.resolve(result);
        }
    });

    return d.promise();
};

var markSplashAsShown = function (type, url) {
    var route = window['_eumis_options']['session'];
    $.ajax({
        type: 'POST',
        url: '/api/' + route + '/splash/markAsShown?type=' + type,
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: '{ }',
        success: function (result) {
            if(url != undefined){
                window.location = url;
            }
            return result;
        }
    });
};

var hideShowSplashModal = function (type, modalId, buttonClass) {
    isSplashShown(type)
        .then(function (result) {
            if (!result) {
                if ($('#' + modalId)) {
                    $('#' + modalId).modal({
                        backdrop: 'static',
                        keyboard: false
                    });

                    $('.' + buttonClass).click(function () {
                        markSplashAsShown(type, $('.' + buttonClass).attr('data-redirect'));
                    });
                }
            }
        });
};

var getResolvedInjectors = function () {
    var modules = [];
    $("[data-ng-controller],  [ng-controller]").each(function (i, container) {
        if ($(container).injector()) {
            modules.push($(container).injector());
        }
    });
    return modules;
};

$.when(0).then(function () {
    var d = jQuery.Deferred();
    $(document).ready(function () {
        d.resolve();
    });
    return d.promise();
}).then(function () {
    onDocumentReady();
}).then(function () {
    initClock();
}).then(function () {
    $(".clock").show();
}).then(function () {

    //now = Date.now();

    triggerSectionHideShow();
    triggerHistoryButton();
}).then(function () {
    var d = jQuery.Deferred();
    var moduleCount = $("[data-ng-controller],  [ng-controller]").length;
    var fnIntervalId = setInterval(function () {
        if (d.state() !== 'resolved' && moduleCount === getResolvedInjectors().length) {
            d.resolve(fnIntervalId);
        }
    }, 10);
    return d.promise();
}).then(function (fnId) {
    clearInterval(fnId);
    var d = jQuery.Deferred();
    var fnIntervalId = setInterval(function (root) {
        if (d.state() !== 'resolved' && (window['__eumis__queue__'] || 0) === getResolvedInjectors().length) {
            d.resolve(fnIntervalId);
        }
    }, 10);
    return d.promise();
}).then(function (fnId) {
    clearInterval(fnId);
    $("div.loader").hide();
    fnPopover($("body"));

    //console.log(Date.now());
    //console.log(Math.abs(now - Date.now()))
});
//var now = Date.now();

// change textarea span counter - equivalent to symbols-count directive
function changeCount(element, selector) {
    $(selector).text(getLength(element.value));
}

function getLength(input) {

    if (input == null || !angular.isDefined(input))
        return 0;

    var nSymbols = input.match(/(\n)/g) || [];
    var rnSymbols = input.match(/(\r\n)/g) || [];
    var addition = nSymbols.length - rnSymbols.length;

    return input.length + addition;
};

$('#hide_lag_procedures').change(function () {
    if ($(this).is(":checked")) {
        $('.lag-procedure').addClass("hidden-procedure");
    } else {
        $('.lag-procedure').removeClass("hidden-procedure");
    }
});

function hideShowTotalsTable() {
    var totalsTable = document.getElementById("totalsTable");
    var hideBtn = document.getElementById("hideTableBtn");
    var showBtn = document.getElementById("showTableBtn");

    if (totalsTable.style.display === "none") {
        totalsTable.style.display = "block";
        hideBtn.style.display = "block";
        showBtn.style.display = "none";
    } else {
        totalsTable.style.display = "none";
        hideBtn.style.display = "none";
        showBtn.style.display = "block";
    }
}
