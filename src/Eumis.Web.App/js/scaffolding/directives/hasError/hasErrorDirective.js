// Usage: <div sc-has-error="fieldName"></div>
function HasErrorDirective() {
  return {
    restrict: 'A',
    link: function(scope, element, attrs) {
      scope.form = element.parent().controller('form');
      scope.$watchCollection(
        '[form["' + attrs.scHasError + '"].$invalid, form.$submitted, form.$readonly]',
        function(newValue, oldValue) {
          if (newValue === oldValue) {
            return;
          }
          if (newValue[0] && newValue[1] && !newValue[2]) {
            element.addClass('has-error');
          } else {
            element.removeClass('has-error');
          }
        }
      );
    }
  };
}

export { HasErrorDirective as scHasErrorDirective };
