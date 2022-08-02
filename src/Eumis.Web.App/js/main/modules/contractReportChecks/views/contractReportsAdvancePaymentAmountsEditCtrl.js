function ContractReportsAdvancePaymentAmountsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportAdvancePaymentAmount,
  contractReportAdvancePaymentAmount
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportAdvancePaymentAmount = contractReportAdvancePaymentAmount;

  $scope.changeAdvancePaymentAmountStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg = 'contractReportChecks_contractReportAdvancePaymentAmountEdit_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg = 'contractReportChecks_contractReportAdvancePaymentAmountEdit_draftConfirm';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportAdvancePaymentAmount',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportAdvancePaymentAmount.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportAdvancePaymentAmountForm.$validate().then(function() {
      if ($scope.editContractReportAdvancePaymentAmountForm.$valid) {
        return ContractReportAdvancePaymentAmount.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportAdvancePaymentAmount
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

ContractReportsAdvancePaymentAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportAdvancePaymentAmount',
  'contractReportAdvancePaymentAmount'
];

ContractReportsAdvancePaymentAmountsEditCtrl.$resolve = {
  contractReportAdvancePaymentAmount: [
    '$stateParams',
    'ContractReportAdvancePaymentAmount',
    function($stateParams, ContractReportAdvancePaymentAmount) {
      return ContractReportAdvancePaymentAmount.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportsAdvancePaymentAmountsEditCtrl };
