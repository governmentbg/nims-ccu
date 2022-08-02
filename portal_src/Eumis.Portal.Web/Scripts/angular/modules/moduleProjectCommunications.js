angular.module('moduleProjectCommunications', ['scaffolding', 'utils'])
    .controller('controllerProjectCommunications',
        ['$scope', '$window',
            function ($scope, $window) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.model = $window[globalKey][parentKey].model;
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]
    );
