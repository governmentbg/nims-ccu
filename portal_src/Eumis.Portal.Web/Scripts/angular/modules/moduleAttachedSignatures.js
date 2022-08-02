/*global angular*/

(function (angular) {
    'use strict';

    angular.module('moduleAttachedSignatures', ['scaffolding', 'ui.jq'])
        .controller('controllerAttachedSignatures',
            ['$scope', '$window', '$q', '$http',
                function ($scope, $window, $q, $http) {

                    $scope.init = function (globalKey, parentKey) {
                        $scope.items = $window[globalKey][parentKey].items;
                        $scope.resourcesObject = $window[globalKey][parentKey].resourcesObject;
                        $scope.url = $window[globalKey][parentKey].url;

                        $scope.initItems();
                    };

                    $scope.initItems = function () {
                        if ($scope.items.length == 0) {
                            $scope.addItem();
                        }
                        
                        $scope.items.forEach(function (item) {
                            $scope.updateItem(item);
                        });;
                    };

                    $scope.updateItem = function (item) {

                        item.progress = 0;
                        item.showError = false;
                        item.showUploader = (item.fileKey || '').length <= 0;

                        item.updateUI;
                        item.add = function (e, data) {
                            item.fileName = data.files[0].name;

                            item.updateUI = $q.defer();
                            item.updateUI.promise.then(function (data) {
                                item.fileKey = data.result.fileKey;
                                item.serialNumber = data.result.serialNumber,
                                item.effectiveDate = data.result.effectiveDate,
                                item.expirationDate = data.result.expirationDate,
                                item.issuer = data.result.issuer,
                                item.subject = data.result.subject
                                item.showUploader = (data.result.fileKey || '').length <= 0;
                            });

                            data.submit();
                        };

                        item.done = function (e, data) {
                            item.updateUI.resolve(data);
                        };

                        item.error = function (e, data) {
                            $scope.$apply(function () {
                                item.showError = true;
                                item.errorMessage = $scope.resourcesObject.InvalidSignatureMessage;
                            });
                        }

                        item.progressall = function (e, data) {
                            $scope.$apply(function () {
                                item.progress = parseInt(data.loaded / data.total * 100, 10);
                            });
                        };

                        item.options = {
                            dataType: "json",
                            url: $scope.url,
                            add: item.add,
                            done: item.done,
                            error: item.error,
                            progressall: item.progressall
                        };

                        return item;
                    };

                    $scope.addItem = function () {
                        $scope.items.push($scope.updateItem({}));
                    };

                    $scope.delItem = function (item) {
                        if (item.showError) {
                            $scope.items.splice($scope.items.indexOf(item), 1);
                        } else {
                            if (item.fileKey) {
                                $http.post('/submit/deleteSignature', { key: item.fileKey }).
                                    success(function(data, status, headers, config) {
                                        $scope.items.splice($scope.items.indexOf(item), 1);
                                    }).
                                    error(function(data, status, headers, config) {
                                        item.showError = true;
                                        item.errorMessage = 'Възникна грешка.';
                                    });
                            } else {
                                $scope.items.splice($scope.items.indexOf(item), 1);
                            }
                        }
                        if ($scope.items.length == 0) {
                            $scope.addItem();
                        }
                    };

                    $scope.$evalAsync(function () {
                        $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                    });
                }]);
}(angular));
