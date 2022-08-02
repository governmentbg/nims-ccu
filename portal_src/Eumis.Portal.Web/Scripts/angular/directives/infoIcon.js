(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('infoIcon', [
      function () {
          'use strict';

          return {
              restrict: 'A',
              link: function (scope, element, attrs) {
                  $(element).popover({
                      container: 'body',
                      placement: function (tip, element) {
                          if ($(element).attr('sc-placement') !== undefined) {
                              return $(element).attr('sc-placement');
                          } else {
                              var offset = $(element).offset();
                              var height = $(document).outerHeight();
                              var width = $(document).outerWidth();
                              var vert = 0.5 * height - offset.top;
                              var vertPlacement = vert > 0 ? 'top' : 'bottom';
                              var horiz = 0.5 * width - offset.left;
                              var horizPlacement = horiz > 0 ? 'right' : 'left';
                              var placement = Math.abs(horiz) > Math.abs(vert) ? horizPlacement : vertPlacement;
                              return placement;
                          }
                      }
                  });
              }
          };
      }
    ]);
}(angular));


