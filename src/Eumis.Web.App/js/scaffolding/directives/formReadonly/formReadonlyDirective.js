// Usage: <ng-form name="" sc-form-readonly=""></ng-form>

import angular from 'angular';
import _ from 'lodash';

function FormReadonlyDirective() {
  return {
    restrict: 'A',
    require: ['?form'],
    scope: false,
    link: function(scope, element, attrs, formController) {
      var parentForm = _.first(
          _.map(element.parents('ng-form, [ng-form]'), function(formElem) {
            return angular.element(formElem).controller('form');
          })
        ),
        deregFunc,
        form = formController[0];

      scope.$watch(attrs.scFormReadonly, function(formReadonly) {
        if (formReadonly !== undefined) {
          form.$readonly = formReadonly;
        } else if (scope.readonly !== undefined) {
          if (deregFunc) {
            deregFunc();
          }
          form.$readonly = scope.readonly;
          deregFunc = scope.$watch('readonly', function(formReadonly) {
            form.$readonly = formReadonly;
          });
        } else {
          if (parentForm && _.has(parentForm, '$readonly')) {
            if (deregFunc) {
              deregFunc();
            }
            form.$readonly = formReadonly;
            deregFunc = scope.$watch(
              function() {
                return parentForm.$readonly;
              },
              function(formReadonly) {
                form.$readonly = formReadonly;
              }
            );
          } else {
            form.$readonly = false;
          }
        }
      });
    }
  };
}

export { FormReadonlyDirective as scFormReadonlyDirective };
