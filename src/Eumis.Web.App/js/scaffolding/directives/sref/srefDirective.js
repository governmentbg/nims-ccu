// Usage: <a sc-sref="{state: '...', params: {...}, options: {...}}" name="linkName">text</a>

function SrefDirective($parse, $state) {
  return {
    restrict: 'A',
    priority: 110,
    link: function(scope, element, attrs) {
      var elementCtrl = {},
        srefExpr = $parse(attrs.scSref)(scope),
        state = srefExpr.state,
        params = srefExpr.params,
        options = srefExpr.options,
        link = $state.href(state, params, options);

      attrs.$set('href', link);

      scope[attrs.name] = elementCtrl;
    }
  };
}

SrefDirective.$inject = ['$parse', '$state'];

export { SrefDirective as scSrefDirective };
