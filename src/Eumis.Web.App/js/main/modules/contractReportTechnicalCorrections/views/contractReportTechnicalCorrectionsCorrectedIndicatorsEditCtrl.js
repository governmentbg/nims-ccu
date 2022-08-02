function ContractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportTechnicalCorrectionIndicator,
  contractReportTechnicalCorrectionIndicator
) {
  $scope.editMode = null;
  $scope.contractReportTechnicalCorrectionId = $stateParams.id;
  $scope.contractReportTechnicalCorrectionStatus =
    $scope.contractReportTechnicalCorrectionInfo.status;
  $scope.contractReportTechnicalCorrectionIndicator = contractReportTechnicalCorrectionIndicator;

  $scope.changeTechnicalCorrectionIndicatorStatus = function(status) {
    var confirmMsg = null,
      action = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_endedConfirm';
      action = 'changeStatusToEnded';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportTechnicalCorrections_contractReportTechnicalCorrectionsCorrectedIndicatorsEdit_draftConfirm';
      action = 'changeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportTechnicalCorrectionIndicator',
      action: action,
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportTechnicalCorrectionIndicator.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editTechnicalCorrectionIndicatorForm.$validate().then(function() {
      if ($scope.editTechnicalCorrectionIndicatorForm.$valid) {
        return ContractReportTechnicalCorrectionIndicator.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractReportTechnicalCorrectionIndicator
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
      resource: 'ContractReportTechnicalCorrectionIndicator',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportTechnicalCorrectionIndicator.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportTechnicalCorrections.view.correctedIndicators.search');
      }
    });
  };
}

ContractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportTechnicalCorrectionIndicator',
  'contractReportTechnicalCorrectionIndicator'
];

ContractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl.$resolve = {
  contractReportTechnicalCorrectionIndicator: [
    '$stateParams',
    'ContractReportTechnicalCorrectionIndicator',
    function($stateParams, ContractReportTechnicalCorrectionIndicator) {
      return ContractReportTechnicalCorrectionIndicator.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsCorrectedIndicatorsEditCtrl };
