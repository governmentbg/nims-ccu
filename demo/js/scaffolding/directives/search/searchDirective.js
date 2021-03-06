// Usage: <sc-search selected-filters="" btn-classes=""></sc-search>

/*globals angular, _, $*/
(function (angular, _, $) {
  'use strict';

  function SearchDirective($timeout) {
    function SearchController ($scope) {
      var filters = {},
          //an object used as the special value of the watch expression
          //when the watched property does not exist on the object
          emptyValue = {};

      this.selectedFilters = $scope.selectedFilters;
      this.dropdownFilters = $scope.dropdownFilters = [];

      this.registerFilter = function (name, filterScope) {
        filters[name] = filterScope;
      };

      this.initialize = function () {
        _.forOwn(filters, function (filterScope, filterName) {
          var dropdownFilter = {
            label: filterScope.label,
            name: filterName,
            visible: false
          };
          $scope.dropdownFilters.push(dropdownFilter);

          $scope.$watch(
            function () {
              return $scope.selectedFilters.hasOwnProperty(filterName) ?
                $scope.selectedFilters[filterName] :
                emptyValue;
            },
            function (newVal) {
              filterScope.model = newVal === emptyValue ? null : newVal;
              dropdownFilter.visible = filterScope.label && newVal === emptyValue;
            },
            true);

          filterScope.$watch('model', function (newVal, oldVal) {
            if (newVal !== oldVal &&
              $scope.selectedFilters.hasOwnProperty(filterName)
            ) {
              $scope.selectedFilters[filterName] = newVal;
            }
          }, true);
        });
      };
    }

    function SearchCompile(tElement, tAttrs) {
      if (!tAttrs.btnClasses) {
        tAttrs.btnClasses = 'col-sm-3';
      }

      return function ($scope, element, attrs, scSearch, transcludeFn) {

        transcludeFn($scope.$parent, function (clone) {
          var buttonBlock = element.find('div.btns-block');

          angular.forEach(clone, function (elem) {
            if (angular.element(elem).hasClass('btn-div')) {
              buttonBlock.append(elem);
            } else {
              buttonBlock.before(elem);
            }
          });

          $scope.$on('$destroy', function () {
            clone.remove();
          });
        });

        scSearch.initialize();

        //delaying the focus, because using a templateUrl leads to
        //an asynchronous resolve which delays the inner directives' linking
        $timeout(function () {
          if ($scope.selectedFilters !== null) {
            $('form[selected-filters] input').first().focus();
          }
        },
        0);
      };
    }

    return {
      restrict: 'E',
      transclude: true,
      replace: true,
      templateUrl: 'js/scaffolding/directives/search/searchDirective.html',
      scope: {
        selectedFilters: '=',
        btnClasses: '@'
      },
      controller: ['$scope', SearchController],
      compile: SearchCompile
    };
  }

  SearchDirective.inject = ['$timeout'];

  angular.module('scaffolding').directive('scSearch', SearchDirective);
}(angular, _, $));
