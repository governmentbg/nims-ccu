function CorrectionCSDBudgetItemModalCtrl(
  $scope,
  $uibModalInstance,
  scConfirm,
  scModalParams,
  ContractReportFinancialCorrectionCSD,
  correctionCSDBudgetItem
) {
  $scope.editMode = null;
  $scope.contractReportFinancialCorrectionId = scModalParams.contractReportFinancialCorrectionId;
  $scope.contractReportFinancialCorrectionStatus =
    scModalParams.contractReportFinancialCorrectionStatus;
  $scope.correctionCSDBudgetItem = correctionCSDBudgetItem;

  $scope.changeCorrectionCSDBudgetItemStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg = 'contractReportChecks_modals_correctionCSDBudgetItemModal_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCorrectionCSD',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: scModalParams.contractReportFinancialCorrectionId,
        ind: scModalParams.contractReportFinancialCorrectionCSDId,
        version: $scope.correctionCSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $uibModalInstance.close();
      }
    });
  };

  $scope.save = function() {
    return $scope.correctionCSDBudgetItemForm.$validate().then(function() {
      if ($scope.correctionCSDBudgetItemForm.$valid) {
        return ContractReportFinancialCorrectionCSD.update(
          {
            id: scModalParams.contractReportFinancialCorrectionId,
            ind: scModalParams.contractReportFinancialCorrectionCSDId
          },
          $scope.correctionCSDBudgetItem
        ).$promise.then(function() {
          return $uibModalInstance.close();
        });
      }
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReportFinancialCorrectionCSD',
      action: 'remove',
      params: {
        id: scModalParams.contractReportFinancialCorrectionId,
        ind: scModalParams.contractReportFinancialCorrectionCSDId,
        version: $scope.correctionCSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $uibModalInstance.close();
      }
    });
  };
}

CorrectionCSDBudgetItemModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scConfirm',
  'scModalParams',
  'ContractReportFinancialCorrectionCSD',
  'correctionCSDBudgetItem'
];

CorrectionCSDBudgetItemModalCtrl.$resolve = {
  correctionCSDBudgetItem: [
    'scModalParams',
    'ContractReportFinancialCorrectionCSD',
    function(scModalParams, ContractReportFinancialCorrectionCSD) {
      return ContractReportFinancialCorrectionCSD.get({
        id: scModalParams.contractReportFinancialCorrectionId,
        ind: scModalParams.contractReportFinancialCorrectionCSDId
      }).$promise;
    }
  ]
};

export { CorrectionCSDBudgetItemModalCtrl };
