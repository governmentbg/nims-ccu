function ProcedureTechnicalReportDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureTechnicalReportDocument,
  technicalReportDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.technicalReportDocument = technicalReportDocument;

  $scope.save = function() {
    return $scope.newProcedureTechnicalReportDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureTechnicalReportDocumentsForm.$valid) {
        return ProcedureTechnicalReportDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.technicalReportDocument
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

ProcedureTechnicalReportDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureTechnicalReportDocument',
  'technicalReportDocument'
];

ProcedureTechnicalReportDocumentsNewCtrl.$resolve = {
  technicalReportDocument: [
    'ProcedureTechnicalReportDocument',
    '$stateParams',
    function(ProcedureTechnicalReportDocument, $stateParams) {
      return ProcedureTechnicalReportDocument.newTechnicalReportDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureTechnicalReportDocumentsNewCtrl };
