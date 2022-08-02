(function () {
    //change search type:
    $("form span.search-type").click(function () {
        if ($(this).data('search') == 'advanced') {
            $("form.quick").removeClass("quick").addClass("advanced");
        }
        else {
            $("form.advanced").removeClass("advanced").addClass("quick");
        }
    });

    // set boxes equal height:
    function heightAlign(cssSelector) {
        if (!$(".hidden-lg").is(":visible") || cssSelector == '.home-stat-boxes a.stat') {
            var elementHeights = $('' + cssSelector).map(function () {
                $(this).height('auto');
                return $(this).height();
            }).get();
            var maxHeight = Math.max.apply(null, elementHeights);
            if (cssSelector != '.home-stat-boxes a.stat' && $(window).width() > 1006) {
                searchHeight = $(".link-box.search").parent().height() - 137;
                if (maxHeight < searchHeight) {
                    maxHeight = searchHeight;
                }
            }
            $('' + '' + cssSelector).height(maxHeight);
        }
        else {
            $('' + cssSelector).map(function () {
                $(this).height('auto');
            });
        }
    }

    function alignAll() {
        heightAlign(".home-link-boxes .link-box.auto-height");
        heightAlign(".home-stat-boxes a.stat");
    }

    $(window).resize(function () {
        alignAll();
    });

    alignAll();

    // combobox ajax filling
    // There is a chain of five comboboxes.
    // When one node of the chain changes, it reflects on all nodes after that.

    var selProgram = $($('select[name="program"]')[0]);
    var selPriority = $($('select[name="priority"]')[0]);
    var selInvestment = $($('select[name="investment"]')[0]);
    var selSpecific = $($('select[name="specific"]')[0]);
    var selProcedure = $($('select[name="procedure"]')[0]);

    function setOptions(select, options)
    {
        select.empty();
        select.append($('<option></option>')); // watermark
        _.forEach(options, function (opt) {
            var option = $('<option></option>').attr("value", opt.id).text(opt.text);
            select.append(option);
        });
        select.select2("val", ""); 

        select.trigger('change'); // move down the chain of comboboxes
    }

    selProgram.change(function () {
        if (selProgram.select2('data') != null) {
            $.when($.getJSON('/Home/SearchByOp/' + selProgram.select2('data').id)).done(function (resp) {
                setOptions(selPriority, resp);
            });
        }
        else {
            setOptions(selPriority, []);
        }
    });

    selPriority.change(function () {
        if (selPriority.select2('data') != null) {
            $.when($.getJSON('/Home/SearchByPriorityAxis/' + selPriority.select2('data').id)).done(function (resp) {
                setOptions(selInvestment, resp);
            });
        }
        else {
            setOptions(selInvestment, []);
        }
    });

    selInvestment.change(function () {
        if (selInvestment.select2('data') != null) {
            $.when($.getJSON('/Home/SearchByInvestmentPriority/' + selInvestment.select2('data').id)).done(function (resp) {
                setOptions(selSpecific, resp);
            });
        }
        else {
            setOptions(selSpecific, []);
        }
    });

    selSpecific.change(function () {
        if (selPriority.select2('data') != null) {
            var address = '/Home/GetProceduresByPriorityAxis/' + selPriority.select2('data').id;

            var investmentData = selInvestment.select2('data');
            var specificData = selSpecific.select2('data');
            if (investmentData != null) {
                address = address + '?inv=' + investmentData.id;
                if (specificData != null) {
                    address = address + '&spec=' + specificData.id;
                }
            }

            $.when($.getJSON(address)).done(function (resp) {
                setOptions(selProcedure, resp);
            });
        }
        else {
            setOptions(selProcedure, []);
        }
    });
})();