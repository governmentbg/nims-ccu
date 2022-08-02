angular.module('moduleIndicators', ['scaffolding', 'utils'])
    .controller('controllerIndicators',
        ['$scope', '$window', '$filter', '$timeout', 'rfc4122',
            function ($scope, $window, $filter, $timeout, rfc4122) {

                function mapIndicator(indicator) {
                    return {
                        id: indicator.Id,
                        text: indicator.displayName,
                        name: indicator.Name,
                        nameEN: indicator.NameEN,
                        typeName: indicator.TypeName,
                        typeNameEN: indicator.TypeNameEN,
                        displayTypeName: indicator.displayTypeName,
                        trendName: indicator.TrendName,
                        trendNameEN: indicator.TrendNameEN,
                        displayTrendName: indicator.displayTrendName,
                        kindName: indicator.KindName,
                        kindNameEN: indicator.KindNameEN,
                        displayKindName: indicator.displayKindName,
                        measureName: indicator.MeasureName,
                        measureNameEN: indicator.MeasureNameEN,
                        displayMeasureName: indicator.displayMeasureName,

                        aggregatedReport: indicator.AggregatedReport,
                        aggregatedReportEN: indicator.AggregatedReportEN,
                        displayAggregatedReport: indicator.displayAggregatedReport,
                        aggregatedTarget: indicator.AggregatedTarget,
                        aggregatedTargetEN: indicator.AggregatedTargetEN,
                        displayAggregatedTarget: indicator.displayAggregatedTarget,

                        hasGenderDivision: indicator.HasGenderDivision
                    };
                }

                $scope.init = function (globalKey, parentKey) {
                    $scope.items = $window[globalKey][parentKey].items;
                    $scope.indicatorNomenclature = ($window[globalKey][parentKey].indicatorNomenclature || []).map(function (item) {
                        return mapIndicator(item);
                    });

                    $scope.areItemsValid = $window[globalKey][parentKey].areItemsValid;
                    $scope.hasUniqueIds = $window[globalKey][parentKey].hasUniqueIds;

                    $scope.items.forEach(function (item) {
                        item.SelectedIndicator = mapIndicator(item);
                    });

                    $scope.$watch('items', function () {
                        $scope.updateItemsTotals();
                    }, true);
                };

                $scope.updateItemsTotals = function () {
                    $scope.items.forEach(function (item) {
                        if (item.SelectedIndicator != null
                             && item.SelectedIndicator.hasGenderDivision) {
                            item.BaseTotal = (((1 * item.BaseMen) || 0) + ((1 * item.BaseWomen) || 0)).toFixed(2);
                            item.TargetTotal = (((1 * item.TargetMen) || 0) + ((1 * item.TargetWomen) || 0)).toFixed(2);
                        }
                    });
                }

                $scope.addItem = function () {
                    var item = {
                        editTriggerId: rfc4122.newuuid(),
                        IsNameValid: true,
                        IsBaseMenValid: true,
                        IsBaseWomenValid:  true,
                        IsBaseValid: true,
                        IsTargetMenValid: true,
                        IsTargetWomenValid: true,
                        IsTargetValid: true,
                        IsDescriptionValid: true
                    }

                    $scope.items.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (item) {
                    $scope.items.splice($scope.items.indexOf(item), 1);
                }

                $scope.clearBaseTarget = function (item) {
                    var defaultValue = '0.00';

                    item.BaseMen = defaultValue;
                    item.BaseWomen = defaultValue;
                    item.BaseTotal = defaultValue;

                    item.TargetMen = defaultValue;
                    item.TargetWomen = defaultValue;
                    item.TargetTotal = defaultValue;
                }

                $scope.loadIndicatorNomenclature = function (query) {
                    var data = { results: [] };
                    $.each($scope.indicatorNomenclature, function () {
                        if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                            data.results.push(this);
                        }
                    });
                    query.callback(data);
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);
