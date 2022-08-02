(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('historyBtn', [
      function () {
          'use strict';

          return {
              restrict: 'A',
              link: function (scope, element, attrs) {
                  $(element).on("click", function (e) {
                      e.preventDefault();
                      triggerHistoryButtonClick($(this));
                  });
              }
          };
      }
    ]);
}(angular));


