// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('eumisAddress', function () {
        return {
            restrict: 'E',
            scope: {
                path: '@',
                bgCode: '@',
                model:'=ngModel',
                resources: '=',
                showManagement: '='
            },
            templateUrl: '/Scripts/angular/directives/eumisAddress/eumisAddress.html'
        }
    });
}(angular));