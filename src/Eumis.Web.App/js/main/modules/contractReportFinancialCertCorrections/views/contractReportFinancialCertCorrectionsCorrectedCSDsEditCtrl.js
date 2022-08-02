function ContractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportFinancialCertCorrectionCSD,
  correctionCSDBudgetItem
) {
  $scope.editMode = null;
  $scope.contractReportFinancialCertCorrectionId = $stateParams.id;
  $scope.contractReportFinancialCertCorrectionStatus =
    $scope.contractReportFinancialCertCorrectionInfo.status;
  $scope.correctionCSDBudgetItem = correctionCSDBudgetItem;

  $scope.changeCertCorrectionCSDBudgetItemStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportFinancialCertCorrections_' +
        'contractReportFinancialCertCorrectionsCorrectedCSDsEdit_endedConfirm';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportFinancialCertCorrections_' +
        'contractReportFinancialCertCorrectionsCorrectedCSDsEdit_draftConfirm';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCertCorrectionCSD',
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
    return $scope.editCertCorrectionCSDBudgetItemForm.$validate().then(function() {
      if ($scope.editCertCorrectionCSDBudgetItemForm.$valid) {
        return ContractReportFinancialCertCorrectionCSD.update(
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
      resource: 'ContractReportFinancialCertCorrectionCSD',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.correctionCSDBudgetItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportFinancialCertCorrections.view.correctedCsds.search');
      }
    });
  };
}

ContractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancialCertCorrectionCSD',
  'correctionCSDBudgetItem'
];

ContractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl.$resolve = {
  correctionCSDBudgetItem: [
    '$stateParams',
    'ContractReportFinancialCertCorrectionCSD',
    function($stateParams, ContractReportFinancialCertCorrectionCSD) {
      return ContractReportFinancialCertCorrectionCSD.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsCorrectedCSDsEditCtrl };
