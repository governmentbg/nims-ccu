// Usage: <sc-time ng-model="<model_name>" />

import timeDirectiveTemplateUrl from './timeDirective.html';

function TimeDirective() {
  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    require: '?ngModel',
    scope: {},
    templateUrl: timeDirectiveTemplateUrl,
    link: function(scope, element, attrs, ngModel) {
      if (!ngModel) {
        return;
      }

      var infinityMode = attrs.mode === 'infinity';

      attrs.$observe('readonly', function(value) {
        scope.isReadonly = !!value;
      });

      function calculateMilliseconds() {
        if (scope.hours === undefined && scope.minutes === undefined) {
          ngModel.$setViewValue(undefined);
        } else {
          var hours = scope.hours ? scope.hours : 0,
            minutes = scope.minutes ? scope.minutes : 0,
            milliseconds = (60 * hours + minutes) * 60000;

          ngModel.$setViewValue(milliseconds);
        }
      }

      ngModel.$render = function() {
        if (ngModel.$viewValue !== undefined && ngModel.$viewValue !== null) {
          var minutes = ngModel.$viewValue / 60000;

          scope.hours = Math.floor(minutes / 60);
          scope.minutes = Math.floor(minutes % 60);
        }
      };

      scope.$watch('minutes', function(value) {
        scope.minutes = value < 0 || value > 59 ? undefined : value;

        calculateMilliseconds();
      });

      scope.$watch('hours', function(value) {
        if (!infinityMode) {
          scope.hours = value < 0 || value > 23 ? undefined : value;
        }

        calculateMilliseconds();
      });
    }
  };
}

export { TimeDirective as scTimeDirective };
