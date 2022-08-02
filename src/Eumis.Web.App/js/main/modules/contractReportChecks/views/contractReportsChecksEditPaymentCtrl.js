function ContractReportChecksEditPaymentCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportPaymentCheck,
  contractReportPaymentCheck
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportPaymentCheck = contractReportPaymentCheck;

  $scope.changePaymentCheckStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'active') {
      confirmMsg = 'contractReportChecks_editContractReportChecksPayment_activeReason';
      validationAction = 'canChangeStatusToActive';
    } else if (status === 'archived') {
      confirmMsg = 'contractReportChecks_editContractReportChecksPayment_archivedReason';
      validationAction = 'canChangeStatusToArchived';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportPaymentCheck',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportPaymentCheck.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportPaymentCheckForm.$validate().then(function() {
      if ($scope.editContractReportPaymentCheckForm.$valid) {
        return ContractReportPaymentCheck.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportPaymentCheck
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
      resource: 'ContractReportPaymentCheck',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportPaymentCheck.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportChecks.view.paymentChecks.search');
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.contractReportChecks.view.paymentChecks.search');
  };
}

ContractReportChecksEditPaymentCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportPaymentCheck',
  'contractReportPaymentCheck'
];

ContractReportChecksEditPaymentCtrl.$resolve = {
  contractReportPaymentCheck: [
    'ContractReportPaymentCheck',
    '$stateParams',
    function(ContractReportPaymentCheck, $stateParams) {
      return ContractReportPaymentCheck.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportChecksEditPaymentCtrl };
