angular.module('moduleDirectionsBudgetContract', ['scaffolding', 'utils'])
    .controller('controllerDirectionsBudgetContract',
        ['$scope', '$filter', '$window', 'romanize', '$timeout', '$http', 'appcontext', 'rfc4122',
            function ($scope, $filter, $window, $romanize, $timeout, $http, appcontext, rfc4122) {

                var _emptyNutsAddress = {
                    id: "",
                    text: "",
                    Code: "",
                    Name: "",
                    FullPath: "",
                    FullPathName: ""
                };

                // ***** INIT *****
                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.triple = $window[globalKey][parentKey].triple;

                    $scope.initWatches();
                    $scope.initActivations();
                    $scope.initDirections();
                };

                $scope.initActivations = function () {
                    $scope.$on('budgetActivation', function (event, args) {
                        if (!!args.update) {
                            appcontext.save(args.d, 'UpdateBudget', $scope.triple.Budget, { index: $scope.triple.Index });
                        } else {
                            $scope.$apply(function () {
                                $scope.triple.Budget.IsActive = !!args.isActive;
                            });
                        }
                    });
                };

                $scope.initWatches = function () {
                    $scope.triple.Budget.ProgrammeBudgetCollection.forEach(function (level1, i) {
                        level1.ProgrammeExpenseBudgetCollection.forEach(function (level2, j) {
                            $scope.$watch('triple.Budget.ProgrammeBudgetCollection[' + i + '].ProgrammeExpenseBudgetCollection[' + j + ']', function () {
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
                        , function () {
                            $scope.SelectedNutsAddresses = $window[$scope.globalKey]['_selected_nuts_addresses'] || [];
                            $scope.hasMoreThanOneNutsAddress = $scope.SelectedNutsAddresses.length >= 2;
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

                // ***** DIRECTIONS / NUTS ADDRESSES *****

                $scope.$on('triggerBroadcast', function (event, args) {
                    $scope.$apply(function () {
                        $scope.updateNutsAddresses();
                        $scope.updateDirections();
                    });
                });

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
                            level3.Direction= undefined;
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
                // ***** END DIRECTIONS / NUTS ADDRESSES *****

                // ***** CALCULATIONS *****
                $scope.updatePercentages = function (level2) {
                    if (level2.isTotalSumActive) {
                        level2.ProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
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

                        level2.ProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {

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
                        level2.ProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
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
                        level1.ProgrammeExpenseBudgetCollection.forEach(function (level2) {
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

                    $scope.triple.Budget.GrandAmount = budgetGrand.toFixed(2);
                    $scope.triple.Budget.SelfAmount = budgetSelf.toFixed(2);
                    $scope.triple.Budget.TotalAmount = budgetTotal.toFixed(2);
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

                    $scope.triple.Budget.ProgrammeBudgetCollection.forEach(function (level1) {
                        level1.ProgrammeExpenseBudgetCollection.forEach(function (level2) {
                            level2.ProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
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
                        level2.ProgrammeDetailsExpenseBudgetCollection.forEach(function (level3) {
                            if (angular.isDefined(level3)) {
                                callback(level3);
                            }
                        })
                    });
                }

                $scope.eachLevel2 = function (callback) {
                    $scope.eachLevel1(function (level1) {
                        level1.ProgrammeExpenseBudgetCollection.forEach(function (level2) {
                            callback(level2);
                        })
                    })
                }

                $scope.eachLevel1 = function (callback) {
                    $scope.triple.Budget.ProgrammeBudgetCollection.forEach(function (level1) {
                        callback(level1);
                    });
                }

                $scope.addItem = function (index1, index2) {
                    $scope.triple.Budget
                        .ProgrammeBudgetCollection[index1]
                        .ProgrammeExpenseBudgetCollection[index2]
                        .ProgrammeDetailsExpenseBudgetCollection.push({
                            IsNameValid: true,
                            IsGrandAmountValid: true,
                            IsSelfAmountValid: true,
                            IsTotalAmountValid: true,
                            IsDirectionValid: true,

                            IsNutsValid: true
                        });

                    $scope.updateOrderNums(index1, index2);

                    $scope.updateNutsAddresses();
                }

                $scope.delItem = function (index1, index2, index3) {
                    $scope.triple.Budget
                        .ProgrammeBudgetCollection[index1]
                        .ProgrammeExpenseBudgetCollection[index2]
                        .ProgrammeDetailsExpenseBudgetCollection.splice(index3, 1);
                    $scope.updateOrderNums(index1, index2);
                }

                $scope.updateOrderNums = function (index1, index2) {
                    $scope.triple.Budget
                        .ProgrammeBudgetCollection[index1]
                        .ProgrammeExpenseBudgetCollection[index2]
                        .ProgrammeDetailsExpenseBudgetCollection.forEach(function (level3, i) {
                            level3.OrderNum = i + 1;
                        });
                };

                $scope.romanize = function (num) {
                    return $romanize.convert(num);
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });

                $scope.setCountValidity = function (currentLevel2) {
                    var totalCount = 0;

                    $scope.eachLevel2(function (level2) {
                        totalCount += level2.ProgrammeDetailsExpenseBudgetCollection.length;
                    });

                    currentLevel2.showTotalError = totalCount >= $window[$scope.globalKey]['constants'].BudgetMaxLevel3ItemsTotal;
                    currentLevel2.showCurrentError = currentLevel2.ProgrammeDetailsExpenseBudgetCollection.length >= $window[$scope.globalKey]['constants'].BudgetMaxLevel3Items;

                    return !currentLevel2.showTotalError && !currentLevel2.showCurrentError;
                }
                // ***** END COMMON *****

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
            }]);