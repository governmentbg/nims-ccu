// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('validNumber', function () {
         return {
             require: '?ngModel',
             link: function (scope, element, attrs, ngModelCtrl) {
                 if (!ngModelCtrl) {
                     return;
                 }

                 var min = attrs.minValue ? parseInt(attrs.minValue) : null;
                 var max = attrs.maxValue ? parseInt(attrs.maxValue) : null;

                 ngModelCtrl.$parsers.push(function (val) {
                     var clean = val.replace(/[^0-9]+/g, '');
                     var clearVal = parseInt(clean);
                     var defaultVal = '';

                     if (val !== clean || isNaN(clearVal)) {
                         ngModelCtrl.$setViewValue(clean);
                         ngModelCtrl.$render();
                         return clearVal;
                     }
                     else {
                         var result = clearVal.toString();

                         if (min && clearVal < min) {
                             result = min.toString();
                         }
                         else if (max && clearVal > max) {
                             result = max.toString();
                         }

                         ngModelCtrl.$setViewValue(result);
                         ngModelCtrl.$render();
                         return clearVal;
                     }
                 });

                 element.bind('keypress', function (event) {
                     if (event.keyCode === 32) {
                         event.preventDefault();
                     }
                 });
             }
         };
     });
}(angular));