// Usage: <sc-money ng-model="<model_name>"></sc-money>
import numberDirectiveTemplateUrl from './numberDirective.html';

export const scMoneyDirective = [
  'numberDirectiveFactory',
  function(numberDirectiveFactory) {
    return numberDirectiveFactory(
      numberDirectiveTemplateUrl,
      function() {
        return function(strValue) {
          var str = (strValue || '').replace(/\s/g, ''),
            num = parseFloat((str || '').replace(',', '.'));
          return isNaN(num) ? undefined : Math.round((num + 0.00001) * 100);
        };
      },
      function() {
        return function(numValue) {
          if (numValue === undefined || numValue === null) {
            return undefined;
          } else {
            var num = (numValue / 100).toFixed(2);
            num = num.replace('.', ',');
            return num.replace(/\B(?=(\d{3})+(?!\d))/g, ' ');
          }
        };
      },
      false,
      {
        min: 0
      }
    );
  }
];
