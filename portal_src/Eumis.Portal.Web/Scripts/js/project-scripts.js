
var PARTIAL_SAVE_SECTIONS = function (callback, errorCallbackOrForm) {
    var errorCallback,
        disabledButtons = [];

    if (typeof errorCallbackOrForm === 'function') {
        errorCallback = errorCallbackOrForm;
    } else if ($(errorCallbackOrForm).is('form')) {
        $(errorCallbackOrForm).find('input[type=submit]').each(function () {
            if ($(this).is(':enabled')) {
                $(this).attr('disabled', 'disabled');
                disabledButtons.push(this);
            }
        });
    }

    setTimeout(function () {
        var deferreds = triggerActivationBroadcast();
        $.when.apply($, deferreds).then(function () {
            callback();
        }, function () {
            if (errorCallback) {
                errorCallback();
            } else {
                var saveAsDraftButton = $("a.save-as-draft");
                if (saveAsDraftButton) {
                    hideWait();
                    saveAsDraftButton.hide();
                    partialSaveHandleError();

                    setTimeout(function () {
                        $(".response-message").hide();
                        saveAsDraftButton.show();
                    }, 5050);
                }

                $.each(disabledButtons, function () {
                    $(this).removeAttr('disabled');
                });
            }
        });
    }, 0);
};

var triggerActivationBroadcast = function () {
    var deferreds = [];
    $(".form-with-sections h2.section-heading, .form-with-sections h3.section-heading, .form-with-sections h4.section-heading").each(function () {
        section = $(this).data('section');
        var elementSection = $("section[data-section=" + section + "]");
        var hasActivation = $(this).attr('activation-name') != undefined && $(this).attr('broadcast-name') != undefined;

        if (hasActivation) {
            var d = jQuery.Deferred();
            deferreds.push(d.promise());
            activateSection($(this).attr('activation-name'), $(this).attr('broadcast-name'), { update: true, isActive: false, d: d});
        }
    });
    return deferreds;
};

function activateSection(activationName, broadcastName, args) {
    var broadcastName = ((broadcastName == undefined || broadcastName == null || broadcastName == '')
                            ? 'activationBroadcast'
                            : broadcastName);
    $("." + activationName).each(function (index, container) {
        triggerBroadcast(container, broadcastName, args);
    });
};
