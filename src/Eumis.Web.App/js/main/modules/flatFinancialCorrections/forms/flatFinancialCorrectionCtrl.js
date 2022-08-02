function FlatFinancialCorrectionCtrl($scope) {
  $scope.levelChanged = function() {
    $scope.model.contractId = null;
  };
}

FlatFinancialCorrectionCtrl.$inject = ['$scope'];

export { FlatFinancialCorrectionCtrl };
