// Usage: <sc-link-button name="" sc-sref="{state: '...', params: {...}}" text="" class="" icon="">
//</sc-link-button>

import linkButtonDirectiveTemplateUrl from './linkButtonDirective.html';

function LinkButtonDirective($parse, l10n) {
  return {
    restrict: 'E',
    priority: 110,
    scope: {
      icon: '@'
    },
    replace: true,
    templateUrl: linkButtonDirectiveTemplateUrl,
    link: function(scope, element, attrs) {
      var elementCtrl = {};

      scope.$parent[attrs.name] = elementCtrl;

      scope.text = l10n.get(attrs.text);
      if (!scope.text) {
        scope.text = attrs.text;
      }

      scope.iconBaseClass = scope.icon ? scope.icon.substring(0, scope.icon.indexOf('-')) : null;
    }
  };
}

LinkButtonDirective.$inject = ['$parse', 'l10n'];

export { LinkButtonDirective as scLinkButtonDirective };
