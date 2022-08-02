function ContractReportChecksDocumentsEditFinancialCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportFinancial,
  contractReportFinancial
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportFinancial = contractReportFinancial;

  $scope.reportUpdated = function() {
    return $state.partialReload();
  };

  $scope.changeFinancialStatus = function(status) {
    var noteLabel = null,
      confirmMsg = null,
      validationAction = null;

    if (status === 'returned') {
      noteLabel = 'contractReportChecks_editContractReportDocumentsFinancial_returnedReason';
      validationAction = 'canChangeStatusToReturned';
      confirmMsg =
        'contractReportChecks_editContractReportDocumentsFinancial_confirmReturnFinancial';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportChecks_editContractReportDocumentsFinancial_confirmDraftFinancial';
    } else if (status === 'actual') {
      confirmMsg =
        'contractReportChecks_editContractReportDocumentsFinancial_confirmActualFinancial';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLabel,
      validationAction: validationAction,
      resource: 'ContractReportFinancial',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportFinancial.version
      }
    }).then(function(result) {
      if (result.executed) {
        if (status === 'returned') {
          return $state.go('root.contractReportChecks.view.documents.search');
        } else {
          return $state.partialReload();
        }
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.contractReportChecks.view.documents.search');
  };
}

ContractReportChecksDocumentsEditFinancialCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancial',
  'contractReportFinancial'
];

ContractReportChecksDocumentsEditFinancialCtrl.$resolve = {
  contractReportFinancial: [
    'ContractReportFinancial',
    '$stateParams',
    function(ContractReportFinancial, $stateParams) {
      return ContractReportFinancial.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportChecksDocumentsEditFinancialCtrl };
