function ContractReportFinancialCertCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  scConfirm,
  ContractReportFinancialCertCorrection,
  contractReportFinancialCertCorrection
) {
  $scope.editMode = null;
  $scope.contractReportFinancialCertCorrectionId = $stateParams.id;
  $scope.contractReportFinancialCertCorrection = contractReportFinancialCertCorrection;

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportFinancialCertCorrections_' +
        'editContractReportFinancialCertCorrection_endedReason';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportFinancialCertCorrections_' +
        'editContractReportFinancialCertCorrection_draftReason';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCertCorrection',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportFinancialCertCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportFinancialCertCorrectionForm.$validate().then(function() {
      if ($scope.editContractReportFinancialCertCorrectionForm.$valid) {
        return ContractReportFinancialCertCorrection.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportFinancialCertCorrection
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
      resource: 'ContractReportFinancialCertCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportFinancialCertCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportFinancialCertCorrections.search');
      }
    });
  };
}

ContractReportFinancialCertCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'scConfirm',
  'ContractReportFinancialCertCorrection',
  'contractReportFinancialCertCorrection'
];

ContractReportFinancialCertCorrectionsEditCtrl.$resolve = {
  contractReportFinancialCertCorrection: [
    '$stateParams',
    'ContractReportFinancialCertCorrection',
    function($stateParams, ContractReportFinancialCertCorrection) {
      return ContractReportFinancialCertCorrection.get($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsEditCtrl };
