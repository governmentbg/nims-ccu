angular.module('moduleProgrammeContractActivities', ['scaffolding', 'utils'])
    .controller('controllerProgrammeContractActivities',
        ['$scope', '$window', '$filter', '$timeout', 'rfc4122', 'appcontext',
            function ($scope, $window, $filter, $timeout, rfc4122, appcontext) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.index = $window[globalKey][parentKey].index;
                    $scope.items = $window[globalKey][parentKey].items;
                    $scope.areItemsValid = $window[globalKey][parentKey].areItemsValid;
                    $scope.isPeriodValid = $window[globalKey][parentKey].isPeriodValid;
                    $scope.maxDiagramMonth = 100;
                    $scope.maxContractActivities = $window[globalKey]['constants'].ContractActivitiesMaxCount;

                    $scope.items.forEach(function (item) {
                        item.StartMonth = parseInt(item.StartMonth);
                        item.Duration = parseInt(item.Duration);
                    });
                    $scope.initActivations();
                    
                    // load nomenclature of dependant section are locked
                    $scope.updateCompanyNomenclature();
                };

                $scope.initActivations = function () {
                    $scope.$on('programmesActivation', function (event, args) {
                        if (!!args.update) {
                            appcontext.save(args.d, 'UpdateProgrammes', $scope.items, { index: $scope.index });
                        } else {
                            $scope.$apply(function () {
                                $scope.items.IsActive = !!args.isActive;
                            });
                        }
                    });
                };
                
                $scope.addItem = function () {
                    var selectedCandidate = $window[$scope.globalKey]['_selected_candidate'];
                    var isSelectedCandidate = selectedCandidate != null && selectedCandidate.id != null && selectedCandidate.text != null;

                    var item = {
                        editTriggerId: rfc4122.newuuid(),
                        IsCompanyValid: true,
                        IsCodeValid: true,
                        IsNameValid: true,
                        IsExecutionMethodValid: true,
                        IsResultValid: true,
                        IsStartMonthValid: true,
                        IsDurationValid: true,
                        IsAmountValid: true,
                        CompanyCollection: isSelectedCandidate ? [{ id: selectedCandidate.id, text: selectedCandidate.text, Name: selectedCandidate.Name, NameEN: selectedCandidate.NameEN }] : []
                    };

                    $scope.items.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (item) {
                    $scope.items.splice($scope.items.indexOf(item), 1);
                }

                $scope.loadCompanyNomenclature = function (query) {
                    var data = { results: [] };
                    $.each($scope.companyNomenclature, function () {
                        if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                            data.results.push(this);
                        }
                    });
                    query.callback(data);
                };

                $scope.$on('triggerBroadcast', function (event, args) {
                    $scope.updateCompanyNomenclature();
                    $scope.updateCompanyCollection();
                });

                $scope.updateCompanyNomenclature = function () {
                    $scope.companyNomenclature = [];

                    if ($window[$scope.globalKey]['_selected_candidate']) {
                        $scope.companyNomenclature.push($window[$scope.globalKey]['_selected_candidate']);
                    }

                    var partners = $window[$scope.globalKey]['_selected_partners'] || [];
                    partners.forEach(function (partner) {
                        var map = $.map($scope.companyNomenclature, function (val) { return val.id; });
                        if (map.indexOf(partner.id) == -1) {
                            $scope.companyNomenclature.push(partner);
                        }
                    });
                };

                $scope.updateCompanyCollection = function () {
                    var selectedCandidate = $window[$scope.globalKey]['_selected_candidate'];

                    $scope.items.forEach(function (item) {
                        if (angular.isDefined(item.CompanyCollection) && item.CompanyCollection !== null && item.CompanyCollection.length > 0) {
                            var mapIds = $.map($scope.companyNomenclature, function (val) { return val.id; });
                            var mapNames = $.map($scope.companyNomenclature, function (val) { return val.text; });

                            item.CompanyCollection.forEach(function (selected) {
                                if (mapIds.indexOf(selected.id) == -1
                                    || mapNames.indexOf(selected.text) == -1) {
                                    $scope.$apply(function () {
                                        var index = item.CompanyCollection.indexOf(selected);
                                        item.CompanyCollection.splice(index, 1);
                                    });
                                }
                            });
                        }


                        if (selectedCandidate && selectedCandidate.id && selectedCandidate.text
                            && !(angular.isDefined(item.CompanyCollection) && item.CompanyCollection !== null && item.CompanyCollection.length > 0)) {
                            if (!item.CompanyCollection) {
                                item.CompanyCollection = [];
                            }

                            $scope.$apply(function () {
                                item.CompanyCollection.push({
                                    id: selectedCandidate.id,
                                    text: selectedCandidate.text,
                                    Name: selectedCandidate.Name,
                                    NameEN: selectedCandidate.NameEN
                                });
                            });
                        }
                    });
                };

                $scope.months = [1];

                $scope.$watch('items', function () {
                    var maxMonth = 1;

                    $scope.items.forEach(function (item) {
                        var startMonth = parseInt(item.StartMonth);
                        var duration = parseInt(item.Duration);
                        var currentEndMonth = 0;

                        if (!isNaN(startMonth) && !isNaN(duration))
                            currentEndMonth = startMonth + duration;

                        if (currentEndMonth > maxMonth && currentEndMonth <= $scope.maxDiagramMonth) {
                            maxMonth = currentEndMonth;
                        }
                    });

                    if (maxMonth > 1 && maxMonth != $scope.months.length) {
                        $scope.months = new Array(maxMonth - 1);

                        for (var i = 0; i < $scope.months.length; i++)
                            $scope.months[i] = (i + 1);
                    }
                }, true)

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);
