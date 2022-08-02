function CorrectionDebtsNewStep2Ctrl(
  $scope,
  $state,
  scConfirm,
  CorrectionDebt,
  newCorrectionDebt,
  flatFinancialCorrectionInfo
) {
  $scope.flatFinancialCorrectionInfo = flatFinancialCorrectionInfo;
  $scope.newCorrectionDebt = newCorrectionDebt;

  $scope.save = function() {
    return $scope.correctionDebtsNewStep2Form.$validate().then(function() {
      if ($scope.correctionDebtsNewStep2Form.$valid) {
        return CorrectionDebt.save($scope.newCorrectionDebt).$promise.then(function(result) {
          return $state.go('root.correctionDebts.view.edit', {
            id: result.correctionDebtId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.correctionDebts.search');
  };
}

CorrectionDebtsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  'scConfirm',
  'CorrectionDebt',
  'newCorrectionDebt',
  'flatFinancialCorrectionInfo'
];

CorrectionDebtsNewStep2Ctrl.$resolve = {
  newCorrectionDebt: [
    'CorrectionDebt',
    '$stateParams',
    function(CorrectionDebt, $stateParams) {
      return CorrectionDebt.newCorrectionDebt({
        flatFinancialCorrectionId: $stateParams.cId
      }).$promise;
    }
  ],
  flatFinancialCorrectionInfo: [
    'FlatFinancialCorrection',
    '$stateParams',
    function(FlatFinancialCorrection, $stateParams) {
      return FlatFinancialCorrection.getCorrectionDebtInfo({
        id: $stateParams.cId
      }).$promise;
    }
  ]
};

export { CorrectionDebtsNewStep2Ctrl };
