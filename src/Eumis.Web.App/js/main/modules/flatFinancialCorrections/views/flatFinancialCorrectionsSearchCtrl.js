function FlatFinancialCorrectionsSearchCtrl($scope, $stateParams, flatFinancialCorrections) {
  $scope.flatFinancialCorrections = flatFinancialCorrections;
}

FlatFinancialCorrectionsSearchCtrl.$inject = ['$scope', '$stateParams', 'flatFinancialCorrections'];

FlatFinancialCorrectionsSearchCtrl.$resolve = {
  flatFinancialCorrections: [
    'FlatFinancialCorrection',
    function(FlatFinancialCorrection) {
      return FlatFinancialCorrection.query().$promise;
    }
  ]
};

export { FlatFinancialCorrectionsSearchCtrl };
