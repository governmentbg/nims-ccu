(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('symbolsCount', ['$compile', function ($compile) {
        return {
            link: function (scope, element, attrs) {

                scope.getInputLength = function (input) {

                    return getLength(input);
                };

                attrs.$set("ngTrim", "false");
                var countText = attrs.countText ? attrs.countText : 'Брой въведени символи';
                var exp = '{{ getInputLength(' + attrs.ngModel + ') }}';
                var html = $compile('<span style="width: 10%; float: right; padding-top: 10px; text-align: right;">' + exp + '</span>')(scope); //countText + ': ' + exp + '</span>')(scope);

                var counterElement = angular.element(html);
                counterElement.insertBefore(element);
            }
        };
    }]);
}(angular));