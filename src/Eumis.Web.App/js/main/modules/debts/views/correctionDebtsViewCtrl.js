function CorrectionDebtsViewCtrl($scope, correctionDebtInfo) {
  $scope.correctionDebtInfo = correctionDebtInfo;
}

CorrectionDebtsViewCtrl.$inject = ['$scope', 'correctionDebtInfo'];

CorrectionDebtsViewCtrl.$resolve = {
  correctionDebtInfo: [
    'CorrectionDebt',
    '$stateParams',
    function(CorrectionDebt, $stateParams) {
      return CorrectionDebt.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { CorrectionDebtsViewCtrl };
