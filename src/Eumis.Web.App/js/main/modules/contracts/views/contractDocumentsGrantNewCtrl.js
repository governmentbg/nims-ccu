function ContractDocumentsGrantNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractGrantDocument,
  document
) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newContractGrantDocumentForm.$validate().then(function() {
      if ($scope.newContractGrantDocumentForm.$valid) {
        return ContractGrantDocument.save({ id: $stateParams.id }, $scope.document).$promise.then(
          function(result) {
            return $state.go('root.contracts.view.documents.grantEdit', {
              id: $stateParams.id,
              ind: result.contractGrantDocumentId
            });
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.documents.search');
  };
}

ContractDocumentsGrantNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractGrantDocument',
  'document'
];

ContractDocumentsGrantNewCtrl.$resolve = {
  document: [
    'ContractGrantDocument',
    '$stateParams',
    function(ContractGrantDocument, $stateParams) {
      return ContractGrantDocument.newContractGrantDocument({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractDocumentsGrantNewCtrl };
