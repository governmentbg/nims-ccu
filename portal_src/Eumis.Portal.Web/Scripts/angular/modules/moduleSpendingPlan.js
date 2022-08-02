angular.module('moduleSpendingPlan', ['scaffolding'])
    .factory('SpendingPlan', ['$window', function ($window) {
        //return $resource('/api/' + route + '/appcontext/:alias', {}, {});
        return $window['_eumis_options'].SpendingPlan;
    }])
    .controller('controllerMainSpendingPlan',
            ['$scope', '$filter', '$timeout', '$window', 'SpendingPlan', 'appcontext',
            function ($scope, $filter, $timeout, $window, SpendingPlan, appcontext) {
                $scope.globalKey = '_eumis_options';

                $scope.SpendingPlan = SpendingPlan;
                $scope.budget = SpendingPlan.SpendingBudget;
                $scope.items = SpendingPlan.SpendingBudget.SpendingBudgetLevel1Collection;

                $scope.items.forEach(function (level1) {
                    $scope.$watch(
                        function () { return level1.QuarterlyDistributionCollection; }
                        , function () {
                            updateTotals();
                        }
                        , true
                    );
                });

                var updateTotals = function () {
                    // clear total values
                    $scope.budget.TotalCalculatedAmount = 0.00;
                    $scope.budget.QuarterlyDistributionCollection.forEach(function (quarter, index) {
                        quarter.Q1Amount = 0.00;
                        quarter.Q2Amount = 0.00;
                        quarter.Q3Amount = 0.00;
                        quarter.Q4Amount = 0.00;
                    });
                    
                    $scope.items.forEach(function (level1) {
                        level1.TotalCalculatedAmount = 0.00;

                        level1.QuarterlyDistributionCollection.forEach(function (quarter, index) {
                            // Q1
                            level1.TotalCalculatedAmount = (((1 * level1.TotalCalculatedAmount) || 0) + ((1 * quarter.Q1Amount) || 0)).toFixed(2);
                            $scope.budget.TotalCalculatedAmount = (((1 * $scope.budget.TotalCalculatedAmount) || 0) + ((1 * quarter.Q1Amount) || 0)).toFixed(2);
                            $scope.budget.QuarterlyDistributionCollection[index].Q1Amount = (((1 * $scope.budget.QuarterlyDistributionCollection[index].Q1Amount) || 0) + ((1 * quarter.Q1Amount) || 0)).toFixed(2);

                            // Q2
                            level1.TotalCalculatedAmount = (((1 * level1.TotalCalculatedAmount) || 0) + ((1 * quarter.Q2Amount) || 0)).toFixed(2);
                            $scope.budget.TotalCalculatedAmount = (((1 * $scope.budget.TotalCalculatedAmount) || 0) + ((1 * quarter.Q2Amount) || 0)).toFixed(2);
                            $scope.budget.QuarterlyDistributionCollection[index].Q2Amount = (((1 * $scope.budget.QuarterlyDistributionCollection[index].Q2Amount) || 0) + ((1 * quarter.Q2Amount) || 0)).toFixed(2);

                            // Q3
                            level1.TotalCalculatedAmount = (((1 * level1.TotalCalculatedAmount) || 0) + ((1 * quarter.Q3Amount) || 0)).toFixed(2);
                            $scope.budget.TotalCalculatedAmount = (((1 * $scope.budget.TotalCalculatedAmount) || 0) + ((1 * quarter.Q3Amount) || 0)).toFixed(2);
                            $scope.budget.QuarterlyDistributionCollection[index].Q3Amount = (((1 * $scope.budget.QuarterlyDistributionCollection[index].Q3Amount) || 0) + ((1 * quarter.Q3Amount) || 0)).toFixed(2);

                            // Q4
                            level1.TotalCalculatedAmount = (((1 * level1.TotalCalculatedAmount) || 0) + ((1 * quarter.Q4Amount) || 0)).toFixed(2);
                            $scope.budget.TotalCalculatedAmount = (((1 * $scope.budget.TotalCalculatedAmount) || 0) + ((1 * quarter.Q4Amount) || 0)).toFixed(2);
                            $scope.budget.QuarterlyDistributionCollection[index].Q4Amount = (((1 * $scope.budget.QuarterlyDistributionCollection[index].Q4Amount) || 0) + ((1 * quarter.Q4Amount) || 0)).toFixed(2);
                        });
                    });
                };

                $scope.$on('spendingPlanActivation', function (event, args) {
                    if (!!args.update) {
                        appcontext.save(args.d, 'SaveSpendingPlan', $scope.SpendingPlan, {});
                    }
                });

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }])


