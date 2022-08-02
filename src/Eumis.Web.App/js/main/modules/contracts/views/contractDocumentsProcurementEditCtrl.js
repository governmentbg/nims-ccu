function ContractDocumentsProcurementEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractProcurementDocument,
  document,
  scConfirm
) {
  $scope.contractId = $stateParams.id;
  $scope.document = document;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editContractProcurementDocumentForm.$validate().then(function() {
      if ($scope.editContractProcurementDocumentForm.$valid) {
        return ContractProcurementDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.document
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteDocument = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractProcurementDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.documents.search');
      }
    });
  };
}

ContractDocumentsProcurementEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractProcurementDocument',
  'document',
  'scConfirm'
];

ContractDocumentsProcurementEditCtrl.$resolve = {
  document: [
    'ContractProcurementDocument',
    '$stateParams',
    function(ContractProcurementDocument, $stateParams) {
      return ContractProcurementDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractDocumentsProcurementEditCtrl };
