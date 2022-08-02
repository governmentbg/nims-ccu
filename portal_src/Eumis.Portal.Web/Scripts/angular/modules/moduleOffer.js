angular.module('moduleOffer', ['scaffolding'])
    .factory('Offer', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].Offer;
    }])
    .factory('CompanyInfo', ['$window', function ($window) {
        return {
            resourcesObjectCompany: $window['_eumis_options'].resourcesObjectCompany
        };
    }])
    .factory('AttachedDocumentsInfo', ['$window', function ($window) {
        return {
            resourcesAttachedDocuments: $window['_eumis_options'].resourcesAttachedDocuments,
            blobUrl: $window['_eumis_options'].blobUrl
        };
    }])
    .controller('controllerMainOffer',
            ['$scope', '$filter', '$timeout', '$window', 'Offer', 'CompanyInfo', 'appcontext', 'copyAddress',
            function ($scope, $filter, $timeout, $window, Offer, CompanyInfo, appcontext, copyAddress) {
                $scope.globalKey = '_eumis_options';

                $scope.Offer = Offer;

                $scope.CompanyInfo = CompanyInfo;

                $scope.acceptances = $window[$scope.globalKey].acceptances;

                $scope.copyAddress = function(company)  {
                    return copyAddress.copySeatAddress(company);
                }

                $scope.$on('offerActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SaveOffer', $scope.Offer, {});
                    }
                });

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
    .controller('controllerBasicData',
        ['$scope', '$filter', '$timeout', '$window', 'Offer',
        function ($scope, $filter, $timeout, $window, Offer) {

            $scope.model = Offer.BasicData;

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])

    .controller('controllerAttachedDocuments',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'Offer', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, rfc4122, Offer, AttachedDocumentsInfo) {

            $scope.items = Offer.AttachedDocuments.AttachedDocumentCollection;
            $scope.hasValidCount = Offer.AttachedDocuments.HasValidCount;
            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;

            $scope.items.forEach(function (item) {
                if (item.AttachedDocumentContent == undefined) {
                    item.AttachedDocumentContent = {};
                }
                if (item.SignatureContentCollection == undefined || item.SignatureContentCollection.length == 0) {
                    item.SignatureContentCollection = [{}];
                }
            });

            $scope.addItem = function () {
                var item = {
                    IsTypeValid: true,
                    IsDescriptionValid: true,
                    AttachedDocumentContent: {
                        IsDocumentValid: true
                    },
                    SignatureContentCollection: [{
                        IsDocumentValid: true
                    }]
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

