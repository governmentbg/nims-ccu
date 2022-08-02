import _ from 'lodash';

export const numberDirectiveFactoryFactory = [
  '$injector',
  'numberValidations',
  function($injector, numberValidations) {
    function transferAttributes(fromElement, toElement, attrs) {
      _.forEach(attrs, function(attr) {
        var attrValue = fromElement.attr(attr);
        if (attrValue !== undefined) {
          fromElement.removeAttr(attr);
          toElement.attr(attr, attrValue);
        }
      });
    }

    return function(templateUrl, parserFactory, formatterFactory, isLeftAligned, validations) {
      function postLink(scope, element, attrs, ngModel) {
        var input, validationFn;

        if (!ngModel) {
          return;
        }

        if (element.is('input')) {
          input = element;
        } else {
          input = element.find('input');
        }

        if (!isLeftAligned) {
          input.addClass('text-right');
        }

        function invokeFn(fn, additionalLocals) {
          if (typeof fn === 'string') {
            fn = $injector.get(fn);
          }

          return $injector.invoke(
            fn,
            undefined,
            _.assign({ attrs: attrs, ngModel: ngModel }, additionalLocals)
          );
        }

        ngModel.$parsers.push(invokeFn(parserFactory));
        ngModel.$formatters.push(invokeFn(formatterFactory));

        //always add the validations
        validations = _.assign({ min: undefined, max: undefined, positive: false }, validations);

        _.forOwn(validations, function(value, key) {
          validationFn = invokeFn(numberValidations[key], { def: value });
          ngModel.$parsers.push(validationFn);
          ngModel.$formatters.push(validationFn);
        });

        input.on('blur', function onBlurFn() {
          var formatters = ngModel.$formatters,
            idx = formatters.length,
            value = ngModel.$modelValue;

          while (idx--) {
            value = formatters[idx](value);
          }

          if (ngModel.$viewValue !== value) {
            ngModel.$viewValue = value;
            ngModel.$render();
          }
        });

        element.bind('$destroy', function() {
          input.unbind('blur');
        });
      }

      function compile(tElement) {
        if (!tElement.is('input')) {
          //if the input is not the root template element
          //transfer the input specific attributes to the input
          transferAttributes(tElement, tElement.find('input'), [
            'ng-model',
            'ng-value',
            'ng-readonly',
            'ng-required',
            'ng-pattern',
            'minlength',
            'maxlength',
            'min',
            'max'
          ]);
        }

        return postLink;
      }

      return {
        priority: 110,
        restrict: 'E',
        replace: true,
        require: '?ngModel',
        templateUrl: templateUrl,
        compile: compile
      };
    };
  }
];
