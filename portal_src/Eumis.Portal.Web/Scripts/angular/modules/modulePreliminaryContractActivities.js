angular.module('modulePreliminaryContractActivities', ['scaffolding', 'utils'])
    .controller('controllerPreliminaryContractActivities',
        ['$scope', '$window', '$filter', '$timeout', 'rfc4122', 'appcontext',
            function ($scope, $window, $filter, $timeout, rfc4122, appcontext) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.items = $window[globalKey][parentKey].items;
                    $scope.areItemsValid = $window[globalKey][parentKey].areItemsValid;
                    $scope.maxContractActivities = $window[globalKey]['constants'].ContractActivitiesMaxCount;
                };

                $scope.addItem = function () {
                    var item = {
                        editTriggerId: rfc4122.newuuid(),
                        IsCodeValid: true,
                        IsNameValid: true,
                        IsResultValid: true
                    };

                    $scope.items.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (item) {
                    $scope.items.splice($scope.items.indexOf(item), 1);
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);
