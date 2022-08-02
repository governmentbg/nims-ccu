angular.module('moduleEumisNomenclature', ['scaffolding'])
    .controller('controllerEumisNomenclature',
        ['$scope', '$filter', '$window',
            function ($scope, $filter, $window) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.item = $window[globalKey][parentKey].item;
                };
                
                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);