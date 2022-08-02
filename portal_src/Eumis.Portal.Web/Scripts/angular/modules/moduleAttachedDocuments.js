/*global angular*/

(function (angular) {
    'use strict';

    angular.module('moduleAttachedDocuments', ['scaffolding'])
        .controller('controllerAttachedDocuments',
            ['$scope', '$window',
                function ($scope, $window) {

                    $scope.init = function (globalKey, parentKey) {
                        $scope.items = $window[globalKey][parentKey].items;
                        $scope.url = $window[globalKey][parentKey].url;
                        $scope.resourcesObject = $window[globalKey][parentKey].resourcesObject;
                        $scope.hasUniqueIds = $window[globalKey][parentKey].hasUniqueIds;
                        $scope.hasValidCount = $window[globalKey][parentKey].hasValidCount;
                        $scope.maxDocuments = $window[globalKey]['constants'].AttachedDocumentsMaxCount;

                        $scope.initItems();
                    };

                    $scope.initItems = function () {
                        $scope.items.forEach(function (item) {
                            if (item.AttachedDocumentContent === undefined) {
                                item.AttachedDocumentContent = {};
                            }
                            if (item.SignatureContentCollection === undefined || item.SignatureContentCollection.length === 0) {
                                item.SignatureContentCollection = [{}];
                            }
                        });
                    };

                    $scope.addItem = function () {
                        var item = {
                            IsTypeValid: true,
                            IsDescriptionValid: true,
                            IsRequired: false,
                            AttachedDocumentContent: {
                                IsDocumentValid: true
                            },
                            SignatureContentCollection: [{
                                IsDocumentValid: true
                            }]
                        };

                        $scope.items.push(item);
                    };

                    $scope.addItemWithDate = function () {
                        var item = {
                            IsTypeValid: true,
                            IsDescriptionValid: true,
                            IsRequired: false,
                            AttachedDocumentContent: {
                                IsDocumentValid: true
                            },
                            SignatureContentCollection: [{
                                IsDocumentValid: true
                            }],
                            ActivationDate: new Date().toLocaleString('en-GB')
                        };

                        $scope.items.push(item);
                    };

                    $scope.delItem = function (item) {
                        $scope.items.splice($scope.items.indexOf(item), 1);
                    };

                    $scope.addSignature = function (item) {
                        var signature = {
                            IsDocumentValid: true
                        };

                        item.SignatureContentCollection.push(signature);
                    };

                    $scope.delSignature = function (item, signature) {
                        item.SignatureContentCollection.splice(item.SignatureContentCollection.indexOf(signature), 1);
                    };

                    $scope.$evalAsync(function () {
                        $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                    });
                }]);
}(angular));
