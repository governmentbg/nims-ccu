function ContractReportFinancialRevalidationsRevalidatedCSDsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportFinancialRevalidationCSD,
  correctionCSDBudgetItem
) {
  $scope.editMode = null;
  $scope.contractReportFinancialRevalidationId = $stateParams.id;
  $scope.contractReportFinancialRevalidationStatus =
    $scope.contractReportFinancialRevalidationInfo.status;
  $scope.correctionCSDBudgetItem = correctionCSDBudgetItem;

  $scope.changeRevalidationCSDBudgetItemStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportFinancialRevalidations_' +
        'contractReportFinancialRevalidationsCorrectedCSDsEdit_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportFinancialRevalidations_' +
        'contractReportFinancialRevalidationsCorrectedCSDsEdit_draftConfirm';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialRevalidationCSD',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.correctionCSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editRevalidationCSDBudgetItemForm.$validate().then(function() {
      if ($scope.editRevalidationCSDBudgetItemForm.$valid) {
        return ContractReportFinancialRevalidationCSD.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.correctionCSDBudgetItem
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReportFinancialRevalidationCSD',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.correctionCSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportFinancialRevalidations.view.revalidatedCsds.search');
      }
    });
  };
}

ContractReportFinancialRevalidationsRevalidatedCSDsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancialRevalidationCSD',
  'correctionCSDBudgetItem'
];

ContractReportFinancialRevalidationsRevalidatedCSDsEditCtrl.$resolve = {
  correctionCSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialRevalidationCSD',
    function($stateParams, ContractReportFinancialRevalidationCSD) {
      return ContractReportFinancialRevalidationCSD.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsRevalidatedCSDsEditCtrl };
