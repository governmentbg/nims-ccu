$(function () {
    $.ajaxSetup({ cache: false });

    var SETTINGS = {
        INTERACTION: {
            EXPLORER: 0,
            MAP: 1,
        }
    }

    var CurrentSettings = {
        Interaction: SETTINGS.INTERACTION.MAP
        //Interaction: SETTINGS.INTERACTION.EXPLORER
    }

    var mapProps = {
        type: 'type',
        tooltip: 'tooltip',
        baseUrl: 'baseurl',
        infoSet: 'infoset',
        mapId: 'mapid',
        rootId: 'rootid',
        map: 'map',
        redirectItemUrl: 'redirectitemurl'
    };
    var tooltipProps = {
        header: 'header',
        point: 'point',
        footer: 'footer'
    };
    var mapTypeEnum = {
        single: 'single',
        all: 'all'
    };
    var urls = {
        dataTypeParam: 'dataType',
        singlePart: 'Single/',
        allPart: 'All',
        dataPrefix: 'Data'
    };

    var treeEvents = {
        selectSilent: 'selectSilent',
        mouseDown: 'mousedown'
    }

    var redirectUrl = '';

    var render = function (div) {

        var isSingle = div.data(mapProps.type) === mapTypeEnum.single;
        var mapUrl = parseToUrl(div, isSingle, false);
        var dataUrl = parseToUrl(div, isSingle, true);
        redirectUrl = div.data(mapProps.redirectItemUrl);

        var map = BG_MAP;
        var data = BG_MAP_DATA;

        var rootId = div.data(mapProps.rootId);
        var showId = div.data(mapProps.mapId);

        var treetable = setupTree(div, // html element to draw onto
                        rootId, // map additional data
                        data); // drill map additional data

        setupChart(div, // html element to draw onto
            rootId, // map definition
            map, // drill map definitions
            data, // drill map additional data
            extractTooltip(div), // tooltip html-s
            treetable,
            showId);

        //$.when($.getJSON(mapUrl), $.getJSON(dataUrl)).done(function (mapResp, dataResp) {
        //    var map = mapResp[0];
        //    var data = dataResp[0];

        //    var rootId = div.data(mapProps.rootId);
        //    var showId = div.data(mapProps.mapId);

        //    var treetable = setupTree(div, // html element to draw onto
        //                    rootId, // map additional data
        //                    data); // drill map additional data

        //    setupChart(div, // html element to draw onto
        //        rootId, // map definition
        //        map, // drill map definitions
        //        data, // drill map additional data
        //        extractTooltip(div), // tooltip html-s
        //        treetable,
        //        showId);
        //})
        //.fail(function (err) {
        //    console.log('fail');
        //});
    };

    var extractTooltip = function (div) {

        var tooltipDiv = $($('div[data-' + mapProps.tooltip + '=true]', div)[0]);
        return {
            header: $('div[data-' + tooltipProps.header + '=true]', tooltipDiv)[0].innerHTML,
            point: $('div[data-' + tooltipProps.point + '=true]', tooltipDiv)[0].innerHTML,
            footer: $('div[data-' + tooltipProps.footer + '=true]', tooltipDiv)[0].innerHTML,
        };
    };

    var parseToUrl = function (div, isSingle, isData) {

        var baseUrl = div.data(mapProps.baseUrl);
        var prefix = '';
        var suffix = '';
        if (isData) {
            prefix = urls.dataPrefix;
            suffix = '?' + urls.dataTypeParam + '=' + div.data(mapProps.infoSet);
        }

        var id = div.data(mapProps.mapId);

        var url = baseUrl + prefix + urls.allPart + suffix;
        if (isSingle) {
            url = baseUrl + prefix + urls.singlePart + id + suffix;
        }

        return url;
    };

    var prepareMap = function (map, data) {

        if (map.id === undefined || map.id !== data.id) {
            throw { exceptionType: "highmapsException", exceptionMessage: "map.id doesn't match mapData.id" };
        }

        // define map from input
        var result = {
            type: "map",
            joinBy: "regionId",
            id: map.id,
            mapData: $.map(map.mapData, function (region, i) {
                return {
                    path: region.path,
                    regionId: region.regionId
                };
            }),
            data: data.regions
        };

        if (window._resources.lang == window._resources.bg) {
            result.name = data.name
        }
        else if (window._resources.lang == window._resources.en) {
            result.name = data.nameAlt
        }

        // set colors
        var colors = Highcharts.getOptions().colors;
        $.each(result.data, function (i, region) {
            region.color = colors[i % colors.length];
        });

        return result;
    };

    var drilldownAction = function (drillMapsData, chart, rootId, id) {

        var drillUpMap = _.find(drillMapsData, function (item) {
            return _.find(item.regions, { drilldown: id });
        });

        if (drillUpMap) {
            if (drillUpMap.id !== rootId) {
                drilldownAction(drillMapsData, chart, rootId, drillUpMap.id);
            }

            _.find(chart.series[0].data, { drilldown: id }).doDrilldown();
        }
    };

    var expandNodeAndParents = function (treetable, regionId) {
        treetable.treetable('collapseAll');
        var rowParentId = $(treetable.treetable('node', regionId).row).data('tt-parent-id');
        if (rowParentId) {
            expandNodeAndParents(treetable, rowParentId);
        }
        treetable.treetable('expandNode', regionId);
    };

    var setupChart = function (div, rootId, drillMaps, drillMapsData, tooltip, treetable, showId) {

        var drills = _.map(drillMaps, function (elem, ind) {
            return prepareMap(elem, _.find(drillMapsData, { id: elem.id }));
        });

        var map = _.find(drills, { id: rootId });

        var findRegionIdByDrillEvent = function (event) {

            var mapId = event.seriesOptions.id;

            var reg;
            if (!reg) {
                var map = _.find(drillMapsData, function (item) {
                    return _.find(item.regions, { drilldown: mapId });
                });
                if (map) {
                    reg = _.find(map.regions, { drilldown: mapId });
                }
            }
            return reg;
        };

        var triggerRegionTreetableEvents = function (reg) {
            if (reg) {
                expandNodeAndParents(treetable, reg.regionId);
                $(treetable.treetable('node', reg.regionId).row).trigger(treeEvents.selectSilent);
            }
        };

        var drilldownEvent = function (event) {
            var reg = findRegionIdByDrillEvent(event);
            triggerRegionTreetableEvents(reg);
        };

        var drillupEvent = function (event) {
            var reg = findRegionIdByDrillEvent(event);
            triggerRegionTreetableEvents(reg);
        };

        var clickEvent = function (event) {
            if (CurrentSettings.Interaction === SETTINGS.INTERACTION.EXPLORER ||
                CurrentSettings.Interaction === SETTINGS.INTERACTION.MAP) {
                var p = event.point;
                if (p.drilldown === 0) { // last level, so click acts as redirect
                    redirectItem(p.regionId);
                }
            }
        };

        Highcharts.setOptions({ lang: { drillUpText: window._resources.backTo + ' {series.name}' } });
        div.highcharts("Map", {
            chart: {
                type: "map",
                width: 700,
                height: 500,
                events: {
                    drilldown: drilldownEvent,
                    drillup: drillupEvent
                }
            },
            title: {
                text: '' //map.name
            },
            legend: {
                enabled: true,
                verticalAlign: "top",
                useHTML: true,
                labelFormat: "{name}",
                itemStyle: {
                    fontSize: '14px'
                }
            },
            tooltip: {
                headerFormat: tooltip.header, // '{series.name}<br />',
                pointFormat: tooltip.point, //'{point.name} - {point.value}',
                footerFormat: tooltip.footer //''
            },
            credits: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            mapNavigation: {
                //enable: true // zoom off
            },
            plotOptions: {
                map: {
                    dataLabels: {
                        enabled: true,
                        formatter: function () {
                            if (window._resources.lang == window._resources.bg) {
                                return this.point.name
                            }
                            else if (window._resources.lang == window._resources.en) {
                                return this.point.nameAlt
                            }
                        },
                        shadow: true,
                        style: { textShadow: false }
                    },
                    events: {
                        click: clickEvent,
                        legendItemClick: function () {
                            return false;
                        }
                    },
                    cursor: 'pointer'
                }
            },
            series: [map],
            drilldown: {
                series: drills,
                activeDataLabelStyle: {
                    color: '#FFFF00',
                    textDecoration: 'none',
                    textShadow: '0 0 3px #000000'
                },
                drillUpButton: {
                    relativeTo: 'spacingBox',
                    position: {
                        align: 'left',
                        x: 10,
                        y: 0
                    }
                }
            }
        }, function (chart) {
            drilldownAction(drillMapsData, chart, rootId, showId);
        });
    };

    var redirectItem = function (itemId) {
        if (redirectUrl) {
            showWait();
            window.location = simpleUpdateQueryStringParameter(redirectUrl, 'id', itemId);
        }
    }

    var simpleUpdateQueryStringParameter = function (uri, key, value) {

        var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
        var separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
        else {
            return uri + separator + key + "=" + value;
        }
    }

    var setupNode = function (elem, parentId) {
        var sourceNode;

        if (window._resources.lang == window._resources.bg) {
            sourceNode = $('<tr data-tt-id="' + elem.regionId + '" data-tt-parent-id="' + parentId + '"><td>' + elem.name + '</td></tr>');
        }
        else if (window._resources.lang == window._resources.en) {
            sourceNode = $('<tr data-tt-id="' + elem.regionId + '" data-tt-parent-id="' + parentId + '"><td>' + elem.nameAlt + '</td></tr>');
        }

        return sourceNode;
    };

    var prepareTree = function (rootMapData, drillMapsData, table, parentId) {

        _.each(rootMapData.regions, function (elem) {
            table.append(setupNode(elem, parentId)); // append the region
            var drilldownMap = _.find(drillMapsData, { id: elem.drilldown });
            if (drilldownMap) {
                prepareTree(drilldownMap, drillMapsData, table, elem.regionId);
            }
        });
    }

    var setupTree = function (div, rootId, drillMapsData) {

        var tree = $('<table></table>');

        var sourceNode = $('<tr data-tt-id="999" data-tt-parent-id=""><td>' + window._resources.international + '</td></tr>');
        tree.append(sourceNode);

        var sourceNode = $('<tr data-tt-id="888" data-tt-parent-id=""><td>' + window._resources.bulgaria + '</td></tr>');
        tree.append(sourceNode);

        prepareTree(_.find(drillMapsData, { id: rootId }), drillMapsData, tree, '');

        var dynamicContainer = $('<div class="mapTree"></div>').insertBefore(div).append(tree);

        // var button;
        // if (CurrentSettings.Interaction === SETTINGS.INTERACTION.MAP) {
        //     button = $('<button class="select-tree hide"/>');
        //     dynamicContainer.append(button);
        //     button.click(function () {
        //         var selectedId = $('tr.selected', tree).data('tt-id');
        //         if (selectedId) {
        //             redirectItem(selectedId);
        //         }
        //     });
        // }
        // var changeButtonText = function (text) {
        //     if (button) {
        //         button.text(window._resources.choose + ' ' + text);
        //         button.removeClass('hide');
        //     }
        // }

        tree.treetable({
            expandable: true,
            stringCollapse: window._resources.collapse,
            stringExpand: window._resources.expand
        });

        var selectNode = function (tr) {
            $(".selected").not(tr).removeClass("selected");
            $(tr).addClass("selected");
            // changeButtonText($('.treetext', tr).text());
        };

        tree.on(treeEvents.mouseDown, "tr", function () {
            if(!$(this).hasClass("leaf")) {
                selectNode(this);
            }
        });
        tree.on(treeEvents.selectSilent, "tr", function () {
            selectNode(this);
        });

        var clickEvent = function () {
            var tr = $(this).closest('tr');
            var id = tr.data('tt-id');
            if (CurrentSettings.Interaction === SETTINGS.INTERACTION.EXPLORER) {
                redirectItem(id);
            }
            else if (CurrentSettings.Interaction === SETTINGS.INTERACTION.MAP) {
                if (tr.hasClass('leaf')) {
                    // redirectItem(id);
                }
                else {
                    expandNodeAndParents(tree, id);

                    var currentDrillLevel = 0;
                    if (div.highcharts().drilldownLevels) {
                        currentDrillLevel = div.highcharts().drilldownLevels.length;
                    }

                    while (currentDrillLevel > 0) {
                        div.highcharts().drillUp();
                        currentDrillLevel--;
                    }

                    var parentMap = _.find(drillMapsData, function (item) {
                        return _.find(item.regions, { regionId: id });
                    });

                    drilldownAction(drillMapsData, div.highcharts(), rootId, _.find(parentMap.regions, { regionId: id }).drilldown);
                }
            }
        };

        var redirectEvent = function () {
            var tr = $(this).closest('tr');
            var id = tr.data('tt-id');

            redirectItem(id);
        };

        $.each($('td', tree).contents(), function (ind, item) {
            if (item.nodeType == 3) {
                var wrapper = '<a/>';
                if (CurrentSettings.Interaction === SETTINGS.INTERACTION.MAP) {
                    wrapper = '<span/>';
                }

                $(item).wrap(wrapper)
                    .parent()
                    .attr('class', 'treetext')
                    .click(clickEvent);

                var $redirectLink = $("<a/>");

                $redirectLink
                    .text(window._resources.choose)
                    .attr('class', 'map-redirect')
                    .attr('title', window._resources.choose)
                    .click(redirectEvent);

                $(item).parent().after($redirectLink);
            }
        });

        return tree;
    };

    $.each($('div[data-' + mapProps.map + '=true]'), function (i, div) {
        render($(div));
    });

});