angular.module('moduleEvalSheetGroups', ['scaffolding', 'utils'])
    .controller('controllerEvalSheetGroups',
        ['$scope', '$filter', '$window', 'romanize',
            function ($scope, $filter, $window, $romanize) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;

                    $scope.groups = $window[globalKey][parentKey].model.EvalSheetGroupCollection;

                    $scope.type = $window[globalKey][parentKey].model.Type;
                    $scope.limit = $window[globalKey][parentKey].model.Limit;
                    $scope.total = $window[globalKey][parentKey].model.Total;
                    $scope.isTotalValid = $window[globalKey][parentKey].model.IsTotalValid;
                    $scope.WeightTotal = $window[globalKey][parentKey].model.WeightTotal;

                    $scope.reasonManual = $window[globalKey][parentKey].model.ReasonManual;
                    $scope.isManual = $window[globalKey][parentKey].model.IsManual;
                    $scope.isSuccess = $window[globalKey][parentKey].model.IsSuccess;

                    $scope.weightType = $window[globalKey][parentKey].weightType;
                    $scope.rejectionType = $window[globalKey][parentKey].rejectionType;
                    $scope.noId = $window[globalKey][parentKey].noId;

                    $scope.acceptances = $window[globalKey][parentKey].acceptances;

                    $scope.eachCriteria = function (criteria) {
                        if (!criteria.Accept) {
                            criteria.Accept = { Id: "", Name: "" }
                        }
                    };
                };

                $scope.$watch('groups', function () {
                    $scope.updateTotals();
                    $scope.updateSuccess();
                }, true);

                $scope.$watch('isManual', function () {
                    $scope.updateSuccess();
                }, true);

                $scope.eachCriteria = function (callback) {
                    $scope.eachGroup(function (group) {
                        group.EvalSheetCriteriaCollection.forEach(function (criteria) {
                            callback(criteria);
                        })
                    })
                }

                $scope.eachGroup = function (callback) {
                    $scope.groups.forEach(function (group) {
                        callback(group);
                    });
                }

                $scope.assignAcceptance = function (criteria, acceptance) {
                    if (!criteria.Accept) {
                        criteria.Accept = {};
                    }

                    criteria.Accept.Name = acceptance.Name;
                }

                $scope.updateTotals = function () {
                    var total = 0.00;
                    $scope.eachGroup(function (group) {
                        var groupTotal = 0.00;

                        group.EvalSheetCriteriaCollection.forEach(function (criteria) {
                            var parsedCriteriaTotal = (1 * criteria.Evaluation) || 0;
                            if (parsedCriteriaTotal) {
                                groupTotal += parsedCriteriaTotal;
                            }
                        })

                        group.Total = groupTotal.toFixed(2);
                        total += groupTotal;
                    })
                    $scope.total = total.toFixed(2);
                }

                $scope.updateSuccess = function () {
                    if ($scope.type == $scope.weightType) {
                        var totalSuccess = true;
                        $scope.groups.forEach(function (group) {
                            var groupSuccess = ((1 * group.Limit) || 0) <= ((1 * group.Total) || 0);

                            group.isSuccess = groupSuccess;
                            totalSuccess = totalSuccess && groupSuccess;
                        });

                        totalSuccess = totalSuccess && ((1 * $scope.limit) || 0) <= ((1 * $scope.total) || 0)

                        if (!$scope.isManual) {
                            $scope.isSuccess = totalSuccess;
                        }
                    }

                    if ($scope.type == $scope.rejectionType) {
                        var totalSuccess = true;
                        $scope.groups.forEach(function (group) {
                            var groupSuccess = true;

                            group.EvalSheetCriteriaCollection.forEach(function (criteria) {
                                groupSuccess = groupSuccess &&
                                    ((criteria.Accept != null && criteria.Accept.id != null && criteria.Accept.id != $scope.noId)
                                    || !criteria.EvalTableCriteria.IsRejection);
                            })

                            group.isSuccess = groupSuccess;
                            totalSuccess = totalSuccess && groupSuccess;
                        });
                        
                        if (!$scope.isManual) {
                            $scope.isSuccess = totalSuccess;
                        }
                    }
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });

                $scope.romanize = function (num) {
                    return $romanize.convert(num);
                }

            }]);