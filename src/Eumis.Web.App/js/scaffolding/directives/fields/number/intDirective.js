// Usage: <sc-int ng-model="<model_name>"></sc-int>
import numberDirectiveTemplateUrl from './numberDirective.html';

export const scIntDirective = [
  'numberDirectiveFactory',
  function(numberDirectiveFactory) {
    return numberDirectiveFactory(
      numberDirectiveTemplateUrl,
      function() {
        var minInt32 = -Math.pow(2, 31),
          maxInt32 = -minInt32 - 1;

        function truncateToInt32(i) {
          return Math.min(Math.max(i, minInt32), maxInt32);
        }

        return function(strValue) {
          var num = parseInt(strValue, 10);
          return isNaN(num) ? undefined : truncateToInt32(num);
        };
      },
      [
        'attrs',
        function(attrs) {
          function padLeft(str, padding, length) {
            while (str.length < length) {
              str = padding + str;
            }

            return str;
          }

          return function(numValue) {
            numValue = numValue === undefined || numValue === null ? undefined : numValue;
            if (attrs.padding && numValue !== null && numValue !== undefined) {
              numValue = padLeft(numValue.toString(), '0', attrs.padding);
            }
            return numValue;
          };
        }
      ],
      true
    );
  }
];
