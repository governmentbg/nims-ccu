(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('nomRadio', ['rfc4122', '$filter', function (rfc4122, $filter) {
        return {
            scope: {
                model: '=ngModel',
                path: '@',
                disabled: '=',
                nomItems: '='
            },
            templateUrl: '/Scripts/angular/directives/nomRadio/nomRadio.html',
            link: function (scope, element, attrs) {
                scope.modelName = attrs.modelName ? attrs.modelName : 'Name';
                scope.modelId = attrs.modelId ? attrs.modelId : 'id';
                scope.nomName = attrs.nomName ? attrs.nomName : 'Name';
                scope.nomId = attrs.nomId ? attrs.nomId : 'Id';

                scope.editorId = rfc4122.newuuid();

                scope.assignName = function (nomItem) {
                    scope.model[scope.modelName] = nomItem[scope.nomName];
                }
            }
        };
    }]);
}(angular));