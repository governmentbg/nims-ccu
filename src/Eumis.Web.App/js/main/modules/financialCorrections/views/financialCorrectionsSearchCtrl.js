function FinancialCorrectionsSearchCtrl($scope, $stateParams, financialCorrections) {
  $scope.financialCorrections = financialCorrections;
}

FinancialCorrectionsSearchCtrl.$inject = ['$scope', '$stateParams', 'financialCorrections'];

FinancialCorrectionsSearchCtrl.$resolve = {
  financialCorrections: [
    'FinancialCorrection',
    function(FinancialCorrection) {
      return FinancialCorrection.query().$promise;
    }
  ]
};

export { FinancialCorrectionsSearchCtrl };
