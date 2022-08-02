// Usage: <sc-file ng-model="<model_name>"> </sc-file>

import fileDirectiveTemplateUrl from './fileDirective.html';

export const scFileConfigConstant = {
  uploadUrl: window.eumisConfiguration.blobServerLocation
};

export const scFileDirective = [
  '$injector',
  '$q',
  function($injector, $q) {
    return {
      priority: 110,
      restrict: 'E',
      controller: 'FileCtrl',
      require: ['scFile', '?ngModel'],
      replace: true,
      scope: {
        urlParams: '&'
      },
      templateUrl: fileDirectiveTemplateUrl,
      link: function link(scope, iElement, iAttrs, controllers) {
        var fileCtrl = controllers[0],
          ngModelCtrl = controllers[1];

        iAttrs.$observe('readonly', function(value) {
          scope.isReadonly = value === true;
        });

        if (iAttrs.urlTemplate) {
          scope.urlTemplate = $injector.get(iAttrs.urlTemplate);
        } else {
          throw new Error('trying to create <sc-file> directive with no url-template');
        }

        function validateBlobSize(viewValue) {
          var isValid = null;
          if (viewValue !== null && viewValue !== undefined) {
            //we want to set the validity of 'eumisMaxBlobSize' only when the size property
            //has a value, which will happen only when we pass the fake file
            if (viewValue.size !== null && viewValue.size !== undefined) {
              isValid = viewValue.size < window.eumisConfiguration.maxBlobSize;
              ngModelCtrl.$setValidity('eumisMaxBlobSize', isValid);
            } else {
              isValid = true;
              ngModelCtrl.$setValidity('eumisMaxBlobSize', isValid);
            }
          }
          return $q.resolve(isValid);
        }

        function validatorFn(viewValue) {
          validateBlobSize(viewValue);
          return viewValue;
        }

        ngModelCtrl.$parsers.push(validatorFn);
        ngModelCtrl.$formatters.push(validatorFn);

        fileCtrl.setNgModelCtrl(ngModelCtrl, scope);
      }
    };
  }
];
