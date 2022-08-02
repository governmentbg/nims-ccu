function ProcedureAdvancePaymentDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureAdvancePaymentDocument,
  advancePaymentDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.advancePaymentDocument = advancePaymentDocument;

  $scope.save = function() {
    return $scope.newProcedureAdvancePaymentDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureAdvancePaymentDocumentsForm.$valid) {
        return ProcedureAdvancePaymentDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.advancePaymentDocument
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

ProcedureAdvancePaymentDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureAdvancePaymentDocument',
  'advancePaymentDocument'
];

ProcedureAdvancePaymentDocumentsNewCtrl.$resolve = {
  advancePaymentDocument: [
    'ProcedureAdvancePaymentDocument',
    '$stateParams',
    function(ProcedureAdvancePaymentDocument, $stateParams) {
      return ProcedureAdvancePaymentDocument.newAdvancePaymentDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureAdvancePaymentDocumentsNewCtrl };
