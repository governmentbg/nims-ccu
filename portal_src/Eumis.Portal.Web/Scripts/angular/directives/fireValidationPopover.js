(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('scFireValidationPopover', [
      function () {
          'use strict';

          return {
              scope: {
                  scFireValidationPopover: '='
              },
              link: function (scope, element) {
                  scope.$watch('scFireValidationPopover', function (isOpen) {
                      if (!!isOpen) {
                          var currentHistoryTable = $(element).parents('tr').nextAll("tr.history-table").first();
                          var historyTableWrapper = currentHistoryTable.find("div.history-table-wrapper");
                          setTimeout(function () {
                              triggerTextareasEvents(historyTableWrapper);
                              fnPopover(historyTableWrapper);
                          }, 200);
                      }
                  });
              }
          };
      }
    ]);
}(angular));
