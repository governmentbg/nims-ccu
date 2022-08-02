angular.module('moduleBFPContract', ['scaffolding'])
    .factory('BFPContract', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].BFPContract;
    }])
    .factory('BasicDataInfo', ['$window', function ($window) {
        return {
            resourcesObjectBoolean: $window['_eumis_options'].resourcesObjectBoolean,
            resourcesObjectNuts: $window['_eumis_options'].resourcesObjectNuts,
            nuts: $window['_eumis_options'].nuts
        };
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
    .controller('controllerMainBFPContract',
            ['$scope', '$filter', '$timeout', '$window', 'BFPContract', 'CompanyInfo', 'appcontext', 'copyAddress',
            function ($scope, $filter, $timeout, $window, BFPContract, CompanyInfo, appcontext, copyAddress) {
                $scope.globalKey = '_eumis_options';

                $scope.BFPContract = BFPContract;

                $scope.CompanyInfo = CompanyInfo;

                $scope.acceptances = $window[$scope.globalKey].acceptances;

                $scope.copyAddress = function(company)  {
                    return copyAddress.copySeatAddress(company);
                }

                $scope.$on('bfpContractActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SaveBFPContract', $scope.BFPContract, {});
                    }
                });

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
    .controller('controllerBFPContractBasicData',
        ['$scope', '$filter', '$timeout', '$window', 'BFPContract', 'BasicDataInfo',
        function ($scope, $filter, $timeout, $window, BFPContract, BasicDataInfo) {

            $scope.model = BFPContract.BFPContractBasicData;

            $scope.resourcesObjectBoolean = BasicDataInfo.resourcesObjectBoolean;
            $scope.resourcesObjectNuts = BasicDataInfo.resourcesObjectNuts;
            $scope.nuts = BasicDataInfo.nuts;

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
    .controller('controllerBFPContractPartners',
        ['$scope', '$filter', '$timeout', '$window', 'BFPContract', 'CompanyInfo', 'rfc4122',
        function ($scope, $filter, $timeout, $window, BFPContract, CompanyInfo, rfc4122) {

            if (BFPContract.Partners.PartnerCollection == undefined) {
                BFPContract.Partners.PartnerCollection = [];
            }

            $scope.partners = BFPContract.Partners.PartnerCollection;// || [];
            $scope.resourcesObject = CompanyInfo.resourcesObjectCompany;

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),

                    gid: rfc4122.newuuid(),
                    isActive: true,
                    isActivated: false
                };
                item = $scope.upgrade(item);

                $scope.partners.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delItem = function (partner) {
                $scope.partners.splice($scope.partners.indexOf(partner), 1);
            }

            $scope.upgrade = function (item) {
                item.IsUinTypeValid = true;
                item.IsUinValid = true;
                item.IsNameValid = true;
                item.IsNameEnValid = true;
                item.IsCompanyTypeValid = true;
                item.IsCompanyLegalTypeValid = true;
                item.IsCompanySizeTypeValid = true;
                item.IsPrivateLegal = false;

                if (!item.Seat) {
                    item.Seat = {};
                    item.Seat.IsCountryValid = true;
                    item.Seat.IsSettlementValid = true;
                    item.Seat.IsPostCodeValid = true;
                    item.Seat.IsStreetValid = true;
                    item.Seat.IsFullAddressValid = true;
                }

                if (!item.Correspondence) {
                    item.Correspondence = {};
                    item.Correspondence.IsCountryValid = true;
                    item.Correspondence.IsSettlementValid = true;
                    item.Correspondence.IsPostCodeValid = true;
                    item.Correspondence.IsStreetValid = true;
                    item.Correspondence.IsFullAddressValid = true;
                }

                item.IsEmailValid = true;
                item.IsPhone1Valid = true;
                item.IsPhone2Valid = true;
                item.IsFaxValid = true;
                item.IsCompanyRepresentativePersonValid = true;
                item.IsCompanyContactPersonValid = true;
                item.IsCompanyContactPersonPhoneValid = true;
                item.IsCompanyContactPersonEmailValid = true;

                if (!item.Correspondence.Country) {
                    item.Correspondence.Country = {};
                    item.Correspondence.Country.id = $window[$scope.globalKey]['constants'].BulgariaId;
                    item.Correspondence.Country.text = $window[$scope.globalKey]['constants'].BulgariaName;
                    item.Correspondence.Country.Code = $window[$scope.globalKey]['constants'].BulgariaId;
                    item.Correspondence.Country.Name = $window[$scope.globalKey]['constants'].BulgariaName;
                }

                if (!item.Seat.Country) {
                    item.Seat.Country = {};
                    item.Seat.Country.id = $window[$scope.globalKey]['constants'].BulgariaId;
                    item.Seat.Country.text = $window[$scope.globalKey]['constants'].BulgariaName;
                    item.Seat.Country.Code = $window[$scope.globalKey]['constants'].BulgariaId;
                    item.Seat.Country.Name = $window[$scope.globalKey]['constants'].BulgariaName;
                }

                return item;
            }

            $scope.$watch('partners', function () {

                var partners = [];

                $scope.partners.forEach(function (partner) {
                    if (partner.Name && partner.Uin) {
                        partners.push({ id: partner.Uin, text: partner.Name, Name: partner.Name });
                    }
                });

                $window[$scope.globalKey]['_selected_partners'] = partners;

                refreshProgrammeContractActivities();

            }, true);

            $scope.accordion = function (index, $event) {
                $scope.partners[index].isOpen = !$scope.partners[index].isOpen;
                $scope.partners.forEach(function (partner, i) {
                    if (i != index) {
                        partner.isOpen = false;
                    }
                });
            };
            
             $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])
    .controller('controllerBFPContractDirectonBudgetContract',
        ['$scope', '$filter', '$timeout', '$window', 'BFPContract', 'romanize', 'rfc4122',
        function ($scope, $filter, $timeout, $window, BFPContract, $romanize, rfc4122) {

            $scope.getName = function (index1, index2, index3) {
                var result = '';

                if (index1 == undefined)
                    return result;
                result += '.BFPContractProgrammeBudgetCollection[' + index1 + '].';

                if (index2 == undefined)
                    return result;
                result += 'BFPContractProgrammeExpenseBudgetCollection[' + index2 + '].';

                if (index3 == undefined)
                    return result;
                result += 'BFPContractProgrammeDetailsExpenseBudgetCollection[' + index3 + '].';

                return result;
            }

            $scope.triple = BFPContract.BFPContractDirectionsBudgetContract;
            $scope.isInitial = BFPContract.type == 0;
            $scope.triple.BFPContractBudget.IsActive = true;
            var _emptyNutsAddress = {
                id: "",
                text: "",
                Code: "",
                Name: "",
                FullPath: "",
                FullPathName: ""
            };

            // ***** INIT *****

            $scope.init = function (sectionNumber) {
                $scope.triple.SectionNumber = sectionNumber;

                $scope.initWatches();
                $scope.initDirections();
                $scope.updateDirections();
            };

            //BFPContractDirectionsBudgetContract.BFPContractBudget.BFPContractProgrammeBudgetCollection[0].BFPContractProgrammeExpenseBudgetCollection[0].BFPContractProgrammeDetailsExpenseBudgetCollection[0].GrandAmount
            $scope.initWatches = function () {
                $scope.triple.BFPContractBudget.BFPContractProgrammeBudgetCollection.forEach(function (level1, i) {
                    level1.BFPContractProgrammeExpenseBudgetCollection.forEach(function (level2, j) {
                        $scope.$watch('triple.BFPContractBudget.BFPContractProgrammeBudgetCollection[' + i + '].BFPContractProgrammeExpenseBudgetCollection[' + j + ']', function () {
                            
                            $scope.updatePercentages(level2);
                            $scope.updateTotals(level1, level2);
                            $scope.updateContract();
                            $scope.updateDirections();
                        }, true);
                    })
                });


                $scope.$watch(
                    function () {
                        return $window[$scope.globalKey]['_selected_nuts_addresses'];
                    }
                    , function (newVal, oldVal) {
                        if (newVal != oldVal) {
                            $scope.updateNutsAddresses();
                        }
                    }
                , true);
            };

            $scope.initDirections = function () {
                DirectrionSection = $scope.triple.Directions
                if (DirectrionSection.DirectionCollection == undefined) {
                    DirectrionSection.DirectionCollection = [];
                }
                $scope.DirectionSection = DirectrionSection;
                $scope.items = DirectrionSection.DirectionCollection;
                $scope.directionNomenclature = (DirectrionSection.Items || []).map(function (item) {
                    return {
                        direction: $scope.toNomenclature(item.DirectionItem),
                        subDirection: $scope.toNomenclature(item.SubDirection)
                    };
                });
            }
            // ***** END INIT *****

            // ***** CODES / NUTS ADDRESSES *****

            $scope.updateNutsAddresses = function () {
                $scope.SelectedNutsAddresses = $window[$scope.globalKey]['_selected_nuts_addresses'] || [];

                $scope.hasMoreThanOneNutsAddress = angular.isDefined($scope.SelectedNutsAddresses) && $scope.SelectedNutsAddresses.length >= 2;

                $scope.eachLevel3(function (level3) {
                    if ($scope.SelectedNutsAddresses.length == 0) {
                        level3.Nuts = _emptyNutsAddress;
                    }
                    else if ($scope.SelectedNutsAddresses.length == 1) {
                        level3.Nuts = $scope.SelectedNutsAddresses[0];
                    } else {
                        if (angular.isDefined(level3.Nuts) && level3.Nuts != null) {
                            var filtered = $scope.SelectedNutsAddresses.filter(function (item) {
                                if (angular.isDefined(level3.Nuts) && level3.Nuts != null) {
                                    return item.Code == level3.Nuts.Code
                                        && item.Name == level3.Nuts.Name
                                        && item.FullPath == level3.Nuts.FullPath
                                        && item.FullPathName == level3.Nuts.FullPathName;
                                } else {
                                    return false;
                                }
                            });
                            if (filtered.length == 0) {
                                level3.Nuts = _emptyNutsAddress;
                            }
                        }
                        else {
                            level3.Nuts = _emptyNutsAddress;
                        }
                    }
                });
            };

            $scope.removeObsoleteDirection = function () {
                $scope.eachLevel3(function (level3) {
                    var foundItem = $scope.items.filter(function (directionSection) {
                        let equalDirection, equalSubDirection = false;
                        if (level3.Direction && level3.Direction.DirectionItem && directionSection.DirectionItem) {
                            equalDirection = directionSection.DirectionItem.id === level3.Direction.DirectionItem.id;
                        }
                        if (level3.Direction && directionSection.SubDirection && level3.Direction.SubDirection) {
                            equalSubDirection = directionSection.SubDirection.id === level3.Direction.SubDirection.id
                        }
                        return equalDirection && equalSubDirection
                    });
                    if (foundItem.length === 0) {
                        level3.Direction = undefined;
                    }
                });
            }

            $scope.updateDirections = function () {
                $scope.eachLevel3(function (level3) {
                    if (level3.Direction) {
                        let compositeDirection = $scope.directionPairToNomenclature(level3.Direction);
                        level3.Direction.text = compositeDirection.text;
                    }
                });
            };

            $scope.loadSelectedDirections = function (query) {
                var data = { results: [] };
                $.each($scope.items, function (index, item) {
                    if (item.DirectionItem && item.DirectionItem.Name) {
                        data.results.push($scope.directionPairToNomenclature(item));
                    }
                });
                query.callback(data);
            };

            $scope.directionPairToNomenclature = function (i) {
                let displayName = i.DirectionItem.Name;
                if (i.SubDirection && i.SubDirection.Name) {
                    displayName += " / " + i.SubDirection.Name
                }
                return {
                    id: i.id,
                    text: displayName,
                    Name: i.Name,
                    NameEN: i.NameEN
                }
            }

            $scope.onChangeDirection = function (direction) {
                if (!direction) {
                    return;
                }
                let currentSelection = $scope.items.filter(function (item) {
                    return item.id === direction.id
                })
                if (currentSelection.length === 1) {
                    direction.DirectionItem = currentSelection[0].DirectionItem;
                    direction.SubDirection = currentSelection[0].SubDirection;
                }
            }
            // ***** END CODES / NUTS ADDRESSES *****

            // ***** CALCULATIONS *****
            $scope.updatePercentages = function (level2) {
                if (level2.isTotalSumActive) {
                    level2.BFPContractProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
                        var parsedGrandPercentage = parseFloat(level2.grandPercentage);
                        var parsedTotalAmount = parseFloat(level3.TotalAmount);
                        if (isNaN(parsedGrandPercentage) || isNaN(parsedTotalAmount)) {
                            level3.GrandAmount = "";
                            level3.SelfAmount = "";
                        }
                        else {
                            level3.GrandAmount = ((parsedGrandPercentage / 100) * parsedTotalAmount).toFixed(2);
                            level3.SelfAmount = (parsedTotalAmount - level3.GrandAmount).toFixed(2);
                        }
                    });
                }
                else {
                    var totalGrand = 0.00;
                    var totalSelf = 0.00;

                    level2.BFPContractProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {

                        level3.TotalAmount = (((1 * level3.GrandAmount) || 0) + ((1 * level3.SelfAmount) || 0)).toFixed(2);

                        var pGrand = parseFloat(level3.GrandAmount);
                        if (!isNaN(pGrand)) {
                            totalGrand += pGrand;
                        }

                        var pSelf = parseFloat(level3.SelfAmount);
                        if (!isNaN(pSelf)) {
                            totalSelf += pSelf;
                        }
                    });

                    if (totalGrand == 0.00 && totalGrand == totalSelf) {
                        level2.grandPercentage = "";
                    }
                    else {
                        level2.grandPercentage = ((totalGrand / (totalGrand + totalSelf)) * 100).toFixed(2);
                    }
                }
            };

            $scope.updateTotals = function (level1, level2) {
                // LEVEL 2
                var level2Grand = 0.00;
                var level2Self = 0.00;
                var level2Total = 0.00;

                if (!level2.IsDeactivated) {
                    level2.BFPContractProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
                        level2Grand += parseFloat(level3.GrandAmount) || 0.00;
                        level2Self += parseFloat(level3.SelfAmount) || 0.00;
                        level2Total += parseFloat(level3.TotalAmount) || 0.00;
                    });
                }

                level2.GrandAmount = level2Grand.toFixed(2);
                level2.SelfAmount = level2Self.toFixed(2);
                level2.TotalAmount = level2Total.toFixed(2);
                // END LEVEL 2

                var level1Grand = 0.00;
                var level1Self = 0.00;
                var level1Total = 0.00;

                if (!level1.IsDeactivated) {
                    level1.BFPContractProgrammeExpenseBudgetCollection.forEach(function (level2) {
                        level1Grand += parseFloat(level2.GrandAmount) || 0.00;
                        level1Self += parseFloat(level2.SelfAmount) || 0.00;
                        level1Total += parseFloat(level2.TotalAmount) || 0.00;
                    });
                }

                level1.GrandAmount = level1Grand.toFixed(2);
                level1.SelfAmount = level1Self.toFixed(2);
                level1.TotalAmount = level1Total.toFixed(2);

                $scope.updateGlobalTotals();
            };

            $scope.updateGlobalTotals = function () {
                var budgetGrand = 0.00;
                var budgetSelf = 0.00;
                var budgetTotal = 0.00;

                $scope.eachLevel1(function (level1) {
                    budgetGrand += parseFloat(level1.GrandAmount) || 0.00;
                    budgetSelf += parseFloat(level1.SelfAmount) || 0.00;
                    budgetTotal += parseFloat(level1.TotalAmount) || 0.00;
                });

                $scope.triple.BFPContractBudget.GrandAmount = budgetGrand.toFixed(2);
                $scope.triple.BFPContractBudget.SelfAmount = budgetSelf.toFixed(2);
                $scope.triple.BFPContractBudget.TotalAmount = budgetTotal.toFixed(2);
            };

            $scope.updateContract = function () {

                var _eligible_grand_total = 0.00;
                var _eligible_self_total = 0.00;
                var _ineligible_grand_total = 0.00;
                var _ineligible_self_total = 0.00;

                function innerParseFloat(value) {
                    var p = parseFloat(value);
                    if (isNaN(p))
                        return 0.00;
                    else return p;
                }

                $scope.triple.BFPContractBudget.BFPContractProgrammeBudgetCollection.forEach(function (level1) {
                    level1.BFPContractProgrammeExpenseBudgetCollection.forEach(function (level2) {
                        level2.BFPContractProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
                            if (!level1.IsDeactivated && !level2.IsDeactivated) {
                                _eligible_grand_total += innerParseFloat(level3.GrandAmount);
                                _eligible_self_total += innerParseFloat(level3.SelfAmount);
                            }
                        })
                    })
                });

                //Искано финансиране (Безвъзмездна финансова помощ)
                $scope.triple.Contract.RequestedFundingAmount = _eligible_grand_total + _ineligible_grand_total;

                //Общо съфинансиране
                $scope.triple.Contract.TotalCoFinancingAmount = _eligible_self_total + _ineligible_self_total;

                //Общо допустими разходи
                $scope.triple.Contract.TotalEligibleCosts = _eligible_grand_total + _eligible_self_total;

                //Съотношение Безвъзмездна финансова помощ към Общо допустими разходи
                var ratio = $scope.triple.Contract.TotalEligibleCosts / $scope.triple.Contract.RequestedFundingAmount;
                $scope.triple.Contract.RatioRequestedFundingTotalEligibleCosts = (!isFinite(ratio) || ratio <= 0) ? '' : (100 / ratio).toFixed(2) + ' %';

                //Недопустими разходи, необходими за изпълнението на проекта (когато е приложимо)
                $scope.triple.Contract.IneligibleCosts = _ineligible_grand_total + _ineligible_self_total;

                //Обща стойност на проектното предложение
                $scope.triple.Contract.TotalProjectCost = $scope.triple.Contract.TotalEligibleCosts + $scope.triple.Contract.IneligibleCosts;

                // toFixed(2)
                $scope.triple.Contract.RequestedFundingAmount = $scope.triple.Contract.RequestedFundingAmount.toFixed(2);
                $scope.triple.Contract.TotalCoFinancingAmount = $scope.triple.Contract.TotalCoFinancingAmount.toFixed(2);
                $scope.triple.Contract.TotalEligibleCosts = $scope.triple.Contract.TotalEligibleCosts.toFixed(2);
                $scope.triple.Contract.IneligibleCosts = $scope.triple.Contract.IneligibleCosts.toFixed(2);
                $scope.triple.Contract.TotalProjectCost = $scope.triple.Contract.TotalProjectCost.toFixed(2);
            };
                // ***** END CALCULATIONS *****

            // ***** COMMON *****
            $scope.eachLevel3 = function (callback) {
                $scope.eachLevel2(function (level2) {
                    level2.BFPContractProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
                        if (angular.isDefined(level3)) {
                            callback(level3);
                        }
                    })
                });
            }

            $scope.eachLevel2 = function (callback) {
                $scope.eachLevel1(function (level1) {
                    level1.BFPContractProgrammeExpenseBudgetCollection.forEach(function (level2) {
                        callback(level2);
                    })
                })
            }

            $scope.eachLevel1 = function (callback) {
                $scope.triple.BFPContractBudget.BFPContractProgrammeBudgetCollection.forEach(function (level1) {
                    callback(level1);
                });
            }

            $scope.addItem = function (index1, index2) {
                $scope.triple.BFPContractBudget
                    .BFPContractProgrammeBudgetCollection[index1]
                    .BFPContractProgrammeExpenseBudgetCollection[index2]
                    .BFPContractProgrammeDetailsExpenseBudgetCollection.push({
                        IsNameValid: true,
                        IsGrandAmountValid: true,
                        IsSelfAmountValid: true,
                        IsTotalAmountValid: true,
                        IsDirectionValid: true,

                        IsNutsValid: true,
                        gid: rfc4122.newuuid(),
                        isActive: true
                    });

                $scope.updateOrderNums(index1, index2);

                $scope.updateNutsAddresses();
            }

            $scope.delItem = function (index1, index2, index3) {
                $scope.triple.BFPContractBudget
                    .BFPContractProgrammeBudgetCollection[index1]
                    .BFPContractProgrammeExpenseBudgetCollection[index2]
                    .BFPContractProgrammeDetailsExpenseBudgetCollection.splice(index3, 1);
                $scope.updateOrderNums(index1, index2);
            }

            $scope.updateOrderNums = function (index1, index2) {
                $scope.triple.BFPContractBudget
                    .BFPContractProgrammeBudgetCollection[index1]
                    .BFPContractProgrammeExpenseBudgetCollection[index2]
                    .BFPContractProgrammeDetailsExpenseBudgetCollection.forEach(function (level3, i) {
                        level3.OrderNum = i + 1;
                    });
            };

            $scope.romanize = function (num) {
                return $romanize.convert(num);
            }

            $scope.setCountValidity = function (currentLevel2) {
                var totalCount = 0;

                $scope.eachLevel2(function (level2) {
                    totalCount += level2.BFPContractProgrammeDetailsExpenseBudgetCollection.length;
                });

                currentLevel2.showTotalError = totalCount >= $window[$scope.globalKey]['constants'].BudgetMaxLevel3ItemsTotal;
                currentLevel2.showCurrentError = currentLevel2.BFPContractProgrammeDetailsExpenseBudgetCollection.length >= $window[$scope.globalKey]['constants'].BudgetMaxLevel3Items;

                return !currentLevel2.showTotalError && !currentLevel2.showCurrentError;
            }

            // ***** DIRECTIONS *****

            $scope.toNomenclature = function (i) {
                if (i) {
                    return {
                        id: i.id,
                        text: i.Name,
                        Name: i.Name,
                        NameEN: i.NameEN
                    }
                }
                return undefined;
            }

            $scope.addDirection = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsNameValid: true,

                    id: rfc4122.newuuid(),
                    isActive: true,
                    isActivated: false,
                    IsDirectionValid: true
                }

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delDirection = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
                $scope.removeObsoleteDirection();
            }

            $scope.uniqueDirection = function (item) {
                return item.id === $scope.currentDirection.id;
            }

            $scope.loadDirectionNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.directionNomenclature, function () {
                    $scope.currentDirection = this.direction;
                    if (data.results.filter($scope.uniqueDirection).length === 0) {
                        if (query.term.length == 0 || this.direction.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                            data.results.push(this.direction);
                        }
                    }
                });
                query.callback(data);
            };

            $scope.loadSubDirectionNomenclature = function (item) {
                return {
                    allowClear: true,
                    placeholder: ' ',
                    query: function (query) {
                        var data = { results: [] };

                        if (item.DirectionItem) {
                            filtered = $scope.directionNomenclature.filter(function (x) {
                                return x.direction.id === item.DirectionItem.id
                            });

                            for (let m = 0; m < filtered.length; m++) {
                                if (filtered[m].subDirection) {
                                    data.results.push(filtered[m].subDirection);
                                }
                            }
                        }
                        query.callback(data);
                    }
                };
            }

            $scope.clearSubDirection = function (item) {
                item.SubDirection = undefined;
                $scope.removeObsoleteDirection();
            }

            // ***** END DIRECTIONS *****

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])


.controller('controllerBFPContractContractActivities',
        ['$scope', '$filter', '$timeout', '$window', 'BFPContract', 'rfc4122',
        function ($scope, $filter, $timeout, $window, BFPContract, rfc4122) {

            if (BFPContract.BFPContractContractActivities.BFPContractContractActivityCollection == undefined) {
                BFPContract.BFPContractContractActivities.BFPContractContractActivityCollection = [];
            }

            $scope.items = BFPContract.BFPContractContractActivities.BFPContractContractActivityCollection;
            $scope.isValid = BFPContract.BFPContractContractActivities.IsValid;

            $scope.maxDiagramMonth = 100;

            $scope.items.forEach(function (item) {
                item.StartMonth = parseInt(item.StartMonth);
                item.Duration = parseInt(item.Duration);
                item.isPeriodValid = item.IsPeriodValid;
            });

            $scope.addItem = function () {
                var selectedCandidate = $window[$scope.globalKey]['_selected_candidate'];
                var isSelectedCandidate = selectedCandidate != null && selectedCandidate.id != null && selectedCandidate.text != null;

                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsCompanyValid: true,
                    IsCodeValid: true,
                    IsNameValid: true,
                    IsExecutionMethodValid: true,
                    IsResultValid: true,
                    IsStartMonthValid: true,
                    IsDurationValid: true,
                    IsAmountValid: true,
                    CompanyCollection: isSelectedCandidate ? [{ id: selectedCandidate.id, text: selectedCandidate.text, Name: selectedCandidate.Name, NameEN: selectedCandidate.NameEN }] : [],

                    gid: rfc4122.newuuid(),
                    isActive: true,
                    isActivated: false,
                    isPeriodValid: true
                };

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            }

            $scope.loadCompanyNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.companyNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.$watch(function () {
                return BFPContract.Beneficiary;
            }, function () {
                if (BFPContract.Beneficiary.Name && BFPContract.Beneficiary.Uin) {
                    $window[$scope.globalKey]['_selected_candidate'] = { id: BFPContract.Beneficiary.Uin, text: BFPContract.Beneficiary.Name, Name: BFPContract.Beneficiary.Name, NameEN: BFPContract.Beneficiary.NameEN };
                }
                else {
                    $window[$scope.globalKey]['_selected_candidate'] = null;
                }

                refreshContractActivities();
            }, true);

            $scope.$watch(function () {
                return BFPContract.Partners.PartnerCollection;
            }, function () {

                var partners = [];

                BFPContract.Partners.PartnerCollection.forEach(function (partner) {
                    if (partner.Name && partner.Uin) {
                        partners.push({ id: partner.Uin, text: partner.Name, Name: partner.Name });
                    }
                });

                $window[$scope.globalKey]['_selected_partners'] = partners;

                refreshContractActivities();

            }, true);

            var refreshContractActivities = function () {
                $scope.updateCompanyNomenclature();
                $scope.updateCompanyCollection();
            };

            $scope.updateCompanyNomenclature = function () {
                $scope.companyNomenclature = [];

                if ($window[$scope.globalKey]['_selected_candidate']) {
                    $scope.companyNomenclature.push($window[$scope.globalKey]['_selected_candidate']);
                }

                var partners = $window[$scope.globalKey]['_selected_partners'] || [];
                partners.forEach(function (partner) {
                    var map = $.map($scope.companyNomenclature, function (val) { return val.id; });
                    if (map.indexOf(partner.id) == -1) {
                        $scope.companyNomenclature.push(partner);
                    }
                });
            };

            $scope.updateCompanyCollection = function () {
                var selectedCandidate = $window[$scope.globalKey]['_selected_candidate'];

                $scope.items.forEach(function (item) {
                    if (angular.isDefined(item.CompanyCollection) && item.CompanyCollection !== null && item.CompanyCollection.length > 0) {
                        var mapIds = $.map($scope.companyNomenclature, function (val) { return val.id; });
                        var mapNames = $.map($scope.companyNomenclature, function (val) { return val.text; });

                        item.CompanyCollection.forEach(function (selected) {
                            if (mapIds.indexOf(selected.id) == -1
                                || mapNames.indexOf(selected.text) == -1) {
                                var index = item.CompanyCollection.indexOf(selected);
                                item.CompanyCollection.splice(index, 1);
                            }
                        });
                    }


                    if (selectedCandidate && selectedCandidate.id && selectedCandidate.text
                        && !(angular.isDefined(item.CompanyCollection) && item.CompanyCollection !== null && item.CompanyCollection.length > 0)) {
                        if (!item.CompanyCollection) {
                            item.CompanyCollection = [];
                        }

                        item.CompanyCollection.push({
                            id: selectedCandidate.id,
                            text: selectedCandidate.text,
                            Name: selectedCandidate.Name
                        });
                    }
                });
            };

            $scope.months = [1];

            $scope.$watch('items', function () {
                var maxMonth = 1;

                $scope.items.forEach(function (item) {
                    var startMonth = parseInt(item.StartMonth);
                    var duration = parseInt(item.Duration);
                    var currentEndMonth = 0;

                    if (!isNaN(startMonth) && !isNaN(duration))
                        currentEndMonth = startMonth + duration;

                    if (currentEndMonth > maxMonth && currentEndMonth <= $scope.maxDiagramMonth) {
                        maxMonth = currentEndMonth;
                    }
                });

                if (maxMonth > 1 && maxMonth != $scope.months.length) {
                    $scope.months = new Array(maxMonth - 1);

                    for (var i = 0; i < $scope.months.length; i++)
                        $scope.months[i] = (i + 1);
                }
            }, true)

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
        .controller('controllerIndicators',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'BFPContract',
        function ($scope, $filter, $timeout, $window, rfc4122, BFPContract) {

            if (BFPContract.BFPContractIndicators.BFPContractIndicatorCollection == undefined) {
                BFPContract.BFPContractIndicators.BFPContractIndicatorCollection = [];
            }

            $scope.hasUniqueIds = BFPContract.BFPContractIndicators.HasUniqueIds;
            $scope.isValid = BFPContract.BFPContractIndicators.IsValid;
            $scope.items = BFPContract.BFPContractIndicators.BFPContractIndicatorCollection;

            $scope.indicatorNomenclature = (BFPContract.BFPContractIndicators.Items || []).map(function (indicator) {
                return {
                    id: indicator.id,
                    text: indicator.Name,
                    Name: indicator.Name,
                    TypeName: indicator.TypeName,
                    TrendName: indicator.TrendName,
                    KindName: indicator.KindName,
                    MeasureName: indicator.MeasureName,

                    AggregatedReport: indicator.AggregatedReport,
                    AggregatedTarget: indicator.AggregatedTarget,

                    HasGenderDivision: indicator.HasGenderDivision
                };
            });

            var findMatchingIndicator = function (indicator) {
                if (indicator.SelectedIndicator != null) {
                    var found = $filter('filter')($scope.indicatorNomenclature, { id: indicator.SelectedIndicator.id }, true);
                    if (found.length > 0) {
                        return found[0];
                    }
                }

                return null;
            }

            $scope.items.forEach(function (item) {
                var procIndicator = findMatchingIndicator(item);
                
                if(procIndicator != null) {
                    item.isDataChanged = 
                        item.SelectedIndicator.id != procIndicator.id
                     || item.SelectedIndicator.Name != procIndicator.Name
                     || item.SelectedIndicator.TypeName != procIndicator.TypeName
                     || item.SelectedIndicator.TrendName != procIndicator.TrendName
                     || item.SelectedIndicator.KindName != procIndicator.KindName
                     || item.SelectedIndicator.MeasureName != procIndicator.MeasureName
                     || item.SelectedIndicator.AggregatedReport != procIndicator.AggregatedReport
                     || item.SelectedIndicator.AggregatedTarget != procIndicator.AggregatedTarget
                     || item.SelectedIndicator.HasGenderDivision != procIndicator.HasGenderDivision;
                }
            });

            $scope.updateDataFromProcedure = function (item) {
                var procIndicator = findMatchingIndicator(item);

                if (procIndicator != null && item.isDataChanged) {
                    item.SelectedIndicator.text = procIndicator.Name;
                    item.SelectedIndicator.Name = procIndicator.Name;
                    item.SelectedIndicator.TypeName = procIndicator.TypeName;
                    item.SelectedIndicator.TrendName = procIndicator.TrendName;
                    item.SelectedIndicator.KindName = procIndicator.KindName;
                    item.SelectedIndicator.MeasureName = procIndicator.MeasureName;
                    item.SelectedIndicator.AggregatedReport = procIndicator.AggregatedReport;
                    item.SelectedIndicator.AggregatedTarget = procIndicator.AggregatedTarget;
                    item.SelectedIndicator.HasGenderDivision = procIndicator.HasGenderDivision;
                }

                item.isDataChanged = false;
            }

            $scope.$watch('items', function () {
                $scope.updateItemsTotals();
            }, true);

            $scope.updateItemsTotals = function () {
                $scope.items.forEach(function (item) {
                    if (item.SelectedIndicator != null
                         && item.SelectedIndicator.HasGenderDivision) {
                        item.BaseTotal = (((1 * item.BaseMen) || 0) + ((1 * item.BaseWomen) || 0)).toFixed(2);
                        item.TargetTotal = (((1 * item.TargetMen) || 0) + ((1 * item.TargetWomen) || 0)).toFixed(2);
                    }
                });
            }

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsNameValid: true,
                    IsBaseMenValid: true,
                    IsBaseWomenValid: true,
                    IsBaseValid: true,
                    IsTargetMenValid: true,
                    IsTargetWomenValid: true,
                    IsTargetValid: true,
                    IsDescriptionValid: true,

                    gid: rfc4122.newuuid(),
                    isActive: true,
                    isActivated: false
                }

                $scope.items.push(item);

                $timeout(function () {
                    $("#" + item.editTriggerId).click();
                }, 50);
            }

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            }

            $scope.clearBaseTarget = function (item) {
                var defaultValue = '0.00';

                item.BaseMen = defaultValue;
                item.BaseWomen = defaultValue;
                item.BaseTotal = defaultValue;

                item.TargetMen = defaultValue;
                item.TargetWomen = defaultValue;
                item.TargetTotal = defaultValue;
            }

            $scope.loadIndicatorNomenclature = function (query) {
                var data = { results: [] };
                $.each($scope.indicatorNomenclature, function () {
                    if (query.term.length == 0 || this.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                        data.results.push(this);
                    }
                });
                query.callback(data);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
        .controller('controllerContractTeams',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'BFPContract',
        function ($scope, $filter, $timeout, $window, rfc4122, BFPContract) {

            $scope.items = BFPContract.BFPContractContractTeams.BFPContractContractTeamCollection;
            $scope.isValid = BFPContract.BFPContractContractTeams.IsValid;

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsNameValid: true,
                    IsPositionValid: true,
                    IsResponsibilitiesValid: true,
                    IsPhoneValid: true,
                    IsEmailValid: true,
                    IsFaxValid: true
                }

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
        .controller('controllerPlans',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'BFPContract',
        function ($scope, $filter, $timeout, $window, rfc4122, BFPContract) {

            $scope.items = BFPContract.BFPContractPlans.BFPContractPlanCollection;

            $scope.addItem = function () {
                var item = {
                    editTriggerId: rfc4122.newuuid(),
                    IsNameValid: true,
                    IsErrandAreaValid: true,
                    IsErrandLegalActValid: true,
                    IsErrandTypeValid: true,
                    IsAmountValid: true,
                    IsPlanDateValid: true,
                    IsDescriptionValid: true
                }

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
        .controller('controllerAttachedDocuments',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'BFPContract', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, rfc4122, BFPContract, AttachedDocumentsInfo) {

            $scope.items = BFPContract.AttachedDocuments.AttachedDocumentCollection;
            $scope.hasValidCount = BFPContract.AttachedDocuments.HasValidCount;
            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;

            $scope.items.forEach(function (item) {
                if (item.AttachedDocumentContent == undefined) {
                    item.AttachedDocumentContent = {};
                }
            });

            $scope.addItem = function () {
                var item = {
                    IsTypeValid: true,
                    IsDescriptionValid: true,
                    AttachedDocumentContent: {
                        IsDocumentValid: true
                    }
                };

                $scope.items.push(item);
            };

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }])
        .controller('controllerSignedContracts',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'BFPContract', 'AttachedDocumentsInfo',
        function ($scope, $filter, $timeout, $window, rfc4122, BFPContract, AttachedDocumentsInfo) {

            $scope.items = BFPContract.SignedContracts.SignedContractCollection;
            $scope.hasValidCount = BFPContract.SignedContracts.HasValidCount;
            $scope.resourcesObject = AttachedDocumentsInfo.resourcesAttachedDocuments;
            $scope.url = AttachedDocumentsInfo.blobUrl;

            $scope.items.forEach(function (item) {
                if (item.AttachedDocumentContent == undefined) {
                    item.AttachedDocumentContent = {};
                }
            });

            $scope.addItem = function () {
                var item = {
                    IsDescriptionValid: true,
                    AttachedDocumentContent: {
                        IsDocumentValid: true
                    }
                };

                $scope.items.push(item);
            };

            $scope.delItem = function (item) {
                $scope.items.splice($scope.items.indexOf(item), 1);
            };

            $scope.$evalAsync(function () {
                $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
            });
        }]);

