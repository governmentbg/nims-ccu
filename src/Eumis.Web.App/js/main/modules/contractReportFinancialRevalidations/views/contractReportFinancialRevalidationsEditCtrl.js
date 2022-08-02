function ContractReportFinancialRevalidationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  structuredDocument,
  scConfirm,
  ContractReportFinancialRevalidation,
  contractReportFinancialRevalidation
) {
  $scope.editMode = null;
  $scope.contractReportFinancialRevalidationId = $stateParams.id;
  $scope.contractReportFinancialRevalidation = contractReportFinancialRevalidation;

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportFinancialRevalidations_' +
        'editContractReportFinancialRevalidation_endedReason';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportFinancialRevalidations_' +
        'editContractReportFinancialRevalidation_draftReason';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialRevalidation',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportFinancialRevalidation.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportFinancialRevalidationForm.$validate().then(function() {
      if ($scope.editContractReportFinancialRevalidationForm.$valid) {
        return ContractReportFinancialRevalidation.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportFinancialRevalidation
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      validationAction: 'canDelete',
      resource: 'ContractReportFinancialRevalidation',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportFinancialRevalidation.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportFinancialRevalidations.search');
      }
    });
  };
}

ContractReportFinancialRevalidationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'structuredDocument',
  'scConfirm',
  'ContractReportFinancialRevalidation',
  'contractReportFinancialRevalidation'
];

ContractReportFinancialRevalidationsEditCtrl.$resolve = {
  contractReportFinancialRevalidation: [
    '$stateParams',
    'ContractReportFinancialRevalidation',
    function($stateParams, ContractReportFinancialRevalidation) {
      return ContractReportFinancialRevalidation.get($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsEditCtrl };
