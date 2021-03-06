// Usage: <sc-date ng-model="<model_name>" />

import dateDirectiveTemplateUrl from './dateDirective.html';
import 'bootstrap-datepicker/js/bootstrap-datepicker';

function DateDirective(scDateConfig, moment) {
  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    require: '?ngModel',
    scope: {},
    templateUrl: dateDirectiveTemplateUrl,
    link: function(scope, element, attrs, ngModel) {
      if (!ngModel) {
        return;
      }
      var input = element.children('input'),
        span = element.children('span'),
        changingDate = false;

      span.datepicker({
        autoclose: true,
        format: scDateConfig.datepickerFormat,
        language: 'bg',
        weekStart: 1
      });

      attrs.$observe('readonly', function(value) {
        scope.isReadonly = !!value;
        if (value) {
          span.data('datepicker')._detachEvents();
        } else {
          span.data('datepicker')._attachEvents();
        }
      });

      ngModel.$render = function() {
        var m = moment(ngModel.$viewValue),
          pickerValue,
          inputValue;

        if (m.isValid() && ngModel.$viewValue) {
          pickerValue = m.startOf('day').toDate();
          inputValue = m.format(scDateConfig.dateDisplayFormat);
        } else {
          pickerValue = undefined;
          inputValue = undefined;
        }

        changingDate = true;
        span.datepicker('setDate', pickerValue);
        changingDate = false;
        input.val(inputValue);
      };

      function changeDate(m) {
        if (changingDate) {
          return;
        }

        scope.$apply(function() {
          if (m && m.isValid()) {
            ngModel.$setViewValue(m.startOf('day').format(scDateConfig.dateModelFormat));
          } else {
            ngModel.$setViewValue(undefined);
          }
          ngModel.$render();
        });
      }

      function changeDateOnSelect(ev) {
        ev.preventDefault();
        ev.stopPropagation();

        changeDate(ev.date && moment(ev.date));
      }

      function changeDateOnInput(ev) {
        ev.preventDefault();
        ev.stopPropagation();

        changeDate(moment(ev.target.value, scDateConfig.dateParseFormats));
      }

      span.on('changeDate', changeDateOnSelect);
      input.on('change', changeDateOnInput);

      element.bind('$destroy', function() {
        input.off('change', changeDateOnInput);
        span.off('changeDate', changeDateOnSelect);
        span.datepicker('remove');
      });
    }
  };
}
DateDirective.$inject = ['scDateConfig', 'moment'];

export { DateDirective as scDateDirective };

export const scDateConfigConstant = {
  dateParseFormats: ['DD.MM.YYYY', 'DD_MM_YYYY', 'DD-MM-YYYY', 'DD/MM/YYYY', 'DD\\MM\\YYYY'],
  dateDisplayFormat: 'DD.MM.YYYY',
  dateModelFormat: 'YYYY-MM-DDTHH:mm:ss',
  datepickerFormat: 'dd.mm.yyyy'
};
