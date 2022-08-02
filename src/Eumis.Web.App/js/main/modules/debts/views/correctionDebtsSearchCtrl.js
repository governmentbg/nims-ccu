function CorrectionDebtsSearchCtrl($scope, $stateParams, correctionDebts) {
  $scope.correctionDebts = correctionDebts;
}

CorrectionDebtsSearchCtrl.$inject = ['$scope', '$stateParams', 'correctionDebts'];

CorrectionDebtsSearchCtrl.$resolve = {
  correctionDebts: [
    'CorrectionDebt',
    function(CorrectionDebt) {
      return CorrectionDebt.query().$promise;
    }
  ]
};

export { CorrectionDebtsSearchCtrl };
