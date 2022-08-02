/*global angular*/

(function (angular) {
    'use strict';
    
    angular.module('moduleProjectSpecFields', ['scaffolding'])
        .controller('controllerProjectSpecFields',
            ['$scope', '$window', '$filter',
                function ($scope, $window, $filter) {
                    $scope.init = function (globalKey, parentKey) {
                        $scope.items = $window[globalKey][parentKey].items;
                    };

                    $scope.delItem = function (item) {
                        $scope.items.splice($scope.items.indexOf(item), 1);
                    };
                    
                    $scope.$evalAsync(function () {
                        $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                    });
                }]);
}(angular));
