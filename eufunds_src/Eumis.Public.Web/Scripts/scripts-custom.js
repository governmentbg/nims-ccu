(function () {
    $(".table-wrapper div.table-heading").click(function () {
        section = $(this).data('section');
        var elementSection = $("tr[data-section=" + section + "]", $(this).closest('table'));

        if ($(this).hasClass("opened")) {
            elementSection.hide();
        } else {
            elementSection.show();
        }
        $(this).toggleClass("opened");
    });
    
    //scroll if has page-headings
    //if ($('.page-headings')[0] != null && $('.page-headings')[0] != undefined) {
    //    $('html, body').animate({ scrollTop: (290) }, 1400, 'easeOutQuint');
    //}
})();


var createSelect2 = function (selector, initFunc, searchFunc, parentSelector, usePlaceholderDefaultText, defaultValue, parent2Selector) {
    if (defaultValue === undefined) {
        defaultValue = null;
    }

    if (usePlaceholderDefaultText === undefined) {
        usePlaceholderDefaultText = true;
    }

    $(selector).select2({
        placeholder: usePlaceholderDefaultText === true ? window._resources.placeholder : null,
        // allowClear: true,
        // containerCssClass: "select2-allowclear",
        initSelection: function (element, callback) {
            var id = $(element).val();

            if (id === "" && defaultValue !== null) {
                id = defaultValue;
            }

            if (id !== "") {
                return $.ajax({
                    url: initFunc,
                    type: "POST",
                    dataType: "json",
                    data: {
                        id: id
                    }
                }).done(function (data) {
                    var results;
                    results = [];
                    results.push({
                        id: data.id,
                        text: data.text
                    });
                    callback(results[0]);
                });
            }
        },
        ajax: {
            url: searchFunc,
            dataType: 'json',
            type: "POST",
            quietMillis: 250,
            data: function (term) {
                return {
                    term: term,
                    parentId: $(parentSelector).val(),
                    parent2Id: $(parent2Selector).val()
                };
            },
            results: function (data) {
                if (usePlaceholderDefaultText) {
                    data.unshift({ id: "", text: window._resources.placeholder });
                }

                return {
                    results: $.map(data, function (item) {
                        return {
                            id: item.id,
                            text: item.text
                        };
                    })
                };
            }
        }
    });

    $(parentSelector).change(function () {
        $(selector).select2('data', null).change();
    });

    $(parent2Selector).change(function () {
        $(selector).select2('data', null).change();
    });
};
