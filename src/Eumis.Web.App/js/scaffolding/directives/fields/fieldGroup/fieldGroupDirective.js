// Usage: <sc-field-group text="" validations="" field-name=""> #content </sc-field-group>

import angular from 'angular';
import _ from 'lodash';
import fieldGroupDirectiveTemplateUrl from './fieldGroupDirective.html';

function FieldGroupDirective($parse, $compile, l10n) {
  function FieldGroupCompile(tElement, tAttrs) {
    var validationElem = tElement.find('sc-validation-error'),
      reqElem = tElement.find('.required-marker'),
      label;

    if (tAttrs.validations) {
      validationElem.attr('validations', tAttrs.validations);
      validationElem.attr('field-name', tAttrs.fieldName);
      tAttrs.$set('scHasError', tAttrs.fieldName);

      if (tAttrs.validations.indexOf('required') === -1) {
        reqElem.remove();
      } else {
        reqElem.attr(
          'ng-show',
          "!form.$submitted && !form.$readonly && form['" + tAttrs.fieldName + "'].$error.required"
        );
      }
    } else {
      validationElem.remove();
      reqElem.remove();
      tElement.removeAttr('sc-has-error');
    }

    label = tElement.find('label');
    label.text(l10n.get(tAttrs.text));

    return function FieldGroupLink($scope, element, attrs, scSearch, transcludeFn) {
      var hasPreffix = false,
        hasSuffix = false;

      transcludeFn($scope, function(clone) {
        var modelFound = false,
          addons = _.map(element.find('span.input-group-addon'), function(addon) {
            return angular.element(addon);
          });

        angular.forEach(clone, function(item) {
          var elem = angular.element(item),
            tagName = elem.prop('tagName');

          if (!tagName) {
            return;
          }

          if (elem.attr('ng-model')) {
            addons[0].after(elem);
            modelFound = true;
          } else if (modelFound) {
            if (tagName === 'SC-BUTTON') {
              addons[1].prop('class', 'input-group-btn');
            }

            hasSuffix = true;
            addons[1].append(elem);
          } else {
            if (tagName === 'SC-BUTTON') {
              addons[0].prop('class', 'input-group-btn');
            }

            hasPreffix = true;
            addons[0].append(elem);
          }
        });

        if (!hasPreffix) {
          addons[0].remove();
        }

        if (!hasSuffix) {
          addons[1].remove();
        }

        $scope.$on('$destroy', function() {
          clone.remove();
        });
      });
    };
  }

  return {
    restrict: 'E',
    transclude: true,
    terminal: true,
    replace: true,
    templateUrl: fieldGroupDirectiveTemplateUrl,
    compile: FieldGroupCompile
  };
}

FieldGroupDirective.$inject = ['$parse', '$compile', 'l10n'];

export { FieldGroupDirective as scFieldGroupDirective };
