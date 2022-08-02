/*global angular*/

(function (angular) {
    'use strict';

    angular.module('moduleElectronicDeclarations', ['scaffolding'])
        .controller('controllerElectronicDeclarations',
            ['$scope', '$window',
                function ($scope, $window) {

                    $scope.init = function (globalKey, parentKey) {
                        $scope.items = $window[globalKey][parentKey].items;
                    };

                    $scope.getDeclarationItems = function (declaration) {
                        return {
                            allowClear: true,
                            placeholder: ' ',
                            query: function (query) {
                                var data = { results: [] };

                                $.each(declaration.Items, function () {
                                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                                        data.results.push(this);
                                    }
                                });

                                query.callback(data);
                            }
                        };
                    }

                    $scope.$evalAsync(function () {
                        $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                    });
                }]);
}(angular));
