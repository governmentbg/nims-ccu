export const numberValidationsConstant = {
  min: [
    'attrs',
    'ngModel',
    'def',
    function(attrs, ngModel, def) {
      return function(value) {
        var min;
        if (def !== undefined || attrs.min) {
          if (attrs.min !== undefined) {
            min = parseFloat(attrs.min);
          } else {
            min = def;
          }

          if (!ngModel.$isEmpty(value) && value < min) {
            ngModel.$setValidity('min', false);
          } else {
            ngModel.$setValidity('min', true);
          }
        }

        return value;
      };
    }
  ],
  max: [
    'attrs',
    'ngModel',
    'def',
    function(attrs, ngModel, def) {
      return function(value) {
        var max;
        if (def !== undefined || attrs.max) {
          if (attrs.max !== undefined) {
            max = parseFloat(attrs.max);
          } else {
            max = def;
          }

          if (!ngModel.$isEmpty(value) && value > max) {
            ngModel.$setValidity('max', false);
          } else {
            ngModel.$setValidity('max', true);
          }
        }

        return value;
      };
    }
  ],
  positive: [
    'attrs',
    'ngModel',
    'def',
    function(attrs, ngModel, def) {
      return function(value) {
        if (def === true || attrs.positive === 'true') {
          if (!ngModel.$isEmpty(value) && value <= 0) {
            ngModel.$setValidity('positive', false);
          } else {
            ngModel.$setValidity('positive', true);
          }
        }

        return value;
      };
    }
  ]
};
