(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('booleanRadio', ['rfc4122', function (rfc4122) {
        return {
            scope: {
                model: '=ngModel',
                resources: '=',
                path: '@'
            },
            templateUrl: '/Scripts/angular/directives/booleanRadio/booleanRadio.html',
            link: function (scope, element, attrs) {
                scope.editorId = rfc4122.newuuid();
            }
        };
    }]);
}(angular));