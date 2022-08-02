angular.module('moduleProjectErrandCollection', ['scaffolding', 'utils'])
    .controller('controllerProjectErrandCollection',
        ['$scope', '$window', '$filter', '$timeout', 'rfc4122', 'appcontext',
            function ($scope, $window, $filter, $timeout, rfc4122, appcontext) {
                $scope.init = function (globalKey, parentKey) {
                    $scope.items = $window[globalKey][parentKey].items;
                    $scope.areItemsValid = $window[globalKey][parentKey].areItemsValid;
                    $scope.maxProjectErrands = $window[globalKey]['constants'].ProjectErrandsMaxCount;
                    $scope.currentCulture = $window['_eumis_options']['currentCulture'];
                    $scope.globalKey = globalKey;
                    $scope.initActivations();
                };
                $scope.initActivations = function () {
                    $scope.$on('erandsActivation', function (event, args) {
                        if (!!args.update) {
                            appcontext.save(args.d, 'UpdateErands', $scope.items, {});
                        } else {
                            $scope.$apply(function () {
                                $scope.items.IsActive = !!args.isActive;
                            });
                        }
                    });
                };

                $scope.addItem = function () {
                    var item = {
                        editTriggerId: rfc4122.newuuid(),
                        IsNameValid: true,
                        IsErrandAreaValid: true,
                        IsErrandLegalActValid: true,
                        IsErrandTypeValid: true,
                        IsAmountValid: true,
                        IsPlanDateValid: true,
                        IsDescriptionValid: true
                    };

                    $scope.items.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (item) {
                    $scope.items.splice($scope.items.indexOf(item), 1);
                }

                $scope.getDisplayName = function (name, nameEN) {
                    if ($scope.currentCulture === $window[$scope.globalKey]['constants'].CultureEN) {
                        return nameEN;
                    }

                    return name;
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);
