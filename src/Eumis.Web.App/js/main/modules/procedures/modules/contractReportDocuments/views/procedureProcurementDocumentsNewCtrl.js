function ProcedureProcurementDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureProcurementDocument,
  procurementDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.procurementDocument = procurementDocument;

  $scope.save = function() {
    return $scope.newProcedureProcurementDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureProcurementDocumentsForm.$valid) {
        return ProcedureProcurementDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procurementDocument
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

ProcedureProcurementDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureProcurementDocument',
  'procurementDocument'
];

ProcedureProcurementDocumentsNewCtrl.$resolve = {
  procurementDocument: [
    'ProcedureProcurementDocument',
    '$stateParams',
    function(ProcedureProcurementDocument, $stateParams) {
      return ProcedureProcurementDocument.newProcurementDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureProcurementDocumentsNewCtrl };
