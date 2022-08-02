// Usage: <button type="button" sc-click="save()" name="saveButton">Save</button>

function ClickDirective($parse, $exceptionHandler) {
  return {
    restrict: 'A',
    link: function(scope, element, attrs) {
      var elementCtrl = {},
        clickExpr = $parse(attrs.scClick);

      scope[attrs.name] = elementCtrl;

      element.on('click', function(event) {
        if (element.prop('disabled') || element.attr('disabled') === 'disabled') {
          return;
        }

        if (elementCtrl.$pending) {
          return;
        }

        scope.$apply(function() {
          // add $event local variables to the expression context as ngClick does
          var result = clickExpr(scope, { $event: event });

          // check if the result is promise
          if (result && result.then && typeof result.then === 'function') {
            elementCtrl.$pending = true;
            result['catch'](function(error) {
              $exceptionHandler(error);
            })['finally'](function() {
              delete elementCtrl.$pending;
            });
          }
        });
      });
    }
  };
}

ClickDirective.$inject = ['$parse', '$exceptionHandler'];

export { ClickDirective as scClickDirective };
