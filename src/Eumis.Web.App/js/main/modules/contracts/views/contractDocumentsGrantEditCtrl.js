function ContractDocumentsGrantEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractGrantDocument,
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
    return $scope.editContractGrantDocumentForm.$validate().then(function() {
      if ($scope.editContractGrantDocumentForm.$valid) {
        return ContractGrantDocument.update(
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
      resource: 'ContractGrantDocument',
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

ContractDocumentsGrantEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractGrantDocument',
  'document',
  'scConfirm'
];

ContractDocumentsGrantEditCtrl.$resolve = {
  document: [
    'ContractGrantDocument',
    '$stateParams',
    function(ContractGrantDocument, $stateParams) {
      return ContractGrantDocument.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ContractDocumentsGrantEditCtrl };
