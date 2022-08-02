// Usage: <sc-float ng-model="<model_name>"></sc-float>
import numberDirectiveTemplateUrl from './numberDirective.html';

export const scFloatParserFactoryConstant = function() {
  return function(strValue) {
    var num = parseFloat((strValue || '').replace(',', '.'));
    return isNaN(num) ? undefined : Math.round((num + 0.00001) * 100) / 100;
  };
};

export const scFloatFormatterFactoryConstant = [
  'attrs',
  '$filter',
  '$locale',
  function(attrs, $filter, $locale) {
    return function(numValue) {
      return numValue === undefined || numValue === null
        ? undefined
        : $filter('number')(numValue).replace(
            new RegExp($locale.NUMBER_FORMATS.GROUP_SEP, 'g'),
            ''
          );
    };
  }
];

export const scFloatDirective = [
  'numberDirectiveFactory',
  function(numberDirectiveFactory) {
    return numberDirectiveFactory(
      numberDirectiveTemplateUrl,
      'scFloatParserFactory',
      'scFloatFormatterFactory',
      true
    );
  }
];
