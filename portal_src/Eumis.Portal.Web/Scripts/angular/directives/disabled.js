// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('scDisabled', function () {
         return {
             restrict: 'A',
             priority: 0,
             link: function (scope, element, attr) {
                 scope.$watch(attr['scDisabled'], function ngBooleanAttrWatchAction(value) {
                     if (value) {
                         element.attr('disabled', 'disabled');
                     }
                     else {
                         element.removeAttr('disabled');
                     }
                 });
             }
         };
     });
}(angular));