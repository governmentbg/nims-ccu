// Usage: <div sc-has-error="fieldName"></div>
/*global angular*/
(function (angular) {
  'use strict';

  function HasErrorDirective() {
    return {
      restrict: 'A',
      link: function (scope, element, attrs) {
        scope.form = element.parent().controller('form');
        scope.$watchCollection('[form["' + attrs.scHasError +
          '"].$invalid, form.$validated, form.$readonly, form["' + attrs.scHasError +
          '"].$error.$pending]',
          function (newValue, oldValue) {
            if (newValue === oldValue) {
              return;
            }
            if (newValue[0] && newValue[1] && !newValue[2] && !newValue[3]) {
              element.addClass('has-error');
            }
            else {
              element.removeClass('has-error');
            }
          }
        );
      }
    };
  }

  angular.module('scaffolding')
    .directive('scHasError', HasErrorDirective);
}(angular));
