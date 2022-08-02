// Usage: <sc-field type="int" model="" text="" validations=""></sc-field>

import _ from 'lodash';

function FieldDirective($compile, $parse) {
  function FieldCompile(tElement, tAttrs) {
    var type = tAttrs.type,
      customType = tAttrs.customType,
      model = tAttrs.ngModel,
      text = tAttrs.l10nText,
      validations = tAttrs.validations,
      cssClass = tAttrs['class'],
      hasValidations = false,
      //TODO posible default validations
      //should be different for each field directive
      currentValidations = ['min', 'max', 'positive'],
      fieldName,
      containerElement,
      labelElement,
      dirElement,
      validationsList = {
        'ng-required': 'required',
        'ng-pattern': 'pattern',
        minlength: 'minlength',
        maxlength: 'maxlength',
        min: 'min',
        max: 'max',
        positive: 'positive',
        'eumis-max-blob-size': 'eumisMaxBlobSize'
      },
      mergedValidation,
      defaultValidation;

    if (type === 'file') {
      currentValidations.push('eumis-max-blob-size');
    }

    if (!tAttrs.name) {
      fieldName = _.includes(model, '.') ? _.last(model.split('.')) : model;
    } else {
      fieldName = tAttrs.name;
    }

    if (customType !== undefined) {
      dirElement = $('<' + customType + '></' + customType + '>');
    } else {
      dirElement = $('<sc-' + type + '></sc-' + type + '>');
    }

    dirElement.attr('ng-model', model).attr('name', fieldName);

    if (!tAttrs.ngReadonly) {
      dirElement.attr('ng-readonly', 'form.$readonly');
    }

    if (tAttrs.inputWidth) {
      dirElement.css('width', tAttrs.inputWidth);
    }

    if (tAttrs.inputClass) {
      dirElement.addClass(tAttrs.inputClass);
    }

    _.forOwn(tAttrs, function(val, key) {
      if (key === 'type' || key === 'ngModel' || key === '$$element' || key === '$attr') {
        return;
      }

      tElement.removeAttr(tAttrs.$attr[key]);

      if (key === 'l10nText' || key === 'validations' || key === 'class' || key === 'ngShow') {
        return;
      }

      if (val) {
        dirElement.attr(tAttrs.$attr[key], val);
      } else {
        dirElement.attr(tAttrs.$attr[key], true);
      }

      if (_.has(validationsList, tAttrs.$attr[key])) {
        hasValidations = true;
        currentValidations.push(tAttrs.$attr[key]);
      }
    });

    containerElement = $('<div></div>').addClass('form-group ' + cssClass);
    labelElement = $('<label></label>')
      .addClass('control-label')
      .css('width', '100%')
      .appendTo(containerElement);
    $('<span></span>')
      .attr('l10n-text', text)
      .appendTo(labelElement);
    labelElement.append(' ');

    if (_.indexOf(currentValidations, 'ng-required') > -1) {
      $(
        '<span class="fa fa-exclamation"' +
          'ng-show="!form.$submitted && !form.$readonly && form[\'' +
          fieldName +
          '\'].$error.required"></span>'
      ).appendTo(labelElement);
    }

    if (tAttrs.ngShow) {
      containerElement.attr(tAttrs.$attr.ngShow, tAttrs.ngShow);
    }

    containerElement.attr('sc-has-error', fieldName);
    if (validations) {
      mergedValidation = $parse(validations)(null);
      if (hasValidations) {
        _(currentValidations).forEach(function(valName) {
          if (!_.has(mergedValidation, validationsList[valName])) {
            mergedValidation[validationsList[valName]] = 'default';
          }
        });
      }

      $('<sc-validation-error></sc-validation-error>')
        .attr('field-name', fieldName)
        .attr('validations', JSON.stringify(mergedValidation))
        .appendTo(labelElement);
    } else if (hasValidations) {
      defaultValidation = {};
      _(currentValidations).forEach(function(valName) {
        defaultValidation[validationsList[valName]] = 'default';
      });

      $('<sc-validation-error></sc-validation-error>')
        .attr('field-name', fieldName)
        .attr('validations', JSON.stringify(defaultValidation))
        .appendTo(labelElement);
    }

    containerElement.append(dirElement);
    tElement.append(containerElement);

    return {
      pre: function preLink(scope, iElement) {
        $compile(iElement.children())(scope);
      }
    };
  }

  return {
    restrict: 'E',
    terminal: true,
    priority: 120,
    compile: FieldCompile
  };
}

FieldDirective.$inject = ['$compile', '$parse'];

export { FieldDirective as scFieldDirective };
