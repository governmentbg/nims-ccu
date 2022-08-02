// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('scDatepicker', ['$timeout', function ($timeout) {
        return {
             restrict: 'A',
             require: 'ngModel',
             link: function (scope, element, attrs, ngModelCtrl) {
                 $timeout(function () {
                     element.datepicker({
                         format: "dd.mm.yyyy",
                         startView: 0,
                         weekStart: 1,
                         language: attrs.lang || "bg",
                         startDate: new Date(Date.parse(attrs.minDate)) || new Date(-1),
                         autoclose: true
                     }).on('changeDate', function (e) {
                         ngModelCtrl.$setViewValue(e.date);
                         scope.$apply();
                     });

                     element.keydown(function () {
                         var _self = $(this);
                         if (_self.val().length > 10) {
                             $(this).val(_self.val().substring(0, 9));
                         }
                         else {
                             for (var i = 0; i < _self.val().length; i++) {
                                 if (i === 2 || i === 5) {
                                     if (_self.val()[i] !== '.') {
                                         $(this).val(_self.val().substring(0, i));
                                         break;
                                     }
                                 } else {
                                     if (!IsNumeric(_self.val()[i])) {
                                         $(this).val(_self.val().substring(0, i));
                                         break;
                                     }
                                 }
                             }
                         }

                         function IsNumeric(input) {
                             var RE = /^-{0,1}\d*\.{0,1}\d+$/;
                             return (RE.test(input));
                         }
                     });

                     element.change(function () {
                         var RE = /(^((([0][1-9]|1[0-9]|2[0-8])\.(0[1-9]|1[012]))|((29|30|31)\.(0[13578]|1[02]))|((29|30)\.(0[4,6,9]|11)))\.(19|[2-9][0-9])\d\d$)|(^29\.02\.(19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)/;
                         if (!RE.test($(this).val())) {
                             $(this).val('');
                         }
                     });
                 }, 0, false);
             }
         }
     }]);
}(angular));