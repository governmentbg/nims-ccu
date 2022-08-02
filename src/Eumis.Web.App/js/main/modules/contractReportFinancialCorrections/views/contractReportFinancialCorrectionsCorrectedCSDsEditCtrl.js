function ContractReportFinancialCorrectionsCorrectedCSDsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportFinancialCorrectionCSD,
  correctionCSDBudgetItem
) {
  $scope.editMode = null;
  $scope.contractReportFinancialCorrectionId = $stateParams.id;
  $scope.contractReportFinancialCorrectionStatus =
    $scope.contractReportFinancialCorrectionInfo.status;
  $scope.correctionCSDBudgetItem = correctionCSDBudgetItem;

  $scope.changeCorrectionCSDBudgetItemStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportFinancialCorrections_' +
        'contractReportFinancialCorrectionsCorrectedCSDsEdit_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportFinancialCorrections_' +
        'contractReportFinancialCorrectionsCorrectedCSDsEdit_draftConfirm';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCorrectionCSD',
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
    return $scope.editCorrectionCSDBudgetItemForm.$validate().then(function() {
      if ($scope.editCorrectionCSDBudgetItemForm.$valid) {
        return ContractReportFinancialCorrectionCSD.update(
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
      resource: 'ContractReportFinancialCorrectionCSD',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.correctionCSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportFinancialCorrections.view.correctedCsds.search');
      }
    });
  };
}

ContractReportFinancialCorrectionsCorrectedCSDsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancialCorrectionCSD',
  'correctionCSDBudgetItem'
];

ContractReportFinancialCorrectionsCorrectedCSDsEditCtrl.$resolve = {
  correctionCSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialCorrectionCSD',
    function($stateParams, ContractReportFinancialCorrectionCSD) {
      return ContractReportFinancialCorrectionCSD.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsCorrectedCSDsEditCtrl };
