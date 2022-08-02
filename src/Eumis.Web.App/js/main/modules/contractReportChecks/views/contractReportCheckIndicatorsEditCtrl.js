function ContractReportCheckIndicatorsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportIndicator,
  contractReportIndicator
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportIndicator = contractReportIndicator;
  $scope.contractReportTechnicalStatus = contractReportIndicator.contractReportTechnicalStatus;

  $scope.changeIndicatorStatus = function(status) {
    var confirmMsg = null,
      validationAction;

    if (status === 'ended') {
      confirmMsg = 'contractReportChecks_editContractReportIndicators_endedReason';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg = 'contractReportChecks_editContractReportIndicators_draftReason';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportIndicator',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportIndicator.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportIndicatorForm.$validate().then(function() {
      if ($scope.editContractReportIndicatorForm.$valid) {
        return ContractReportIndicator.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportIndicator
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
}

ContractReportCheckIndicatorsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportIndicator',
  'contractReportIndicator'
];

ContractReportCheckIndicatorsEditCtrl.$resolve = {
  contractReportIndicator: [
    'ContractReportIndicator',
    '$stateParams',
    function(ContractReportIndicator, $stateParams) {
      return ContractReportIndicator.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportCheckIndicatorsEditCtrl };
