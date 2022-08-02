function ContractReportChecksEditFinancialCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportFinancialCheck,
  contractReportFinancialCheck
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportFinancialCheck = contractReportFinancialCheck;

  $scope.changeFinancialCheckStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'active') {
      confirmMsg = 'contractReportChecks_editContractReportChecksFinancial_activeReason';
      validationAction = 'canChangeStatusToActive';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCheck',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportFinancialCheck.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportFinancialCheckForm.$validate().then(function() {
      if ($scope.editContractReportFinancialCheckForm.$valid) {
        return ContractReportFinancialCheck.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportFinancialCheck
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
      resource: 'ContractReportFinancialCheck',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportFinancialCheck.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportChecks.view.checks.search');
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.contractReportChecks.view.checks.search');
  };
}

ContractReportChecksEditFinancialCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancialCheck',
  'contractReportFinancialCheck'
];

ContractReportChecksEditFinancialCtrl.$resolve = {
  contractReportFinancialCheck: [
    'ContractReportFinancialCheck',
    '$stateParams',
    function(ContractReportFinancialCheck, $stateParams) {
      return ContractReportFinancialCheck.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportChecksEditFinancialCtrl };
