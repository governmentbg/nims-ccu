angular.module('modulePreliminaryContract', ['scaffolding'])
    .controller('controllerPreliminaryContract',
        ['$scope', '$filter', '$window',
            function ($scope, $filter, $window) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.model = $window[globalKey][parentKey].model;
                    $scope.resourcesObjectBoolean = $window[globalKey][parentKey].resourcesObjectBoolean;
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);