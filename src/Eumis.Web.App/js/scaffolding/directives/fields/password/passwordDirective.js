// Usage: <sc-password ng-model="<model_name>"></sc-text>

import passwordDirectiveTemplateUrl from './passwordDirective.html';

function PasswordDirective() {
  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    templateUrl: passwordDirectiveTemplateUrl
  };
}

export { PasswordDirective as scPasswordDirective };
