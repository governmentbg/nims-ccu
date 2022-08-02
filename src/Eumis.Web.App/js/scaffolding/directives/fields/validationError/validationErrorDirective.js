// Usage:
//<sc-validation-error field-name="fieldName" validations="{val1:'l10n', val2: 'default', ...}">
//</sc-validation-error>

import _ from 'lodash';
import validationErrorDirectiveTemplateUrl from './validationErrorDirective.html';

function ValidationErrorDirective(l10n, scValidationErrorConfig) {
  return {
    restrict: 'E',
    scope: {
      fieldName: '@',
      getValidations: '&validations'
    },
    templateUrl: validationErrorDirectiveTemplateUrl,
    link: function(scope, element) {
      scope.form = element.parent().controller('form');
      scope.validations = [];
      _.forOwn(scope.getValidations(), function(text, type) {
        scope.validations.push({
          type: type,
          text: l10n.get(
            text !== 'default' ? text : scValidationErrorConfig.defaultErrorTexts[type]
          )
        });
      });
    }
  };
}

ValidationErrorDirective.$inject = ['l10n', 'scValidationErrorConfig'];

export { ValidationErrorDirective as scValidationErrorDirective };

export const scValidationErrorConfigConstant = {
  defaultErrorTexts: {
    required: 'defaultErrorTexts_required',
    pattern: 'defaultErrorTexts_pattern',
    minlength: 'defaultErrorTexts_minlength',
    maxlength: 'defaultErrorTexts_maxlength',
    min: 'defaultErrorTexts_min',
    max: 'defaultErrorTexts_max',
    unique: 'defaultErrorTexts_unique',
    positive: 'defaultErrorTexts_positive',
    eumisMaxBlobSize: 'defaultErrorTexts_eumisMaxBlobSize'
  }
};
