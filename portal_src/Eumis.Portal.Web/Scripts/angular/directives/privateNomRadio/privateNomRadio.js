(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('privateNomRadio', ['rfc4122', '$filter', function (rfc4122, $filter) {
        return {
            scope: {
                model: '=ngModel',
                path: '@'
            },
            templateUrl: '/Scripts/angular/directives/privateNomRadio/privateNomRadio.html',
            link: function (scope, element, attrs) {
                scope.editorId = rfc4122.newuuid();

                scope.$watch('model.id', function (newVal, oldVal) {
                    if (newVal !== oldVal) {
                        let selectedItem = $filter('filter')(scope.model.Items, function (n) {
                            return n.Value == newVal;
                        })[0];
                        scope.model.Name = selectedItem.Name;
                        scope.model.NameEN = selectedItem.NameEN;
                    }
                });
            }
        };
    }]);
}(angular));