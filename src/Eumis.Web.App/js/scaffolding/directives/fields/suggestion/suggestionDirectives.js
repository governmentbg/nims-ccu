// Usage: <sc-text[area]-suggestion ng-model="<model_name>" alias="<suggestions alias>">
//        </sc-text[area]-suggestion>

import _ from 'lodash';
import textareaSuggestionDirectiveTemplateUrl from './textareaSuggestionDirective.html';
import textSuggestionDirectiveTemplateUrl from './textSuggestionDirective.html';
import 'typeahead.js/dist/typeahead.jquery';

function createSuggestionDirective(templateUrl) {
  function SuggestionDirective($http, $parse) {
    var substringMatcher = function(alias, paramsFn) {
      return function findMatches(term, cb) {
        var params = { term: term };
        if (paramsFn !== null) {
          params = _.assign({ term: term }, paramsFn());
        }
        return $http({
          method: 'GET',
          url: 'api/suggestions/' + alias,
          params: params
        }).then(function(result) {
          return cb(result.data);
        });
      };
    };
    return {
      priority: 110,
      restrict: 'E',
      replace: true,
      require: '?ngModel',
      scope: {
        alias: '&'
      },
      templateUrl: templateUrl,
      link: function(scope, element, attrs, ngModel) {
        if (!ngModel) {
          return;
        }
        var input = element.children('input,textarea'),
          alias = scope.alias();

        input.attr('rows', attrs.rows);

        var paramsFn = null;
        if (attrs.params) {
          paramsFn = _.bind($parse(attrs.params), undefined, scope.$parent);
        }

        input
          .typeahead(
            {
              hint: false,
              highlight: true
            },
            {
              name: 'values',
              displayKey: _.identity,
              source: substringMatcher(alias, paramsFn)
            }
          )
          .on('typeahead:selected', function(ev, suggestion) {
            scope.$apply(function() {
              ngModel.$setViewValue(suggestion);
            });
          });

        if (input.is('input')) {
          $(element.find('span.twitter-typeahead')[0]).css('display', 'table-cell');
        }

        ngModel.$render = function() {
          input.typeahead('val', ngModel.$viewValue);
        };

        input.change(function() {
          scope.$apply(function() {
            ngModel.$setViewValue(input.typeahead('val'));
          });
        });

        attrs.$observe('readonly', function(value) {
          scope.isReadonly = !!value;
        });

        scope.openTypeahead = function() {
          if (scope.isReadonly) {
            return;
          }
          input.typeahead('open');
          input.focus();
        };

        element.bind('$destroy', function() {
          input.typeahead('destroy');
        });
      }
    };
  }
  SuggestionDirective.$inject = ['$http', '$parse'];

  return SuggestionDirective;
}

export const scTextSuggestionDirective = createSuggestionDirective(
  textSuggestionDirectiveTemplateUrl
);
export const scTextareaSuggestionDirective = createSuggestionDirective(
  textareaSuggestionDirectiveTemplateUrl
);
