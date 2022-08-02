function ContractReportsFinancialCSDBudgetItemsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportFinancialCSDBudgetItem,
  CSDBudgetItem
) {
  $scope.editMode = null;
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.CSDBudgetItem = CSDBudgetItem;
  $scope.financialReportStatus = CSDBudgetItem.contractReportFinancialStatus;

  $scope.techCheck = function() {
    return scConfirm({
      confirmMessage: 'contractReportChecks_editCSDBudgetItem_techCheckConfirm',
      resource: 'ContractReportFinancialCSDBudgetItem',
      action: 'techCheck',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.CSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.changeCSDBudgetItemStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg = 'contractReportChecks_editCSDBudgetItem_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg = 'contractReportChecks_editCSDBudgetItem_draftConfirm';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCSDBudgetItem',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.CSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editCSDBudgetItemForm.$validate().then(function() {
      if ($scope.editCSDBudgetItemForm.$valid) {
        return ContractReportFinancialCSDBudgetItem.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.CSDBudgetItem
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

ContractReportsFinancialCSDBudgetItemsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancialCSDBudgetItem',
  'CSDBudgetItem'
];

ContractReportsFinancialCSDBudgetItemsEditCtrl.$resolve = {
  CSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialCSDBudgetItem',
    function($stateParams, ContractReportFinancialCSDBudgetItem) {
      return ContractReportFinancialCSDBudgetItem.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportsFinancialCSDBudgetItemsEditCtrl };
