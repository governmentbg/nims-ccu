import angular from 'angular';

function ActuallyPaidAmountsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  paidAmountData,
  ActuallyPaidAmount,
  scModal
) {
  $scope.paidAmountData = paidAmountData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'actuallyPaidAmounts_basicData_draftConfirm',
      resource: 'ActuallyPaidAmount',
      action: 'setToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.paidAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'actuallyPaidAmounts_basicData_enterConfirm',
      resource: 'ActuallyPaidAmount',
      validationAction: 'canChangeStatusToEntered',
      action: 'changeStatusToEntered',
      params: {
        id: $stateParams.id,
        version: $scope.paidAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'actuallyPaidAmounts_basicData_removeConfirm',
      noteLabel: 'actuallyPaidAmounts_basicData_removeNote',
      resource: 'ActuallyPaidAmount',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.paidAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ActuallyPaidAmount',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.paidAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.actuallyPaidAmounts.search');
      }
    });
  };

  $scope.assignContractReportPayment = function() {
    var modalInstance = scModal.open('chooseActuallyPaidAmountContractReportPaymentModal', {
      id: $stateParams.id
    });

    modalInstance.result.then(function(result) {
      if (result.contractReportPaymentId) {
        return ActuallyPaidAmount.assignContractReportPayment(
          {
            id: $stateParams.id,
            contractReportPaymentId: result.contractReportPaymentId,
            version: $scope.paidAmountData.version
          },
          {}
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.changeContractReportPayment = function() {
    var modalInstance = scModal.open('chooseActuallyPaidAmountContractReportPaymentModal', {
      id: $stateParams.id
    });

    modalInstance.result.then(function(result) {
      if (result.contractReportPaymentId) {
        return ActuallyPaidAmount.changeContractReportPayment(
          {
            id: $stateParams.id,
            contractReportPaymentId: result.contractReportPaymentId,
            version: $scope.paidAmountData.version
          },
          {}
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.dissociateContractReportPayment = function() {
    return scConfirm({
      confirmMessage: 'actuallyPaidAmounts_basicData_dissociateContractReportPaymentConfirm',
      resource: 'ActuallyPaidAmount',
      action: 'dissociateContractReportPayment',
      params: {
        id: $stateParams.id,
        version: $scope.paidAmountData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ActuallyPaidAmountsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'paidAmountData',
  'ActuallyPaidAmount',
  'scModal'
];

ActuallyPaidAmountsBasicViewCtrl.$resolve = {
  paidAmountData: [
    'ActuallyPaidAmount',
    '$stateParams',
    function(ActuallyPaidAmount, $stateParams) {
      return ActuallyPaidAmount.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ActuallyPaidAmountsBasicViewCtrl };
