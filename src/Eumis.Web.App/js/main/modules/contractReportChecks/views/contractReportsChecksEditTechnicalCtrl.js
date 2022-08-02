function ContractReportChecksEditTechnicalCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportTechnicalCheck,
  contractReportTechnicalCheck
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportTechnicalCheck = contractReportTechnicalCheck;

  $scope.changeTechnicalCheckStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'active') {
      confirmMsg = 'contractReportChecks_editContractReportChecksTechnical_activeReason';
      validationAction = 'canChangeStatusToActive';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportTechnicalCheck',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportTechnicalCheck.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportTechnicalCheckForm.$validate().then(function() {
      if ($scope.editContractReportTechnicalCheckForm.$valid) {
        return ContractReportTechnicalCheck.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportTechnicalCheck
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
      resource: 'ContractReportTechnicalCheck',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportTechnicalCheck.version
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

ContractReportChecksEditTechnicalCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportTechnicalCheck',
  'contractReportTechnicalCheck'
];

ContractReportChecksEditTechnicalCtrl.$resolve = {
  contractReportTechnicalCheck: [
    'ContractReportTechnicalCheck',
    '$stateParams',
    function(ContractReportTechnicalCheck, $stateParams) {
      return ContractReportTechnicalCheck.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportChecksEditTechnicalCtrl };
