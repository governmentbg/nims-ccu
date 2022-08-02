function ContractDocumentsProcurementNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractProcurementDocument,
  document
) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newContractProcurementDocumentForm.$validate().then(function() {
      if ($scope.newContractProcurementDocumentForm.$valid) {
        return ContractProcurementDocument.save(
          { id: $stateParams.id },
          $scope.document
        ).$promise.then(function(result) {
          return $state.go('root.contracts.view.documents.procurementEdit', {
            id: $stateParams.id,
            ind: result.contractProcurementDocumentId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.documents.search');
  };
}

ContractDocumentsProcurementNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractProcurementDocument',
  'document'
];

ContractDocumentsProcurementNewCtrl.$resolve = {
  document: [
    'ContractProcurementDocument',
    '$stateParams',
    function(ContractProcurementDocument, $stateParams) {
      return ContractProcurementDocument.newContractProcurementDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractDocumentsProcurementNewCtrl };
