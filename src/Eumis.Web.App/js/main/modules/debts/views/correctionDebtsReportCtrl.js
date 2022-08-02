function CorrectionDebtsReportCtrl($scope, correctionDebts) {
  $scope.correctionDebts = correctionDebts;
}

CorrectionDebtsReportCtrl.$inject = ['$scope', 'correctionDebts'];

CorrectionDebtsReportCtrl.$resolve = {
  correctionDebts: [
    'CorrectionDebt',
    function(CorrectionDebt) {
      return CorrectionDebt.getReport().$promise;
    }
  ]
};

export { CorrectionDebtsReportCtrl };
