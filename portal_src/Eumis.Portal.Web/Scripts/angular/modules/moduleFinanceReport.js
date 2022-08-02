angular.module('moduleFinanceReport', ['scaffolding', 'ui.jq'])
    .factory('FinanceReport', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].FinanceReport;
    }])
    .factory('AttachedDocumentsInfo', ['$window', function ($window) {
        return {
            resourcesAttachedDocuments: $window['_eumis_options'].resourcesAttachedDocuments,
            blobUrl: $window['_eumis_options'].blobUrl
        };
    }])
    .controller('controllerMainFinanceReport',
            ['$scope', '$filter', '$timeout', '$window', 'FinanceReport', 'appcontext',
            function ($scope, $filter, $timeout, $window, FinanceReport, appcontext) {
                $scope.globalKey = '_eumis_options';

                $scope.FinanceReport = FinanceReport;

                $scope.$on('financeReportActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SaveFinanceReport', $scope.FinanceReport, {});
                    }
                });

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
    .controller('controllerBasicData',
        ['$scope', '$filter', '$timeout', '$window', 'FinanceReport',
        function ($scope, $filter, $timeout, $window, FinanceReport) {

            $scope.model = FinanceReport.BasicData;

            $scope.globalKey = '_eumis_options';
            $scope.reportTypes = $window[$scope.globalKey].reportTypes;

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerCostSupportingDocuments',
        ['$scope', '$filter', '$timeout', '$window', 'FinanceReport', 'rfc4122', 'AttachedDocumentsInfo', '$q',
        function ($scope, $filter, $timeout, $window, FinanceReport, rfc4122, AttachedDocumentsInfo, $q) {
            $scope.globalKey = '_eumis_options';
            $scope.acceptances = $window[$scope.globalKey].acceptances;
            $scope.noId = $window[$scope.globalKey].noId;
            $scope.noName = $window[$scope.globalKey].noName;

            $scope.resourcesObjectBoolean = $window[$scope.globalKey].resourcesObjectBoolean;

            $scope.versionNum = $window[$scope.globalKey].FinanceReport.docNumber;
            $scope.versionSubNum = $window[$scope.globalKey].FinanceReport.docSubNumber;

            // init
            $scope.items = FinanceReport.CostSupportingDocuments.CostSupportingDocumentCollection;
            $scope.isValid = FinanceReport.CostSupportingDocuments.IsValid;

            $scope.beneficiary = FinanceReport.Beneficiary;

            $scope.partnersNomenclature = FinanceReport.PartnerItemCollection;

            $scope.contractorsNomenclature = FinanceReport.ContractorItemCollection;

            $scope.contractsNomenclature = FinanceReport.ContractContractorItems;

            $scope.dictionaryNomenclature = FinanceReport.ActivityBudgetDetailItems;

            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;

            initAttachedDocuments();

            syncPartnerCollection();
            syncContractorCollection();

            // items
            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    gid: rfc4122.newuuid(),

                    CompanyType: 'Beneficiary',

                    IsTypeValid: true,
                    IsLocked: false,
                    IsDescriptionValid: true,
                    IsNumberValid: true,
                    IsDateValid: true,
                    IsPaymentDateValid: true,
                    IsPartnerValid: true,
                    IsContractorValid: true,
                    IsContractContractorValid: true,

                    AttachedDocumentCollection: [
                        {
                            IsTypeValid: true,
                            IsDescriptionValid: true,
                            VersionNum: $scope.versionNum,
                            VersionSubNum: $scope.versionSubNum,
                            RegNumber: $scope.versionNum + '.' + $scope.versionSubNum,
                            AttachedDocumentContent: {
                                IsDocumentValid: true
                            }
                        }
                    ],
                    FinanceReportBudgetItemDataCollection: [
                        {
                            IsBudgetDetailValid: true,
                            IsContractActivityValid: true,
                            IsUnitDefinitionValid: true,
                            IsProducedUnitsCountValid: true,
                            IsUnitCostValid: true,
                            IsGrandAmountValid: true,
                            IsSelfAmountValid: true,
                            IsCrossFinancingValid: true,
                            IsTotalAmountValid: true,

                            CrossFinancing: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            },
                            IsVatAmount: false,
                            InsideEU: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            },
                            OutsideEU: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            },
                            OutsideEUInProgrammingAreaEFRR: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            },
                            ThirdCountriesEFRR: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            },
                            AdvancePayment: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            },
                            ContributionNature: {
                                Value: $scope.noId,
                                Description: $scope.noName,
                            }
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

            $scope.sumTotalAmount = function (index) {
                $scope.items[index].TotalAmount = 0;
                $scope.items[index].FinanceReportBudgetItemDataCollection.forEach(function (element) {
                    $scope.items[index].TotalAmount += Number(element.GrandAmount ? element.GrandAmount : 0) + Number(element.SelfAmount ? element.SelfAmount : 0);
                });
            };

            // accordion
            $scope.accordion = function (index, $event) {
                $scope.items[index].isOpen = !$scope.items[index].isOpen;
                $scope.items.forEach(function (items, i) {
                    if (i != index) {
                        items.isOpen = false;
                    }
                });

                var currentHistoryTable = $($event.currentTarget).parents('tr').nextAll("tr.history-table").first();
                var historyTableWrapper = currentHistoryTable.find("div.history-table-wrapper");
                triggerTextareasEvents(historyTableWrapper);
                fnPopover(historyTableWrapper);
            };


            // budget items
            $scope.addBudgetItem = function (item) {
                if (!item.FinanceReportBudgetItemDataCollection) {
                    item.FinanceReportBudgetItemDataCollection = [];
                }

                var budgetItem = {
                    editTriggerId: rfc4122.newuuid(),

                    IsBudgetDetailValid: true,
                    IsContractActivityValid: true,
                    IsGrandAmountValid: true,
                    IsSelfAmountValid: true,
                    IsCrossFinancingValid: true,
                    IsTotalAmountValid: true,
                    IsUnitDefinitionValid: true,
                    IsProducedUnitsCountValid: true,
                    IsUnitCostValid: true,

                    CrossFinancing: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },
                    IsVatAmount: false,
                    InsideEU: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },
                    OutsideEU: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },
                    OutsideEUInProgrammingAreaEFRR: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },
                    ThirdCountriesEFRR: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },
                    AdvancePayment: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },
                    ContributionNature: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    }
                };

                item.FinanceReportBudgetItemDataCollection.push(budgetItem);

                $timeout(function () {
                    $("#" + budgetItem.editTriggerId).click();
                }, 50);
            };

            $scope.delBudgetItem = function (item, budgetItem, index) {
                item.FinanceReportBudgetItemDataCollection.splice(item.FinanceReportBudgetItemDataCollection.indexOf(budgetItem), 1);
                $scope.sumTotalAmount(index);
            };

            // attached documents
            $scope.addAttachedDocumentItem = function (item) {
                if (!item.AttachedDocumentCollection) {
                    item.AttachedDocumentCollection = [];
                }

                var attachedDocumentItem = {
                    IsTypeValid: true,
                    IsDescriptionValid: true,
                    VersionNum: $scope.versionNum,
                    VersionSubNum: $scope.versionSubNum,
                    RegNumber: $scope.versionNum + '.' + $scope.versionSubNum,
                    AttachedDocumentContent: {
                        IsDocumentValid: true
                    }
                };

                item.AttachedDocumentCollection.push(attachedDocumentItem);

                $timeout(function () {
                    $("#" + attachedDocumentItem.editTriggerId).click();
                }, 50);
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

            $scope.delAttachedDocumentItem = function (item, attachedDocumentItem) {
                item.AttachedDocumentCollection.splice(item.AttachedDocumentCollection.indexOf(attachedDocumentItem), 1);
            };

            function initAttachedDocuments() {
                $scope.items.forEach(function (item) {
                    if (item.AttachedDocument == undefined) {
                        item.AttachedDocument = {
                            IsTypeValid: true,
                            IsDescriptionValid: true
                        };
                    }
                    if (item.AttachedDocument.AttachedDocumentContent == undefined) {
                        item.AttachedDocument.AttachedDocumentContent = { IsDocumentValid: true };
                    }
                });
            }

            // nomenclatures
            $scope.loadPartnersNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.partnersNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.loadContractorsNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.contractorsNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.contractsConfig = function (item) {
                return {
                    allowClear: true,
                    placeholder: ' ',
                    query: function (query) {
                        var data = { results: [] };

                        if (item.Contractor && item.Contractor.id) {
                            var noms = $scope.contractsNomenclature[item.Contractor.id];

                            var addedIds = [];
                            $.each(noms, function () {
                                if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                                    if (addedIds.indexOf(this.id) < 0) {
                                        data.results.push(this);
                                        addedIds.push(this.id);
                                    }
                                }
                            });
                        }
                        query.callback(data);
                    }
                };
            }

            $scope.budgetDetailsConfig = function (item, budgetItem) {
                return {
                    allowClear: true,
                    placeholder: ' ',
                    query: function (query) {
                        var data = { results: [] };

                        var doubles = [];

                        if (item.CompanyType == 'Contractor' && item.ContractContractor && item.ContractContractor.id) {
                            doubles = $scope.dictionaryNomenclature[item.ContractContractor.id];
                        }
                        else {
                            doubles = $scope.dictionaryNomenclature[''];
                        }

                        if (doubles && doubles.length > 0) {
                            if (budgetItem.ContractActivity && budgetItem.ContractActivity.id) {
                                doubles = $.map(doubles, function (val) {
                                    if (val.ContractActivity.id == budgetItem.ContractActivity.id) {
                                        return val;
                                    }
                                })
                            }

                            var addedIds = [];
                            $.each(doubles, function () {
                                if (query.term.length == 0 || this.BudgetDetail.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                                    if (addedIds.indexOf(this.BudgetDetail.id) < 0) {
                                        data.results.push(this.BudgetDetail);
                                        addedIds.push(this.BudgetDetail.id);
                                    }
                                }
                            });

                        }

                        query.callback(data);
                    }
                };
            }

            $scope.contractActivitiesConfig = function (item, budgetItem) {
                return {
                    allowClear: true,
                    placeholder: ' ',
                    query: function (query) {
                        var data = { results: [] };

                        var doubles = [];

                        if (item.ContractContractor && item.ContractContractor.id) {
                            doubles = $scope.dictionaryNomenclature[item.ContractContractor.id];
                        }
                        else {
                            doubles = $scope.dictionaryNomenclature[''];
                        }

                        if (doubles && doubles.length > 0) {
                            if (budgetItem.BudgetDetail && budgetItem.BudgetDetail.id) {
                                doubles = $.map(doubles, function (val) {
                                    if (val.BudgetDetail.id == budgetItem.BudgetDetail.id) {
                                        return val;
                                    }
                                })
                            }

                            var addedIds = [];
                            $.each(doubles, function () {
                                if (this.ContractActivity && this.ContractActivity.id) {
                                    if (query.term.length == 0 || this.ContractActivity.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                                        if (addedIds.indexOf(this.ContractActivity.id) < 0) {
                                            data.results.push(this.ContractActivity);
                                            addedIds.push(this.ContractActivity.id);
                                        }
                                    }
                                }
                            });

                        }
                        query.callback(data);
                    }
                };
            }

            function syncPartnerCollection() {
                $scope.items.forEach(function (item) {
                    if (item.CompanyType == 'Partner' && item.Partner && $scope.partnersNomenclature) {
                        let newItem = $scope.partnersNomenclature.find(function (element) {
                            return element.id === item.Partner.id
                        });

                        if (newItem && newItem.displayName != item.Partner.displayName) {
                            item.Partner.displayName = newItem.displayName;
                            item.Partner.Name = newItem.Name;
                            item.Partner.text = newItem.text;
                        }
                    }
                });
            }

            function syncContractorCollection() {
                $scope.items.forEach(function (item) {
                    if (item.CompanyType == 'Contractor' && item.Contractor && $scope.contractorsNomenclature) {
                        let newItem = $scope.contractorsNomenclature.find(function (element) {
                            return element.id === item.Contractor.id
                        });

                        if (newItem && newItem.displayName != item.Contractor.displayName) {
                            item.Contractor.displayName = newItem.displayName;
                            item.Contractor.Name = newItem.Name;
                            item.Contractor.text = newItem.text;
                        }

                        if (item.ContractContractor && $scope.contractsNomenclature) {
                            let noms = $scope.contractsNomenclature[item.Contractor.id];

                            if (noms) {
                                let newContractContractor = noms.find(function (element) {
                                    return element.id === item.ContractContractor.id
                                });

                                if (newContractContractor && newContractContractor.displayName != item.ContractContractor.displayName) {
                                    item.ContractContractor.displayName = newContractContractor.displayName;
                                    item.ContractContractor.Name = newContractContractor.Name;
                                    item.ContractContractor.text = newContractContractor.text;
                                }
                            }
                        }
                    }
                });
            }

            // nomenclatures dependancies

            var _emptyNomItem = {
                id: "",
                text: "",
                Name: ""
            };

            $scope.clearContractContractors = function (item) {
                item.ContractContractor = _emptyNomItem;
            }

            $scope.clearBudgetActivities = function (item) {
                if (item.FinanceReportBudgetItemDataCollection && item.FinanceReportBudgetItemDataCollection.length > 0) {
                    $.each(item.FinanceReportBudgetItemDataCollection, function () {
                        this.BudgetDetail = _emptyNomItem;
                        this.ContractActivity = _emptyNomItem;
                    });
                }
            }


            // excel importing
            $scope.docsImportUrl = $window[$scope.globalKey].docsImportUrl;

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
                            item.IsLocked = false;
                            item.AttachedDocumentCollection[0] = {
                                IsDescriptionValid: true,
                                VersionNum: $scope.versionNum,
                                VersionSubNum: $scope.versionSubNum,
                                RegNumber: $scope.versionNum + '.' + $scope.versionSubNum,
                                AttachedDocumentContent: {
                                    IsDocumentValid: true
                                }
                            };
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
                url: $scope.docsImportUrl,
                add: $scope.add,
                done: $scope.done,
                error: $scope.error,
                always: $scope.refreshDefaultState
            };

            $scope.deleteAllItems = function () {
                var indexes = [];

                for (var i = 0; i < $scope.items.length; i++) {
                    if ($scope.items[i].IsLocked === false) {
                        indexes.push(i);
                    }
                }

                for (let i = indexes.length - 1; i >= 0; i--) {
                    $scope.items.splice(indexes[i], 1);
                }
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }]);

