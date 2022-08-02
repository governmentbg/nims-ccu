function ContractReportsAdvanceNVPaymentAmountsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportAdvanceNVPaymentAmount,
  contractReportAdvanceNVPaymentAmount
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportAdvanceNVPaymentAmount = contractReportAdvanceNVPaymentAmount;

  $scope.changeAdvanceNVPaymentAmountStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg = 'contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg = 'contractReportChecks_contractReportAdvanceNVPaymentAmountEdit_draftConfirm';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportAdvanceNVPaymentAmount',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportAdvanceNVPaymentAmount.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportAdvanceNVPaymentAmountForm.$validate().then(function() {
      if ($scope.editContractReportAdvanceNVPaymentAmountForm.$valid) {
        return ContractReportAdvanceNVPaymentAmount.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportAdvanceNVPaymentAmount
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

ContractReportsAdvanceNVPaymentAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportAdvanceNVPaymentAmount',
  'contractReportAdvanceNVPaymentAmount'
];

ContractReportsAdvanceNVPaymentAmountsEditCtrl.$resolve = {
  contractReportAdvanceNVPaymentAmount: [
    '$stateParams',
    'ContractReportAdvanceNVPaymentAmount',
    function($stateParams, ContractReportAdvanceNVPaymentAmount) {
      return ContractReportAdvanceNVPaymentAmount.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportsAdvanceNVPaymentAmountsEditCtrl };
