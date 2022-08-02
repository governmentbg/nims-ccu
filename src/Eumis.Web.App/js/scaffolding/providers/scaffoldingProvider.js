function ScaffoldingProvider($compileProvider) {
  this.form = function(options) {
    $compileProvider.directive(options.name, [
      '$parse',
      '$controller',
      function($parse, $controller) {
        return {
          restrict: 'E',
          replace: true,
          scope: {
            model: '=ngModel'
          },
          templateUrl: options.templateUrl,
          link: {
            pre: function(scope, element, attrs) {
              var scFormParams, locals;

              if (options.controller) {
                scFormParams = $parse(attrs.scFormParams)(scope.$parent);
                locals = {
                  $scope: scope,
                  $element: element,
                  $attrs: attrs,
                  scFormParams: scFormParams ? scFormParams : {}
                };

                $controller(options.controller, locals);
              }
            },
            post: function(scope, element, attrs) {
              if (attrs.readonly) {
                scope.$parent.$watch(attrs.readonly, function(readonly) {
                  scope.readonly = readonly;
                });
              }

              scope.$parent[attrs.name] = scope[attrs.name];
              scope.form = scope[attrs.name];
            }
          }
        };
      }
    ]);
  };
}

ScaffoldingProvider.$inject = ['$compileProvider'];

ScaffoldingProvider.prototype.$get = function() {};

export { ScaffoldingProvider as scaffoldingProvider };
