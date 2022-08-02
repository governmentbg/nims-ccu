function CorrectionDebtsNewStep1Ctrl($scope, $state) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.correctionDebtsNewStep1Form.$validate().then(function() {
      if ($scope.correctionDebtsNewStep1Form.$valid) {
        return $state.go('root.correctionDebts.newStep2', {
          cId: $scope.model.flatFinancialCorrectionId
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.correctionDebts.search');
  };
}

CorrectionDebtsNewStep1Ctrl.$inject = ['$scope', '$state'];

export { CorrectionDebtsNewStep1Ctrl };
