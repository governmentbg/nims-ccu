var defaultColors = ['#005eae', '#fff000', '#ed1c24', '#f7a35c', '#8085e9', '#f15c80', '#e4d354', '#8d4653', '#91e8e1'];

// COLUMN CHART
$(function () {
    $.ajaxSetup({ cache: false });

    var chartProps = {
        tooltip: 'tooltip',
        dataurl: 'dataurl',
        chart: 'chart',
        title: 'title',
        ytitle: 'ytitle',
        isStacked: 'isstacked',
        hasStackLabels: 'hasstacklabels'
    };
    var tooltipProps = {
        header: 'header',
        point: 'point',
        footer: 'footer'
    };

    var render = function (div) {

        $.when($.getJSON(div.data(chartProps.dataurl))).done(function (dataResp) {

            var data = dataResp;

            setupChart(div, // html element to draw onto
                data, // drill map additional data
                extractTooltip(div)); // tooltip html-s
        })
        .fail(function (err) {
            console.log('fail');
        });
    };

    var extractTooltip = function (div) {

        var tooltipDiv = $($('div[data-' + chartProps.tooltip + '=true]', div)[0]);
        return {
            header: $('div[data-' + tooltipProps.header + '=true]', tooltipDiv)[0].innerHTML,
            //point: $('div[data-' + tooltipProps.point + '=true]', tooltipDiv)[0].innerHTML,
            //footer: $('div[data-' + tooltipProps.footer + '=true]', tooltipDiv)[0].innerHTML,
        };
    };

    var setupChart = function (div, data, tooltip) {
        Highcharts.setOptions({ lang: { numericSymbols: null } });

        div.highcharts({
            chart: { type: 'column' },
            title: { text: div.data(chartProps.title) },
            colors: defaultColors,
            xAxis: {
                categories: data.categories
            },
            yAxis: {
                min: 0,
                title: {
                    text: div.data(chartProps.ytitle)
                },
                stackLabels: {
                    enabled: div.data(chartProps.hasStackLabels) === "True" ? true : false,
                    style: {
                        fontWeight: 'bold',
                        color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                    }
                }
            },
            tooltip: {
                style: {
                    padding: 10,
                    fontFamily: 'Roboto',
                    fontSize: '14px',
                    fontWeight: '400'
                },
                headerFormat: tooltip.header,
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    stacking: (div.data(chartProps.isStacked) == "True") ? 'normal' : null
                }
            },
            credits: {
                enabled: false
            },
            exporting: {
                buttons: {
                    contextButton: {
                        menuItems: [{
                            text: window._resources.export,
                            onclick: function () {
                                this.exportChart();
                            },
                            separator: false
                        }]
                    }
                }
            },
            //actual information in chart
            series: data.data
        });
    };

    $.each($('div[data-' + chartProps.chart + '=true]'), function (i, div) {
        render($(div));
    });

});

// BASIC PIE
$(function () {
    $.ajaxSetup({ cache: false });

    var pieProps = {
        tooltip: 'tooltip',
        dataurl: 'dataurl',
        pie: 'pie',
        title: 'title',
        percentageLabelEnabled: 'percentageLabelEnabled'
    };
    var tooltipProps = {
        header: 'header',
        point: 'point',
        footer: 'footer'
    };

    var render = function (div) {

        $.when($.getJSON(div.data(pieProps.dataurl))).done(function (dataResp) {

            var data = dataResp;

            setupPie(div, // html element to draw onto
                data, // drill map additional data
                extractTooltip(div)); // tooltip html-s
        })
        .fail(function (err) {
            console.log('fail');
        });
    };

    var extractTooltip = function (div) {

        var tooltipDiv = $($('div[data-' + pieProps.tooltip + '=true]', div)[0]);
        return {
            header: $('div[data-' + tooltipProps.header + '=true]', tooltipDiv)[0].innerHTML,
            point: $('div[data-' + tooltipProps.point + '=true]', tooltipDiv).length > 0 ? $('div[data-' + tooltipProps.point + '=true]', tooltipDiv)[0].innerHTML : '<span style="color:{point.color}">\u25CF</span> {series.name}: <b>{point.y}</b><br/>',
            //footer: $('div[data-' + tooltipProps.footer + '=true]', tooltipDiv)[0].innerHTML,
        };
    };

    var setupPie = function (div, data, tooltip) {
        div.highcharts({
            chart: {
                type: 'pie'
            },
            title: { text: div.data(pieProps.title) },
            colors: defaultColors,
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        formatter: function () {
                            var text = this.point.name;

                            if (this.point.name.length > 50) {
                                text = text.substring(0, 47) + '...';
                            }

                            text += '<br/>' + this.point.value;

                            if (div.data(pieProps.percentageLabelEnabled)) {
                                text += '<br/>' + Highcharts.numberFormat(this.point.percentage, 2) + ' %';
                            }

                            return text;
                        },
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }
                    },
                    showInLegend: true,
                    size: '80%'
                }
            },

            //tooltip: {
            //    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
            //},

            tooltip: {
                style: {
                    padding: 10,
                    fontFamily: 'Roboto',
                    fontSize: '14px',
                    fontWeight: '400'
                },
                headerFormat: tooltip.header,
                shared: true,
                useHTML: true,
                pointFormat: tooltip.point,
            },
            legend: {
                margin: 25
            },
            credits: {
                enabled: false
            },
            exporting: {
                buttons: {
                    contextButton: {
                        menuItems: [{
                            text: window._resources.export,
                            onclick: function () {
                                this.exportChart();
                            },
                            separator: false
                        }]
                    }
                }
            },
            //actual information in pie
            series: [data]
        });
    };

    $.each($('div[data-' + pieProps.pie + '=true]'), function (i, div) {
        render($(div));
    });

});