angular.module('moduleEvalTableGroups', ['scaffolding', 'utils'])
    .controller('controllerEvalTableGroups',
        ['$scope', '$filter', '$window', '$timeout', 'romanize',
            function ($scope, $filter, $window, $timeout, $romanize) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.limit = $window[globalKey][parentKey].model.Limit;
                    $scope.groups = $window[globalKey][parentKey].model.EvalTableGroupCollection;
                    $scope.type = $window[globalKey][parentKey].model.Type;
                    $scope.isLimitValid = $window[globalKey][parentKey].model.IsLimitValid;
                    $scope.hasGroups = $window[globalKey][parentKey].model.HasGroups;
                    $scope.weightType = $window[globalKey][parentKey].weightType;
                    $scope.rejectionType = $window[globalKey][parentKey].rejectionType;

                    if (!$scope.limit) {
                        $scope.limit = "60.00";
                    }

                    $scope.eachGroup(function (group) {
                        if (!group.Limit) {
                            group.Limit = "0.00";
                        }
                    });

                    $scope.$watch('groups', function () {
                        if ($scope.type == $scope.weightType) {
                            var modelTotal = 0.00;
                            $scope.groups.forEach(function (group) {
                                var groupTotal = 0.00;
                                group.EvalTableCriteriaCollection.forEach(function (criteria) {
                                    groupTotal += parseFloat(criteria.Weight) || 0.00;
                                })

                                group.WeightTotal = groupTotal.toFixed(2);
                                modelTotal += groupTotal;
                            });
                            $scope.WeightTotal = modelTotal.toFixed(2);
                        }
                    }, true);
                };

                $scope.eachCriteria = function (callback) {
                    $scope.eachGroup(function (group) {
                        group.EvalTableCriteriaCollection.forEach(function (criteria) {
                            callback(criteria);
                        })
                    })
                }

                $scope.eachGroup = function (callback) {
                    $scope.groups.forEach(function (group) {
                        callback(group);
                    });
                }

                $scope.addGroup = function () {
                    $scope.groups.push({
                        Limit: "0.00",
                        EvalTableCriteriaCollection: [],
                        IsNameValid: true,
                        IsLimitValid: true,
                        HasCriterias: true
                    });
                }

                $scope.delGroup = function (index) {
                    $scope.groups.splice(index, 1);
                }

                $scope.addCriteria = function (index) {
                    $scope.groups[index].EvalTableCriteriaCollection.push({
                        IsNameValid: true,
                        IsWeightValid: true,
                        IsRejection: true
                    });
                }

                $scope.delCriteria = function (index1, index2) {
                    $scope.groups[index1]
                        .EvalTableCriteriaCollection.splice(index2, 1);
                }

                $scope.romanize = function (num) {
                    return $romanize.convert(num);
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);