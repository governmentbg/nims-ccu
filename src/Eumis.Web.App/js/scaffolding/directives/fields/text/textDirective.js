// Usage: <sc-text ng-model="<model_name>"></sc-text>

import textDirectiveTemplateUrl from './textDirective.html';

function TextDirective() {
  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    templateUrl: textDirectiveTemplateUrl
  };
}

export { TextDirective as scTextDirective };
