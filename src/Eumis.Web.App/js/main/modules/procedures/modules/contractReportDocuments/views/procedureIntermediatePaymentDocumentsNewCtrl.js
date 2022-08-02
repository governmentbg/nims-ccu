function ProcedureIntermediatePaymentDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureIntermediatePaymentDocument,
  intermediatePaymentDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.intermediatePaymentDocument = intermediatePaymentDocument;

  $scope.save = function() {
    return $scope.newProcedureIntermediatePaymentDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureIntermediatePaymentDocumentsForm.$valid) {
        return ProcedureIntermediatePaymentDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.intermediatePaymentDocument
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

ProcedureIntermediatePaymentDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureIntermediatePaymentDocument',
  'intermediatePaymentDocument'
];

ProcedureIntermediatePaymentDocumentsNewCtrl.$resolve = {
  intermediatePaymentDocument: [
    'ProcedureIntermediatePaymentDocument',
    '$stateParams',
    function(ProcedureIntermediatePaymentDocument, $stateParams) {
      return ProcedureIntermediatePaymentDocument.newIntermediatePaymentDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureIntermediatePaymentDocumentsNewCtrl };
