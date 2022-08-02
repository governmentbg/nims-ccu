angular.module('moduleProjectBasicData', ['scaffolding', 'utils'])
    .controller('controllerProjectBasicData',
        ['$scope', '$window',
            function ($scope, $window) {
                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.model = $window[globalKey][parentKey].projectBasicData;
                    $scope.resourcesObjectBoolean = $window[globalKey][parentKey].resourcesObjectBoolean;
                    $scope.resourcesObjectNuts = $window[globalKey][parentKey].resourcesObjectNuts;
                    $scope.nuts = $window[globalKey][parentKey].nuts;
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);