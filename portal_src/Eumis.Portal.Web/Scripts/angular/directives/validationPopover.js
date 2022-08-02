(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('scValidationPopover', [
      function () {
          'use strict';

          return {
              restrict: 'A',
              link: function (scope, element, attrs) {

                  $(element).popover({
                      container: 'body',
                      content: function () {
                          if (typeof validationSummaryErrors !== "undefined") {

                              var _self = this;

                              // if child elements has .input-validation-error
                              if (this.tagName === "DIV") {

                                  // select2 controls
                                  _self = $(this).parent().find("input.validation-error-key, select.validation-error-key").first();

                                  // uploader
                                  if (typeof _self === "undefined") {

                                  }
                              }
                              else if (this.tagName === "SELECT" && $(this).attr("ng-model")) {

                                  // hidden element
                                  _self = $(this).parent().find(".input-validation-error[name], .validation-error-key[name]").first();
                              }
                              else if (this.tagName === "TD") {
                                  _self = $(this).find("input.validation-error-key, select.validation-error-key").first();
                              }

                              return validationSummaryErrors[$(_self).attr("name")];
                          }
                      },
                      placement: "top",
                      trigger: "hover",
                      'template': '<div class="popover popover-error" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
                  });
              }
          };
      }
    ]);
}(angular));
