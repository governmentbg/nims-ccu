function ContractReportChecksReportsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  scConfirm,
  scModal,
  ContractReport,
  contractReport,
  canChangeStatusToUnchecked
) {
  $scope.editMode = null;
  $scope.contractReport = contractReport;
  $scope.contractReportId = $stateParams.id;
  $scope.canChangeStatusToUnchecked = canChangeStatusToUnchecked.errors.length === 0;

  $scope.changeStatus = function(contractReportStatus) {
    var noteLabel = null,
      validationAction = null,
      action =
        'changeStatusTo' +
        contractReportStatus.charAt(0).toUpperCase() +
        contractReportStatus.slice(1),
      confirmMsg = $interpolate(
        l10n.get('contractReportChecks_editContractReport_confirmChangeStatus')
      )({
        status: l10n.get('contractReportChecks_editContractReport_' + contractReportStatus)
      });
    if (contractReportStatus === 'refused') {
      noteLabel = 'contractReportChecks_editContractReport_statusChangeMessage';
      validationAction = 'canChangeStatusToRefused';
    } else if (contractReportStatus === 'accepted') {
      validationAction = 'canChangeStatusToAccepted';
    } else if (contractReportStatus === 'returnToUnchecked') {
      //overwrite action and configMsg
      validationAction = 'canReturnStatusToUnchecked';
      action = 'returnStatusToUnchecked';
      confirmMsg = 'contractReportChecks_editContractReport_returnToUncheckedConfirm';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLabel,
      validationAction: validationAction,
      resource: 'ContractReport',
      action: action,
      params: {
        id: $stateParams.id,
        version: $scope.contractReport.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editContractReportForm.$validate().then(function() {
      if ($scope.editContractReportForm.$valid) {
        return ContractReport.checkUpdate(
          {
            id: $stateParams.id
          },
          $scope.contractReport
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

ContractReportChecksReportsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'scConfirm',
  'scModal',
  'ContractReport',
  'contractReport',
  'canChangeStatusToUnchecked'
];

ContractReportChecksReportsEditCtrl.$resolve = {
  contractReport: [
    'ContractReport',
    '$stateParams',
    function(ContractReport, $stateParams) {
      return ContractReport.get({
        id: $stateParams.id
      }).$promise;
    }
  ],
  canChangeStatusToUnchecked: [
    'ContractReport',
    '$stateParams',
    function(ContractReport, $stateParams) {
      return ContractReport.getCanChangeStatusToUnchecked({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportChecksReportsEditCtrl };
