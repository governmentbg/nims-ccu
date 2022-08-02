// Usage:
// <sc-nomenclature sc-alias="" ng-model="">
// </sc-nomenclature>

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding')
        .constant('scNomenclatureConfig', {
            pageSize: 20
        })
        .factory('nomenclatures', ['$resource', '$window', function ($resource, $window) {
            var route = $window['_eumis_options']['session'];
            var culture = $window['_eumis_options']['currentCulture'];
            return $resource('/' + culture + '/' + route + '/nomenclature/:alias/:params');
        }])
        .directive('scNomenclature',
                    ['nomenclatures', 'scNomenclatureConfig',
            function (nomenclatures, scNomenclatureConfig) {
                return {
                    restrict: 'E',
                    replace: true,
                    template: '<input type="hidden" ui-select2="select2Options" />',
                    scope: {
                        scAlias: '@',
                        scParentId: '='
                    },
                    require: 'ngModel',
                    link: {
                        pre: function (scope, elment, attrs, ngModel) {
                            var isMultiple = angular.isDefined(attrs.multiple);
                            var hasParent = angular.isDefined(attrs.scParentId);

                            if (hasParent) {
                                scope.$watch('scParentId', function (newVal, oldVal) {
                                    if (newVal !== oldVal && oldVal) {
                                        if (ngModel.$modelValue != null) {
                                            if (isMultiple) {
                                                if (ngModel.$modelValue.length > 0) {
                                                    // TODO: Check each value
                                                    ngModel.$setViewValue({});
                                                    ngModel.$render();
                                                }
                                            }
                                            else {
                                                if (scope.scParentId != ngModel.$modelValue.parentId
                                                    || !angular.isDefined(ngModel.$modelValue.parentId)) {
                                                    ngModel.$setViewValue({ id: "", text: "" });
                                                    ngModel.$render();
                                                }
                                            }
                                        }
                                    }
                                });
                            }

                            scope.select2Options = {
                                multiple: isMultiple,
                                allowClear: true,
                                placeholder: ' ', //required for allowClear to work
                                // dropdownAutoWidth: true,
                                query: function (query) {
                                    var pageSize = scNomenclatureConfig.pageSize,
                                        page = query.page - 1,
                                        queryObj = { alias: scope.scAlias, term: query.term, offset: page * pageSize, limit: pageSize };

                                    if (hasParent) {
                                        queryObj.parentId = scope.scParentId;
                                    }

                                    nomenclatures
                                      .query(queryObj)
                                      .$promise
                                      .then(function (result) {
                                          query.callback({ results: result, more: result.length === pageSize });
                                      }, function (error) {
                                          console.log(error);
                                      });
                                }
                            };
                        }
                    }
                }
            }]);
}(angular));
