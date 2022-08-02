angular.module('moduleProcurements', ['scaffolding'])
    .factory('Procurements', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].Procurements;
    }])
    .factory('AttachedDocumentsInfo', ['$window', function ($window) {
        return {
            resourcesAttachedDocuments: $window['_eumis_options'].resourcesAttachedDocuments,
            blobUrl: $window['_eumis_options'].blobUrl
        };
    }])
    .controller('controllerMainProcurements',
            ['$scope', '$filter', '$timeout', '$window', 'Procurements', 'appcontext',
            function ($scope, $filter, $timeout, $window, Procurements, appcontext) {
                $scope.globalKey = '_eumis_options';

                $scope.Procurements = Procurements;

                $scope.$on('procurementsActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SaveProcurements', $scope.Procurements, {});
                    }
                });

                $scope.resourcesObjectCompany = $window[$scope.globalKey].resourcesObjectCompany;

                $scope.acceptances = $window[$scope.globalKey].acceptances;
                $scope.noId = $window[$scope.globalKey].noId;
                $scope.noName = $window[$scope.globalKey].noName;

                $scope.resourcesObjectBoolean = $window[$scope.globalKey].resourcesObjectBoolean;

                $scope.subcontractorMemberTypes = $window[$scope.globalKey].subcontractorMemberTypes;
                $scope.subcontractorId = $window[$scope.globalKey].subcontractorId;
                $scope.subcontractorName = $window[$scope.globalKey].subcontractorName;

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
        .controller('controllerContractorCollection',
        ['$scope', '$filter', '$timeout', '$window', 'Procurements', 'rfc4122',
        function ($scope, $filter, $timeout, $window, Procurements, rfc4122) {

            if (Procurements.Contractors.ContractorCollection == undefined) {
                Procurements.Contractors.ContractorCollection = [];
            }

            $scope.items = Procurements.Contractors.ContractorCollection;
            $scope.isValid = Procurements.Contractors.IsValid;

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsUinTypeValid: true,
                    IsUinValid: true,
                    IsNameValid: true,
                    IsNameEnValid: true,

                    Seat: {
                        IsCountryValid: true,
                        IsSettlementValid: true,
                        IsPostCodeValid: true,
                        IsStreetValid: true,
                        IsFullAddressValid: true
                    },

                    IsVATRegistrationValid: true,

                    VATRegistration: {
                        Value: $scope.noId,
                        Description: $scope.noName,
                    },

                    gid: rfc4122.newuuid(),
                    isActive: true,
                    isActivated: false
                };

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            }

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
        .controller('controllerContractContractorCollection',
        ['$scope', '$filter', '$timeout', '$window', 'Procurements', 'rfc4122', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, Procurements, rfc4122, AttachedDocumentsInfo) {

            if (Procurements.ContractContractors.ContractContractorCollection == undefined) {
                Procurements.ContractContractors.ContractContractorCollection = [];
            }

            $scope.items = Procurements.ContractContractors.ContractContractorCollection;
            $scope.isValid = Procurements.ContractContractors.IsValid;

            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;
            $scope.orderNum = $window[$scope.globalKey].Procurements.orderNum;
            $scope.activationDate = $window[$scope.globalKey].Procurements.activationDate === '01.01.0001' ? '' : $window[$scope.globalKey].Procurements.activationDate;


            $scope.items.forEach(function (item) {
                if (item.AttachedDocument == undefined) {
                    item.AttachedDocument = {
                        IsTypeValid: true,
                        IsDescriptionValid: true,
                        VersionNum: $scope.orderNum,
                        RegNumber: $scope.orderNum,
                        ActivationDate: $scope.activationDate
                    };
                }
                if (item.AttachedDocument.AttachedDocumentContent == undefined) {
                    item.AttachedDocument.AttachedDocumentContent = { IsDocumentValid: true };
                }
            });

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsSignDateValid: true,
                    IsNumberValid: true,

                    IsTotalAmountExcludingVATValid: true,
                    IsContractAmountWithoutVATValid: true,
                    IsVATAmountIfEligibleValid: true,
                    IsTotalFundedValueValid: true,

                    IsBudgetDifferenceValueValid: true,

                    IsNumberAnnexesValid: true,
                    IsCurrentAnnexTotalAmountValid: true,
                    IsCommentValid: true,

                    IsStartDateValid: true,
                    IsEndDateValid: true,
                    IsContractorValid: true,

                    IsUniquePairValid: true,
                    HasSubcontractorMember: false,

                    SubcontractorMemberCollection: [],

                    AttachedDocumentCollection: [
                        {
                            IsDescriptionValid: true,
                            VersionNum: $scope.orderNum,
                            RegNumber: $scope.orderNum,
                            ActivationDate: $scope.activationDate,
                            AttachedDocumentContent: {
                                IsDocumentValid: true
                            }
                        }
                    ],

                    gid: rfc4122.newuuid(),
                    isActive: true,
                    isActivated: false
                };

                if ($scope.contractorsNomenclature.length == 1) {
                    item.Contractor = $scope.contractorsNomenclature[0];
                }

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            }

            // Contractors

            $scope.loadContractorsNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.contractorsNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.$watch(function () {
                return Procurements.Contractors.ContractorCollection;
            }, function () {

                var contractors = [];

                Procurements.Contractors.ContractorCollection.forEach(function (contractor) {
                    if (contractor.Name && contractor.Uin) {
                        var nomName = contractor.Uin + ' ' + contractor.Name;
                        contractors.push({ id: contractor.gid, text: nomName, Name: nomName });
                    }
                });

                $scope.updateContractorsNomenclature(contractors);
                $scope.updateContractorsCollection();

            }, true);

            $scope.updateContractorsNomenclature = function (contractors) {
                $scope.contractorsNomenclature = [];

                if (!contractors) {
                    contractors = [];
                }
                contractors.forEach(function (contractor) {
                    var map = $.map($scope.contractorsNomenclature, function (val) { return val.id; });
                    if (map.indexOf(contractor.id) == -1) {
                        $scope.contractorsNomenclature.push(contractor);
                    }
                });
            };

            var _emptyNomItem = {
                id: "",
                text: "",
                Name: ""
            };

            $scope.updateContractorsCollection = function () {
                $scope.items.forEach(function (item) {
                    if ($scope.contractorsNomenclature.length == 0) {
                        item.Contractor = _emptyNomItem;
                    }
                    else if ($scope.contractorsNomenclature.length == 1) {
                        item.Contractor = $scope.contractorsNomenclature[0];
                    } else {
                        if (angular.isDefined(item.Contractor) && item.Contractor != null) {
                            var filtered = $scope.contractorsNomenclature.filter(function (nom) {
                                return nom.id == item.Contractor.id
                                    && nom.Name == item.Contractor.Name;
                            });
                            if (filtered.length == 0) {
                                item.Contractor = _emptyNomItem;
                            }
                        }
                        else {
                            item.Contractor = _emptyNomItem;
                        }
                    }
                    if (item.HasSubcontractorMember) {
                        item.SubcontractorMemberCollection.forEach(function (member) {
                            if ($scope.contractorsNomenclature.length == 0) {
                                member.Contractor = _emptyNomItem;
                            }
                            if (angular.isDefined(member.Contractor) && member.Contractor != null) {
                                var filtered = $scope.contractorsNomenclature.filter(function (nom) {
                                    return nom.id == member.Contractor.id
                                        && nom.Name == member.Contractor.Name;
                                });
                                if (filtered.length == 0) {
                                    member.Contractor = _emptyNomItem;
                                }
                            }
                            else {
                                member.Contractor = _emptyNomItem;
                            }
                        })
                    }
                });
            };

            // Subcontractor / members
            $scope.addMember = function (item) {
                var member = {
                    editTriggerId: rfc4122.newuuid(),
                    Type: {
                        Value: $scope.subcontractorId,
                        Description: $scope.subcontractorName
                    },
                    Contractor: {
                        id: "",
                        text: "",
                        Name: ""
                    },
                    IsContractorValid: true,
                    IsContractDateValid: true,
                    IsContractNumberValid: true,
                    IsContractAmountValid: true
                };

                item.SubcontractorMemberCollection.push(member);

                $timeout(function () {
                    $("#" + member.editTriggerId).click();
                }, 50);
            }

            $scope.delMember = function (item, member) {
                item.SubcontractorMemberCollection.splice(item.SubcontractorMemberCollection.indexOf(member), 1);
            }

            // REFS
            $scope.addRef = function (item) {
                var ref = {
                    ContractActivity: {
                        id: "",
                        text: "",
                        Name: ""
                    },
                    BudgetDetail: {
                        id: "",
                        text: "",
                        Name: ""
                    },

                    IsContractActivityValid: true,
                    IsBudgetDetailValid: true
                };

                item.ActivitiesBudgetDetailsRefCollection.push(ref);
            }

            $scope.delRef = function (item, ref) {
                item.ActivitiesBudgetDetailsRefCollection.splice(item.ActivitiesBudgetDetailsRefCollection.indexOf(ref), 1);
            }

            $scope.$watch('items', function () {
                $scope.items.forEach(function (item) {
                    if (item.ActivitiesBudgetDetailsRefCollection == undefined) {
                        item.ActivitiesBudgetDetailsRefCollection = [];
                    }
                    if (item.ActivitiesBudgetDetailsRefCollection.length == 0) {
                        $scope.addRef(item);
                    }

                    if (item.SubcontractorMemberCollection == undefined) {
                        item.SubcontractorMemberCollection = [];
                    }
                    if (item.SubcontractorMemberCollection.length == 0) {
                        $scope.addMember(item);
                    }
                });
            }, true);

            $scope.loadContractActivitiesNomenclature = function (query) {
                var data = { results: [] };
                $.each(Procurements.ContractActivityItemCollection, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.loadBudgetLevel3Nomenclature = function (query) {
                var data = { results: [] };
                $.each(Procurements.BudgetLevel3ItemCollection, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            // attached documents
            $scope.addAttachedDocumentItem = function (item) {
                if (!item.AttachedDocumentCollection) {
                    item.AttachedDocumentCollection = [];
                }

                var attachedDocumentItem = {
                    IsTypeValid: true,
                    IsDescriptionValid: true,
                    VersionNum: $scope.orderNum,
                    RegNumber: $scope.orderNum,
                    ActivationDate: $scope.activationDate,
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
                if (!item.VersionNum) {
                    return true;
                }
                return item.VersionNum === $scope.orderNum;
            };

            $scope.delAttachedDocumentItem = function (item, attachedDocumentItem) {
                item.AttachedDocumentCollection.splice(item.AttachedDocumentCollection.indexOf(attachedDocumentItem), 1);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
        .controller('controllerProcurementPlanCollection',
        ['$scope', '$filter', '$timeout', '$window', 'Procurements', 'rfc4122', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, Procurements, rfc4122, AttachedDocumentsInfo) {
            $scope.globalKey = '_eumis_options';

            if (Procurements.ProcurementPlans.ProcurementPlanCollection == undefined) {
                Procurements.ProcurementPlans.ProcurementPlanCollection = [];
            }

            $scope.items = Procurements.ProcurementPlans.ProcurementPlanCollection;
            $scope.centralProcurements = Procurements.CentralProcurements;
            $scope.isValid = Procurements.ProcurementPlans.IsValid;

            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;
            $scope.pmsGid = $window[$scope.globalKey]['constants'].ProcurementPlansErrandLegalActPmsGid;

            $scope.orderNum = $window[$scope.globalKey].Procurements.orderNum;
            $scope.activationDate = $window[$scope.globalKey].Procurements.activationDate === '01.01.0001' ? '' : $window[$scope.globalKey].Procurements.activationDate;

            $scope.centralProcurementNomenclature = (Procurements.CentralProcurements|| []).map(function (item) {
                return {
                    id: item.centralProcurement.gid,
                    text: item.centralProcurement.name,
                    Name: item.centralProcurement.name
                }
            });

            $scope.loadCentralProcurementNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.centralProcurementNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            }

            // Add/remove items

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),

                    gid: rfc4122.newuuid(),

                    BFPContractPlan: {
                        IsNameValid: true,
                        IsErrandAreaValid: true,
                        IsErrandLegalActValid: true,
                        IsErrandTypeValid: true,
                        IsAmountValid: true,
                        IsPlanDateValid: true,
                        IsDescriptionValid: true
                    },

                    DifferentiatedPositionCollection: [],

                    IsNameValid: true,
                    IsAreaValid: true,
                    IsLegalActValid: true,
                    IsProcedureTypeValid: true,
                    IsMAPreliminaryControlValid: true,
                    IsPPAPreliminaryControlValid: true,
                    IsInternetAddressValid: true,
                    IsExpectedAmountValid: true,
                    IsNoticeDateValid: true,
                    IsOffersDeadlineDateValid: true,
                    IsDifferentiatedPositionCountValid: true,
                    IsPublicAttachedDocumentCountValid: true,
                    IsAdditionalAttachedDocumentCountValid: true,

                    MAPreliminaryControl: {
                        Value: $scope.noId,
                        Description: $scope.noName
                    },

                    PPAPreliminaryControl: {
                        Value: $scope.noId,
                        Description: $scope.noName
                    },

                    AttachedDocumentCollection: [
                    ],

                    PublicAttachedDocumentCollection: [
                    ],

                    AdditionalAttachedDocumentCollection: [
                    ]
                };

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            }

            // Contracts

            $scope.loadContractContractorsNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.contractsNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.$watch(function () {
                return Procurements.ContractContractors.ContractContractorCollection;
            }, function () {

                var contracts = [];

                Procurements.ContractContractors.ContractContractorCollection.forEach(function (contract) {
                    if (contract.SignDate && contract.Number && contract.Contractor && contract.Contractor.Name) {
                        var nomName = 'No ' + contract.Number + ' / ' + contract.SignDate + ' - ' + contract.Contractor.Name;
                        contracts.push({ id: contract.gid, text: nomName, Name: nomName });
                    }
                });

                $scope.updateContractsNomenclature(contracts);
                $scope.updateContractsCollection();

            }, true);

            $scope.updateContractsNomenclature = function (contracts) {
                $scope.contractsNomenclature = [];

                if (!contracts) {
                    contracts = [];
                }
                contracts.forEach(function (contract) {
                    var map = $.map($scope.contractsNomenclature, function (val) { return val.id; });
                    if (map.indexOf(contract.id) == -1) {
                        $scope.contractsNomenclature.push(contract);
                    }
                });
            };

            var _emptyNomItem = {
                id: "",
                text: "",
                Name: ""
            };

            $scope.updateContractsCollection = function () {
                $scope.items.forEach(function (item) {
                    item.DifferentiatedPositionCollection.forEach(function (position) {
                        if ($scope.contractsNomenclature.length == 0) {
                            position.ContractContractor = _emptyNomItem;
                        }
                        else {
                            if (angular.isDefined(position.ContractContractor) && position.ContractContractor != null) {
                                var filtered = $scope.contractsNomenclature.filter(function (nom) {
                                    return nom.id == position.ContractContractor.id
                                        && nom.Name == position.ContractContractor.Name;
                                });
                                if (filtered.length == 0) {
                                    position.ContractContractor = _emptyNomItem;
                                }
                            }
                            else {
                                position.ContractContractor = _emptyNomItem;
                            }
                        }
                    });
                });
            };

            // Positions
            $scope.addPosition = function (item) {
                var position = {
                    editTriggerId: rfc4122.newuuid(),

                    gid: rfc4122.newuuid(),

                    ContractContractor: {
                        id: "",
                        text: "",
                        Name: ""
                    },
                    IsNameValid: true,
                    IsSubmittedOffersCountValid: true,
                    IsRankedOffersCountValid: true,
                    IsCommentValid: true
                };

                item.DifferentiatedPositionCollection.push(position);

                $timeout(function () {
                    $("#" + position.editTriggerId).click();
                }, 50);
            }

            $scope.delPosition = function (item, position) {
                item.DifferentiatedPositionCollection.splice(item.DifferentiatedPositionCollection.indexOf(position), 1);
            }

            // attached documents
            $scope.addAttachedDocumentItem = function (item) {
                if (!item.AttachedDocumentCollection) {
                    item.AttachedDocumentCollection = [];
                }

                var attachedDocumentItem = {
                    IsTypeValid: true,
                    IsDescriptionValid: true,
                    VersionNum: $scope.orderNum,
                    RegNumber: $scope.orderNum,
                    ActivationDate: $scope.activationDate,
                    AttachedDocumentContent: {
                        IsDocumentValid: true
                    }
                };

                item.AttachedDocumentCollection.push(attachedDocumentItem);

                $timeout(function () {
                    $("#" + attachedDocumentItem.editTriggerId).click();
                }, 50);
            };

            $scope.delAttachedDocumentItem = function (item, attachedDocumentItem) {
                item.AttachedDocumentCollection.splice(item.AttachedDocumentCollection.indexOf(attachedDocumentItem), 1);
            };

            $scope.isCurrentVersion = function (item) {
                if (!item.VersionNum) {
                    return true;
                }
                return item.VersionNum === $scope.orderNum;
            };

            $scope.toNomenclature = function (nomenclature) {
                idValue = nomenclature.code ? nomenclature.code : nomenclature.gid
                return {
                    Code: idValue,
                    Value: idValue,
                    Id: idValue,

                    Name: nomenclature.name,
                    NameEN: nomenclature.name,
                    text: nomenclature.name,
                    Description: nomenclature.name,
                    parentId: null,
                    isRequired: null,
                    isSignatureRequired: null
                }
            }

            $scope.loadAttachedDocuments = function (item, procurementDocuments) {
                item.AttachedDocumentCollection = [];

                $.each(procurementDocuments, function () {
                    var attachedDocumentItem = {
                        IsTypeValid: true,
                        IsDescriptionValid: true,
                        VersionNum: $scope.orderNum,
                        RegNumber: $scope.orderNum,
                        ActivationDate: $scope.activationDate,
                        AttachedDocumentContent: {
                            IsDocumentValid: true,
                            FileName: this.name,
                            BlobContentId: this.blobKey,
                        },
                        Description: this.description
                    };

                    item.AttachedDocumentCollection.push(attachedDocumentItem);
                })
            }

            $scope.loadDifferentiatedPositions = function (item, differentiatedPositions) {
                item.DifferentiatedPositionCollection = [];
                $.each(differentiatedPositions, function () {
                    var position = {
                        editTriggerId: rfc4122.newuuid(),

                        gid: rfc4122.newuuid(),

                        ContractContractor: {
                            id: "",
                            text: "",
                            Name: ""
                        },
                        IsNameValid: true,
                        IsSubmittedOffersCountValid: true,
                        IsRankedOffersCountValid: true,
                        IsCommentValid: true,
                        Name: this.name,
                        Comment: this.comment
                    };

                    item.DifferentiatedPositionCollection.push(position);
                })
            }

            $scope.loadContractPlan = function (procurementPlan) {
                return {
                    ErrandArea: $scope.toNomenclature(procurementPlan.errandArea),
                    ErrandLegalAct: $scope.toNomenclature(procurementPlan.errandLegalAct),
                    ErrandType: $scope.toNomenclature(procurementPlan.errandType),
                    Name: procurementPlan.name,
                    Description: procurementPlan.description,
                    IsNameValid: true,
                    IsErrandAreaValid: true,
                    IsErrandLegalActValid: true,
                    IsErrandTypeValid: true,
                    IsAmountValid: true,
                    IsPlanDateValid: true,
                    IsDescriptionValid: true
                }
            }

            $scope.fillProcurementData = function (item) {
                if (!item.CentralProcurement) {
                    return;
                }

                $.each(Procurements.CentralProcurements, function () {
                    if (this.centralProcurement.gid === item.CentralProcurement.id) {
                        item.BFPContractPlan = $scope.loadContractPlan(this.procurementPlan);
                        item.PPANumber = this.pPANumber;
                        $scope.loadDifferentiatedPositions(item, this.differentiatedPositions);
                        $scope.loadAttachedDocuments(item, this.procurementDocuments);
                    }
                });
            }

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])

