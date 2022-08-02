//Usage: <sc-select sc-select="{ select2 options object }" ng-model="model"></sc-select>

import selectDirectiveTemplateUrl from './selectDirective.html';

function SelectDirective() {
  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    templateUrl: selectDirectiveTemplateUrl,
    compile: function(tElement, tAttrs) {
      tAttrs.$set('uiSelect2', tAttrs.scSelect);
    }
  };
}

export { SelectDirective as scSelectDirective };
