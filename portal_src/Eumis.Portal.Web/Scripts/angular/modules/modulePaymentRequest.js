angular.module('modulePaymentRequest', ['scaffolding'])
    .factory('PaymentRequest', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].PaymentRequest;
    }])
    .factory('AttachedDocumentsInfo', ['$window', function ($window) {
        return {
            resourcesAttachedDocuments: $window['_eumis_options'].resourcesAttachedDocuments,
            blobUrl: $window['_eumis_options'].blobUrl
        };
    }])
    .controller('controllerMainPaymentRequest',
            ['$scope', '$filter', '$timeout', '$window', 'PaymentRequest', 'appcontext',
            function ($scope, $filter, $timeout, $window, PaymentRequest, appcontext) {
                $scope.globalKey = '_eumis_options';

                $scope.PaymentRequest = PaymentRequest;

                $scope.acceptances = $window[$scope.globalKey].acceptances;

                $scope.getFinanceAmountUrl = $window[$scope.globalKey].getFinanceAmountUrl;
                $scope.getIncomesUrl = $window[$scope.globalKey].getIncomesUrl;

                $scope.$on('paymentRequestActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SavePaymentRequest', $scope.PaymentRequest, {});
                    }
                });

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
    .controller('controllerBasicData',
        ['$scope', '$filter', '$timeout', '$window', '$resource', 'PaymentRequest',
        function ($scope, $filter, $timeout, $window, $resource, PaymentRequest) {

            $scope.model = PaymentRequest.BasicData;

            // GET FROM FINANCE REPORT LOGIC

            var refreshSearchState = function () {
                $timeout(function () {
                    switchSearchDivs(1); // search
                }, 4000);
            }

            var loadAmount = function (result) {
                $scope.model.FinanceReportAmount = result.amount;
            }

            $scope.isSearch = true;
            $scope.isPleaseWait = false;
            $scope.isOK = false;
            $scope.isNoResult = false;

            // state: 1: search; 2: pleaseWait; 3: OK; 4: noResult;
            var switchSearchDivs = function (state) {
                $scope.isSearch = false;
                $scope.isPleaseWait = false;
                $scope.isOK = false;
                $scope.isNoResult = false;

                $timeout(function () {
                    switch (state) {
                        case 1:
                            $scope.isSearch = true;
                            break;
                        case 2:
                            $scope.isPleaseWait = true;
                            break;
                        case 3:
                            $scope.isOK = true;
                            break;
                        case 4:
                            $scope.isNoResult = true;
                            break;
                    }
                }, 50);

            };

            $scope.searchFinanceAmount = function () {
                switchSearchDivs(2); // pleaseWait

                $timeout(function () {
                    $resource($scope.getFinanceAmountUrl + '/:params')
                        .save({
                            contractGid: PaymentRequest.contractGid,
                            packageGid: PaymentRequest.packageGid
                        })
                        .$promise
                        .then(function (result) {
                            if (!result) {
                                switchSearchDivs(4); // noResult
                            } else {
                                loadAmount(result);
                                $timeout(function () {
                                    switchSearchDivs(3); // OK
                                }, 500);
                            }
                        }, function (error) {
                            switchSearchDivs(4); // noResult
                        })
                        .finally(function () {
                            // Always execute this on both error and success
                            refreshSearchState();
                        });
                }, 200);
            }

            // END GET FROM FINANCE REPORT LOGIC

            // GET INCOMES FROM FINANCE REPORT LOGIC

            var refreshIncomesSearchState = function () {
                $timeout(function () {
                    switchIncomesSearchDivs(1); // search
                }, 4000);
            }

            var loadIncomesAmount = function (result) {
                $scope.model.AdditionalIncome = result.amount;
            }

            $scope.isIncomesSearch = true;
            $scope.isIncomesPleaseWait = false;
            $scope.isIncomesOK = false;
            $scope.isIncomesNoResult = false;

            // state: 1: search; 2: pleaseWait; 3: OK; 4: noResult;
            var switchIncomesSearchDivs = function (state) {
                $scope.isIncomesSearch = false;
                $scope.isIncomesPleaseWait = false;
                $scope.isIncomesOK = false;
                $scope.isIncomesNoResult = false;

                $timeout(function () {
                    switch (state) {
                        case 1:
                            $scope.isIncomesSearch = true;
                            break;
                        case 2:
                            $scope.isIncomesPleaseWait = true;
                            break;
                        case 3:
                            $scope.isIncomesOK = true;
                            break;
                        case 4:
                            $scope.isIncomesNoResult = true;
                            break;
                    }
                }, 50);

            };

            $scope.searchIncomesAmount = function () {
                switchIncomesSearchDivs(2); // pleaseWait

                $timeout(function () {
                    $resource($scope.getIncomesUrl + '/:params')
                        .save({
                            contractGid: PaymentRequest.contractGid,
                            packageGid: PaymentRequest.packageGid
                        })
                        .$promise
                        .then(function (result) {
                            if (!result) {
                                switchIncomesSearchDivs(4); // noResult
                            } else {
                                loadIncomesAmount(result);
                                $timeout(function () {
                                    switchIncomesSearchDivs(3); // OK
                                }, 500);
                            }
                        }, function (error) {
                            switchIncomesSearchDivs(4); // noResult
                        })
                        .finally(function () {
                            // Always execute this on both error and success
                            refreshIncomesSearchState();
                        });
                }, 200);
            }

            // END GET FROM FINANCE REPORT LOGIC

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
        .controller('controllerAttachedDocuments',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'PaymentRequest', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, rfc4122, PaymentRequest, AttachedDocumentsInfo) {

            $scope.items = PaymentRequest.AttachedDocuments.AttachedDocumentCollection;
            $scope.hasValidCount = PaymentRequest.AttachedDocuments.HasValidCount;
            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;

            $scope.versionNum = $window['_eumis_options'].PaymentRequest.docNumber;
            $scope.versionSubNum = $window['_eumis_options'].PaymentRequest.docSubNumber;

            $scope.items.forEach(function (item) {
                if (item.AttachedDocumentContent == undefined) {
                    item.AttachedDocumentContent = {};
                }
            });

            $scope.addItem = function () {
                var item = {
                    IsTypeValid: true,
                    IsDescriptionValid: true,
                    VersionNum: $scope.versionNum,
                    VersionSubNum: $scope.versionSubNum,
                    RegNumber: $scope.versionNum + '.' + $scope.versionSubNum,
                    AttachedDocumentContent: {
                        IsDocumentValid: true
                    }
                };

                $scope.items.push(item);
            };

            $scope.isCurrentVersion = function (item) {
                if (!item.VersionNum || !item.VersionSubNum) {
                    return true;
                }
                else if ($scope.versionSubNum === '1') {
                    return true;
                }

                return item.VersionNum === $scope.versionNum && item.VersionSubNum === $scope.versionSubNum;
            };

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }]);

