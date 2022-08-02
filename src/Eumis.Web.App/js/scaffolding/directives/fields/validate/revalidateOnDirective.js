// Usage:
//  <input type="text" ng-model="dateFrom" name="dateFrom" />
//  <input type="text" ng-model="dateTo" name="dateTo" sc-validate="{greaterThanFrom: greaterThanFrom}" sc-revalidate-on="['dateFrom']"/>

function RevalidateOnDirective($parse) {
  return {
    restrict: 'A',
    require: '?ngModel',
    scope: false,
    link: function(scope, element, attrs, control) {
      var watches = $parse(attrs.scRevalidateOn)(scope);
      var unsubscribe = scope.$watchGroup(watches, function(newValue, oldValue) {
        if (newValue !== oldValue) {
          control.$validate();
        }
      });

      element.on('$destroy', function() {
        unsubscribe();
      });
    }
  };
}

RevalidateOnDirective.$inject = ['$parse'];

export { RevalidateOnDirective as scRevalidateOnDirective };
