// Usage: <input type="text" ng-model="egn" name="egn" sc-validate="{validEGN: isValidEGN}"/>
// Usage: <input type="text" ng-model="username" name="username" sc-validate-async="{unique: isUnique}"/>
import _ from 'lodash';

function validateDirectiveCreator(isAsync) {
  return [
    '$parse',
    '$q',
    function ValidateDirective($parse, $q) {
      function getPendingPromise($scope, form) {
        var pendingPromise, pendingDeferred, pendingWatch;

        if (form.$pending) {
          pendingDeferred = $q.defer();
          pendingWatch = $scope.$watch(
            function() {
              return form.$pending;
            },
            function(pending) {
              if (!pending) {
                pendingWatch();
                pendingDeferred.resolve();
              }
            }
          );
          pendingPromise = pendingDeferred.promise;
        } else {
          pendingPromise = $q.resolve();
        }
        return pendingPromise;
      }

      return {
        restrict: 'A',
        require: ['?ngModel', '?form'],
        scope: false,
        link: function(scope, element, attrs, controllers) {
          var validators = $parse(isAsync ? attrs.scValidateAsync : attrs.scValidate)(scope),
            control,
            form;

          if (controllers[1]) {
            // using sc-validate on a form
            form = controllers[1];

            form.$validate = function() {
              form.$setSubmitted();

              return getPendingPromise(scope, form).then(function() {
                return form.$valid;
              });
            };
          } else {
            // using sc-validate on an input
            control = controllers[0];

            _.assign(isAsync ? control.$asyncValidators : control.$validators, validators);
          }
        }
      };
    }
  ];
}

export const scValidateDirective = validateDirectiveCreator(false);
export const scValidateAsyncDirective = validateDirectiveCreator(true);
