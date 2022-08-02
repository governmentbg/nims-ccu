﻿// Usage:
// <sc-nomenclature alias="" mode="id|object" params="" multiple ng-model="">
// </sc-nomenclature>

import angular from 'angular';
import _ from 'lodash';
import Select2 from 'select2';
import nomenclatureDirectiveTemplateUrl from './nomenclatureDirective.html';

function NomenclatureDirective(
  $filter,
  $parse,
  $exceptionHandler,
  $window,
  Nomenclatures,
  scNomenclatureConfig
) {
  function preLink(scope, iElement, iAttrs, ngModel) {
    var idProp = scNomenclatureConfig.idProp,
      nameProp = scNomenclatureConfig.nameProp,
      aliasProp = scNomenclatureConfig.aliasProp,
      alias = scope.alias(),
      isMultiple = angular.isDefined(iAttrs.multiple),
      initSelectionFunc,
      nomObjFunc,
      paramsFunc,
      createQuery,
      createValQuery,
      valProp;

    if (!alias) {
      throw new Error('sc-nomenclature alias not specified!');
    }

    if (iAttrs.ngDisabled) {
      scope.ngDisabled = $parse(iAttrs.ngDisabled);
    }

    if (iAttrs.params) {
      paramsFunc = $parse(iAttrs.params);
      createQuery = function(params) {
        return _.assign({}, { alias: alias }, params, paramsFunc(scope.$parent));
      };

      scope.$parent.$watch(
        function() {
          return paramsFunc(scope.$parent);
        },
        function(newVal, oldVal) {
          //skip initialization
          if (newVal !== oldVal && ngModel.$viewValue && ngModel.$viewValue[nameProp]) {
            var term = ngModel.$viewValue[nameProp];

            Nomenclatures.query(createQuery({ term: term })).$promise.then(function(result) {
              if (result.length === 0) {
                ngModel.$setViewValue(undefined);
                ngModel.$render();
              }
            });
          }
        },
        true
      );
    } else {
      createQuery = function(params) {
        return _.assign({}, { alias: alias }, params);
      };
    }

    if (iAttrs.mode === 'id' || iAttrs.mode === 'alias') {
      if (iAttrs.mode === 'id') {
        valProp = idProp;
        createValQuery = function(isMultiple, val) {
          if (isMultiple) {
            return createQuery({ ids: val });
          } else {
            return createQuery({ id: val });
          }
        };
      } else if (iAttrs.mode === 'alias') {
        valProp = aliasProp;
        createValQuery = function(isMultiple, val) {
          if (isMultiple) {
            return createQuery({ valueAliases: val });
          } else {
            return createQuery({ valueAlias: val });
          }
        };
      }

      if (iAttrs.nomObj) {
        nomObjFunc = $parse(iAttrs.nomObj);
      }

      //make the value a string when dealing with boolean keys
      ngModel.$formatters.unshift(function(modelValue) {
        if (typeof modelValue === 'boolean') {
          modelValue = modelValue.toString();
        }

        return modelValue;
      });

      ngModel.$parsers.push(function(viewValue) {
        if (nomObjFunc && nomObjFunc.assign) {
          nomObjFunc.assign(scope.$parent, viewValue);
        }

        if (viewValue === null || viewValue === undefined) {
          return viewValue;
        } else if (_.isArray(viewValue)) {
          return _.map(viewValue, function(item) {
            return item[valProp];
          });
        } else {
          return viewValue[valProp];
        }
      });

      initSelectionFunc = function(element, callback) {
        var val = element.select2('val'),
          resultPromise;

        resultPromise = Nomenclatures[isMultiple ? 'query' : 'get'](createValQuery(isMultiple, val))
          .$promise;

        resultPromise.then(
          function(result) {
            // check to see if the element was detached while we were waiting for the response
            if (element.closest($window.document.documentElement).length === 0) {
              return;
            }

            if (nomObjFunc && nomObjFunc.assign) {
              nomObjFunc.assign(scope.$parent, result);
            }

            callback(result);
          },
          function(error) {
            $exceptionHandler(error);
          }
        );
      };
    }

    scope.select2Options = {
      minimumResultsForSearch: 20,
      multiple: isMultiple,
      allowClear: true,
      placeholder: ' ', //required for allowClear to work
      query: function(query) {
        var pageSize = scNomenclatureConfig.pageSize,
          page = query.page - 1;
        Nomenclatures.query(
          createQuery({ term: query.term, offset: page * pageSize, limit: pageSize })
        ).$promise.then(
          function(result) {
            query.callback({ results: result, more: result.length === pageSize });
          },
          function(error) {
            $exceptionHandler(error);
          }
        );
      },
      initSelection: initSelectionFunc,
      formatResult: function(result, container, query, escapeMarkup) {
        var markup = [];
        Select2.util.markMatch(result[nameProp], query.term, markup, escapeMarkup);
        return markup.join('');
      },
      formatSelection: function(data) {
        return data ? Select2.util.escapeMarkup(data[nameProp]) : undefined;
      },
      id: function(obj) {
        var id = obj[idProp];

        //make the value a string when dealing with boolean keys
        if (typeof id === 'boolean') {
          id = id.toString();
        }

        return id;
      }
    };
  }

  return {
    priority: 110,
    restrict: 'E',
    replace: true,
    templateUrl: nomenclatureDirectiveTemplateUrl,
    scope: {
      alias: '&'
    },
    require: '?ngModel',
    link: { pre: preLink }
  };
}

NomenclatureDirective.$inject = [
  '$filter',
  '$parse',
  '$exceptionHandler',
  '$window',
  'Nomenclatures',
  'scNomenclatureConfig'
];

export { NomenclatureDirective as scNomenclatureDirective };

export const scNomenclatureConfigConstant = {
  idProp: 'nomValueId',
  nameProp: 'name',
  aliasProp: 'alias',
  pageSize: 20
};
