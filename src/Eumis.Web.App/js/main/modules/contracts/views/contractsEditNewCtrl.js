function ContractsEditNewCtrl(
  $scope,
  $state,
  scConfirm,
  structuredDocument,
  ContractVersion,
  contract
) {
  $scope.contract = contract;
  $scope.canCheck = contract.version.status === 'entered';
  $scope.canSetToDraft = contract.version.status === 'entered';
  $scope.canDelete = contract.contractStatus === 'draft';

  $scope.docUpdated = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Contract',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $scope.contract.contractId,
        version: $scope.contract.vers,
        contractVersionId: $scope.contract.version.versionId,
        contractVersion: $scope.contract.version.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go(
          'root.contracts.search',
          {},
          {
            reload: true
          }
        );
      }
    });
  };

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contracts_editContract_confirmDraft',
      resource: 'Contract',
      action: 'changeStatusToDraft',
      params: {
        id: $scope.contract.contractId,
        contractVersion: $scope.contract.version.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.check = function() {
    return scConfirm({
      confirmMessage: 'contracts_editContract_confirmCheck',
      resource: 'Contract',
      action: 'markAsChecked',
      params: {
        id: $scope.contract.contractId,
        version: $scope.contract.vers,
        contractVersion: $scope.contract.version.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go(
          'root.contracts.view.data',
          {
            id: contract.contractId
          },
          {
            location: 'replace'
          }
        );
      }
    });
  };
}

ContractsEditNewCtrl.$inject = [
  '$scope',
  '$state',
  'scConfirm',
  'structuredDocument',
  'ContractVersion',
  'contract'
];

ContractsEditNewCtrl.$resolve = {
  contract: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractsEditNewCtrl };
