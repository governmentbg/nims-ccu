// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('confirmClick', [
        function () {
            return {
                link: function (scope, element, attr) {
                    var clickAction = attr.confirmClick;

                    element.confirmation({
                        container: 'body',
                        btnOkLabel: $("#res_yes").val(),
                        btnCancelLabel: $("#res_no").val(),
                        title: $("#res_confirm").val(),
                        popout: true,
                        onConfirm: function () {
                            scope.$apply(function () {
                                scope.$eval(clickAction);
                            });
                        }
                    });
                }
            };
        }]);
}(angular));