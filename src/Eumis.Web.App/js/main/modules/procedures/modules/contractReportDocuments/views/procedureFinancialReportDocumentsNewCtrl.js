function ProcedureFinancialReportDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureFinancialReportDocument,
  financialReportDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.financialReportDocument = financialReportDocument;

  $scope.save = function() {
    return $scope.newProcedureFinancialReportDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureFinancialReportDocumentsForm.$valid) {
        return ProcedureFinancialReportDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.financialReportDocument
        ).$promise.then(function() {
          return $state.go('root.procedures.view.reportDocs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.reportDocs.search');
  };
}

ProcedureFinancialReportDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureFinancialReportDocument',
  'financialReportDocument'
];

ProcedureFinancialReportDocumentsNewCtrl.$resolve = {
  financialReportDocument: [
    'ProcedureFinancialReportDocument',
    '$stateParams',
    function(ProcedureFinancialReportDocument, $stateParams) {
      return ProcedureFinancialReportDocument.newFinancialReportDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureFinancialReportDocumentsNewCtrl };
