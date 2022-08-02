function ContractReportTechnicalCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  scConfirm,
  ContractReportTechnicalCorrection,
  contractReportTechnicalCorrection
) {
  $scope.editMode = null;
  $scope.contractReportTechnicalCorrectionId = $stateParams.id;
  $scope.contractReportTechnicalCorrection = contractReportTechnicalCorrection;

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      action = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportTechnicalCorrections_editContractReportTechnicalCorrection_endedReason';
      action = 'changeStatusToEnded';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportTechnicalCorrections_editContractReportTechnicalCorrection_draftReason';
      action = 'changeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportTechnicalCorrection',
      action: action,
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportTechnicalCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportTechnicalCorrectionForm.$validate().then(function() {
      if ($scope.editContractReportTechnicalCorrectionForm.$valid) {
        return ContractReportTechnicalCorrection.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportTechnicalCorrection
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
      validationAction: 'canDelete',
      resource: 'ContractReportTechnicalCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportTechnicalCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportTechnicalCorrections.search');
      }
    });
  };
}

ContractReportTechnicalCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'scConfirm',
  'ContractReportTechnicalCorrection',
  'contractReportTechnicalCorrection'
];

ContractReportTechnicalCorrectionsEditCtrl.$resolve = {
  contractReportTechnicalCorrection: [
    '$stateParams',
    'ContractReportTechnicalCorrection',
    function($stateParams, ContractReportTechnicalCorrection) {
      return ContractReportTechnicalCorrection.get($stateParams).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsEditCtrl };
