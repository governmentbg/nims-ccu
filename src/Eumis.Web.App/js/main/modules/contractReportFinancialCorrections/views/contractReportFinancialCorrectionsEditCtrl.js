function ContractReportFinancialCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  structuredDocument,
  scConfirm,
  ContractReportFinancialCorrection,
  contractReportFinancialCorrection
) {
  $scope.editMode = null;
  $scope.contractReportFinancialCorrectionId = $stateParams.id;
  $scope.contractReportFinancialCorrection = contractReportFinancialCorrection;

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'ended') {
      confirmMsg =
        'contractReportFinancialCorrections_editContractReportFinancialCorrection_endedReason';
      validationAction = 'canChangeStatusToEnded';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportFinancialCorrections_editContractReportFinancialCorrection_draftReason';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractReportFinancialCorrection',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractReportFinancialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportFinancialCorrectionForm.$validate().then(function() {
      if ($scope.editContractReportFinancialCorrectionForm.$valid) {
        return ContractReportFinancialCorrection.update(
          {
            id: $stateParams.id
          },
          $scope.contractReportFinancialCorrection
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
      resource: 'ContractReportFinancialCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportFinancialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportFinancialCorrections.search');
      }
    });
  };
}

ContractReportFinancialCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'structuredDocument',
  'scConfirm',
  'ContractReportFinancialCorrection',
  'contractReportFinancialCorrection'
];

ContractReportFinancialCorrectionsEditCtrl.$resolve = {
  contractReportFinancialCorrection: [
    '$stateParams',
    'ContractReportFinancialCorrection',
    function($stateParams, ContractReportFinancialCorrection) {
      return ContractReportFinancialCorrection.get($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsEditCtrl };
