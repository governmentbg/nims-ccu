function ProcedureFinalPaymentDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureFinalPaymentDocument,
  finalPaymentDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.finalPaymentDocument = finalPaymentDocument;

  $scope.save = function() {
    return $scope.newProcedureFinalPaymentDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureFinalPaymentDocumentsForm.$valid) {
        return ProcedureFinalPaymentDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.finalPaymentDocument
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

ProcedureFinalPaymentDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureFinalPaymentDocument',
  'finalPaymentDocument'
];

ProcedureFinalPaymentDocumentsNewCtrl.$resolve = {
  finalPaymentDocument: [
    'ProcedureFinalPaymentDocument',
    '$stateParams',
    function(ProcedureFinalPaymentDocument, $stateParams) {
      return ProcedureFinalPaymentDocument.newFinalPaymentDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureFinalPaymentDocumentsNewCtrl };
