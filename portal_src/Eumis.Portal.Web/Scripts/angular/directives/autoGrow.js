(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('autoGrow', ['$timeout', '$window',
      function ($timeout, $window) {
          'use strict';

          return {
              require: 'ngModel',
              restrict: 'A, C',
              link: function (scope, element, attrs, ngModel) {
                  $(element).css('overflow', 'hidden').autogrow();
              }
          };
      }
    ]);
}(angular));


