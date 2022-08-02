angular.module('moduleTechnicalReport', ['scaffolding', 'ui.jq'])
    .factory('TechnicalReport', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].TechnicalReport;
    }])
    .factory('AttachedDocumentsInfo', ['$window', function ($window) {
        return {
            resourcesAttachedDocuments: $window['_eumis_options'].resourcesAttachedDocuments,
            blobUrl: $window['_eumis_options'].blobUrl
        };
    }])
    .controller('controllerMainTechnicalReport',
            ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'appcontext',
            function ($scope, $filter, $timeout, $window, TechnicalReport, appcontext) {
                $scope.globalKey = '_eumis_options';

                $scope.TechnicalReport = TechnicalReport;

                $scope.$on('technicalReportActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SaveTechnicalReport', $scope.TechnicalReport, {});
                    }
                });

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
    .controller('controllerBasicData',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport',
        function ($scope, $filter, $timeout, $window, TechnicalReport) {

            $scope.model = TechnicalReport.BasicData;

            $scope.globalKey = '_eumis_options';

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerActivities',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'rfc4122', 'dateDiff',
        function ($scope, $filter, $timeout, $window, TechnicalReport, rfc4122, dateDiff) {
            $scope.items = TechnicalReport.Activities.ActivityCollection;

            var pattern = /(\d{2})\.(\d{2})\.(\d{4})/;

            $scope.updateMonths = function (item) {
                if (item.ActualStartDate && item.ActualEndDate) {
                    var startDate = item.ActualStartDate instanceof Date ? item.ActualStartDate : new Date(item.ActualStartDate.replace(pattern, '$3-$2-$1'));
                    var endDate = item.ActualEndDate instanceof Date ? item.ActualEndDate : new Date(item.ActualEndDate.replace(pattern, '$3-$2-$1'));
                    item.MonthsDuration = dateDiff.inMonths(startDate, endDate);
                }
                else {
                    item.MonthsDuration = '';
                }
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerIndicators',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'rfc4122',
        function ($scope, $filter, $timeout, $window, TechnicalReport, rfc4122) {
            $scope.items = TechnicalReport.Indicators.IndicatorCollection;

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerBeneficiarySiteInspections',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'rfc4122',
        function ($scope, $filter, $timeout, $window, TechnicalReport, rfc4122) {
            $scope.items = TechnicalReport.BeneficiarySiteInspections.BeneficiarySiteInspectionCollection;

            $scope.items.forEach(function (item) {
                // init actions
            });

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),

                    IsNumberValid: true,
                    IsCheckSubjectValid: true,
                    IsRangeValid: true,
                    IsStartDateValid: true,
                    IsEndDateValid: true,
                    IsKeyRecommendationsValid: true,
                    IsRecommendationsFulfilledValid: true,
                    IsCommentValid: true
                };

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            };

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerAudits',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'rfc4122',
        function ($scope, $filter, $timeout, $window, TechnicalReport, rfc4122) {
            $scope.resourcesObjectBoolean = $window['_eumis_options'].resourcesObjectBoolean;

            $scope.items = TechnicalReport.Audits.AuditCollection;

            $scope.items.forEach(function (item) {
                // init actions
            });

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),

                    IsAuditeeTypeValid: true,
                    IsFinalReportDateValid: true,
                    IsFinalReportNumberValid: true,
                    IsInspectionEndDateValid: true,
                    IsInspectionStartDateValid: true,
                    IsInstitutionValid: true,
                    IsKindValid: true,
                    IsPreviousReportDateValid: true,
                    IsPreviousReportNumberValid: true,
                    IsRangeValid: true,
                    IsTypeValid: true,
                    HasValidCount: true,

                    FindingCollection: [
                        {
                            IsKeyFindingsValid: true,
                            IsRecommendationsValid: true,
                            IsRecommendationsFulfilledValid: true,
                            IsCommentValid: true
                        }
                    ]
                };

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            };

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            };


            // finding items
            $scope.addFindingItem = function (item) {
                if (!item.FindingCollection) {
                    item.FindingCollection = [];
                }

                var findingItem = {
                    editTriggerId: rfc4122.newuuid(),

                    IsKeyFindingsValid: true,
                    IsRecommendationsValid: true,
                    IsRecommendationsFulfilledValid: true,
                    IsCommentValid: true
                };

                item.FindingCollection.push(findingItem);

                $timeout(function () {
                    $("#" + findingItem.editTriggerId).click();
                }, 50);
            };

            $scope.delFindingItem = function (item, findingItem) {
                item.FindingCollection.splice(item.FindingCollection.indexOf(findingItem), 1);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerTeam',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'rfc4122', '$q',
        function ($scope, $filter, $timeout, $window, TechnicalReport, rfc4122, $q) {

            $scope.items = TechnicalReport.Team.TeamMemberCollection;

            $scope.items.forEach(function (item) {
                // init actions
            });

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsNameValid: true,
                    IsPositionValid: true,
                    IsUinTypeValid: true,
                    IsUinValid: true,
                    IsCommitmentTypeValid: true,
                    IsDateValid: true,
                    IsHoursValid: true,
                    IsActivityValid: true,
                    IsResultValid: true
                };

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            };

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            };

            // excel importing
            $scope.membersImportUrl = $window[$scope.globalKey].membersImportUrl;

            // Divs handling
            $scope.validationError = '';

            // state: 1: default; 2: pleaseWait; 3: OK; 4: noResult; 5: validationError
            $scope.switchExcelDivs = function (state) {
                $timeout(function () {
                    $scope.isDefault = false;
                    $scope.isPleaseWait = false;
                    $scope.isOK = false;
                    $scope.isGeneralError = false;
                    $scope.isValidationError = false;

                    switch (state) {
                        case 1:
                            $scope.isDefault = true;
                            break;
                        case 2:
                            $scope.isPleaseWait = true;
                            break;
                        case 3:
                            $scope.isOK = true;
                            break;
                        case 4:
                            $scope.isGeneralError = true;
                            break;
                        case 5:
                            $scope.isValidationError = true;
                            break;
                    }
                }, 150);
            };

            $scope.switchExcelDivs(1);

            var updateUI;
            $scope.add = function (e, data) {
                $scope.switchExcelDivs(2); // pleaseWait

                updateUI = $q.defer();
                updateUI.promise.then(function (data) {
                    if (data && data.result && data.result.length > 0) {
                        data.result.forEach(function (item) {
                            $scope.items.push(item);
                        });

                        $scope.switchExcelDivs(3); // OK
                    }
                    else {
                        $scope.switchExcelDivs(4); // generalError
                    }
                });

                data.submit();
            };

            $scope.done = function (e, data) {
                updateUI.resolve(data);
            };

            $scope.error = function (e, data) {
                if (e && e.responseJSON && e.responseJSON.message) {
                    $scope.validationError = e.responseJSON.message;
                    $scope.switchExcelDivs(5); // validationError
                }
                else {
                    $scope.switchExcelDivs(4); // generalError
                }
            };

            $scope.refreshDefaultState = function () {
                $timeout(function () {
                    $scope.switchExcelDivs(1); // default
                }, 10000);
            }

            $scope.options = {
                dataType: "json",
                url: $scope.membersImportUrl,
                add: $scope.add,
                done: $scope.done,
                error: $scope.error,
                always: $scope.refreshDefaultState
            };
            
            $scope.deleteAllItems = function () {
                $scope.items.splice(0, $scope.items.length);
            };
            
            $scope.accordion = function (index, $event) {
                $scope.items[index].isOpen = !$scope.items[index].isOpen;
                $scope.items.forEach(function (item, i) {
                    if (i != index) {
                        item.isOpen = false;
                    }
                });
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerAttachedDocuments',
        ['$scope', '$filter', '$timeout', '$window', 'TechnicalReport', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, TechnicalReport, AttachedDocumentsInfo) {
            $scope.items = TechnicalReport.AttachedDocuments.AttachedDocumentCollection;
            $scope.hasValidCount = TechnicalReport.AttachedDocuments.HasValidCount;
            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;

            $scope.versionNum = $window['_eumis_options'].TechnicalReport.docNumber;
            $scope.versionSubNum = $window['_eumis_options'].TechnicalReport.docSubNumber;

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

