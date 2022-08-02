/*global angular*/

(function (angular) {
    'use strict';
    
    angular.module('modulePaperAttachedDocuments', ['scaffolding'])
        .controller('controllerPaperAttachedDocuments',
            ['$scope', '$window', '$filter',
                function ($scope, $window, $filter) {
                    $scope.init = function (globalKey, parentKey) {
                        $scope.items = $window[globalKey][parentKey].items;
                        $scope.hasUniqueIds = $window[globalKey][parentKey].hasUniqueIds;
                        $scope.hasValidCount = $window[globalKey][parentKey].hasValidCount;
                        $scope.maxDocuments = $window[globalKey]['constants'].PaperDocumentsMaxCount;
                    };

                    $scope.addItem = function () {
                        $scope.items.push({
                            IsTypeValid: true,
                            IsDescriptionValid: true,
                            IsRequired: false
                        });
                    };

                    $scope.delItem = function (item) {
                        $scope.items.splice($scope.items.indexOf(item), 1);
                    };
                    
                    $scope.$evalAsync(function () {
                        $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                    });
                }]);
}(angular));
