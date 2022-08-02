function FinancialCorrectionsViewCtrl($scope, financialCorrectionInfo) {
  $scope.financialCorrectionInfo = financialCorrectionInfo;
}

FinancialCorrectionsViewCtrl.$inject = ['$scope', 'financialCorrectionInfo'];

FinancialCorrectionsViewCtrl.$resolve = {
  financialCorrectionInfo: [
    'FinancialCorrection',
    '$stateParams',
    function(FinancialCorrection, $stateParams) {
      return FinancialCorrection.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { FinancialCorrectionsViewCtrl };
