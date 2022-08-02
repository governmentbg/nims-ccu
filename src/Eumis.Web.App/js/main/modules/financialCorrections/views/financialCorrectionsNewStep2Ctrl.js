function FinancialCorrectionsNewStep2Ctrl(
  $scope,
  $state,
  scConfirm,
  FinancialCorrection,
  newFinancialCorrection
) {
  $scope.newFinancialCorrection = newFinancialCorrection;

  $scope.save = function() {
    return $scope.financialCorrectionsNewStep2Form.$validate().then(function() {
      if ($scope.financialCorrectionsNewStep2Form.$valid) {
        return FinancialCorrection.save($scope.newFinancialCorrection).$promise.then(function(
          result
        ) {
          return $state.go('root.financialCorrections.view.edit', {
            id: result.financialCorrectionId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.financialCorrections.search');
  };

  $scope.contractContractChanged = function() {
    $scope.newFinancialCorrection.contractBudgetLevel3AmountId = null;
  };
}

FinancialCorrectionsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  'scConfirm',
  'FinancialCorrection',
  'newFinancialCorrection'
];

FinancialCorrectionsNewStep2Ctrl.$resolve = {
  newFinancialCorrection: [
    'FinancialCorrection',
    '$stateParams',
    function(FinancialCorrection, $stateParams) {
      return FinancialCorrection.newFinancialCorrection({
        contractNum: $stateParams.cNum
      }).$promise;
    }
  ]
};

export { FinancialCorrectionsNewStep2Ctrl };
