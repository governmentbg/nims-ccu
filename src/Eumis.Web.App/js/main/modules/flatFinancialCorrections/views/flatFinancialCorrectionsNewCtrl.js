function FlatFinancialCorrectionsNewCtrl(
  $scope,
  $state,
  FlatFinancialCorrection,
  newFlatFinancialCorrection
) {
  $scope.newFlatFinancialCorrection = newFlatFinancialCorrection;

  $scope.save = function() {
    return $scope.newFlatFinancialCorrectionForm.$validate().then(function() {
      if ($scope.newFlatFinancialCorrectionForm.$valid) {
        return FlatFinancialCorrection.save($scope.newFlatFinancialCorrection).$promise.then(
          function(result) {
            return $state.go('root.flatFinancialCorrections.view.edit', {
              id: result.flatFinancialCorrectionId
            });
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.flatFinancialCorrections.search');
  };
}

FlatFinancialCorrectionsNewCtrl.$inject = [
  '$scope',
  '$state',
  'FlatFinancialCorrection',
  'newFlatFinancialCorrection'
];

FlatFinancialCorrectionsNewCtrl.$resolve = {
  newFlatFinancialCorrection: [
    'FlatFinancialCorrection',
    function(FlatFinancialCorrection) {
      return FlatFinancialCorrection.newFlatFinancialCorrection().$promise;
    }
  ]
};

export { FlatFinancialCorrectionsNewCtrl };
