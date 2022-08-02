function ContractReportChecksDocumentsEditTechnicalCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportTechnical,
  contractReportTechnical
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportTechnical = contractReportTechnical;

  $scope.reportUpdated = function() {
    return $state.partialReload();
  };

  $scope.changeTechnicalStatus = function(status) {
    var noteLabel = null,
      confirmMsg = null,
      validationAction = null;

    if (status === 'returned') {
      noteLabel = 'contractReportChecks_editContractReportDocumentsTechnical_returnedReason';
      validationAction = 'canChangeStatusToReturned';
      confirmMsg =
        'contractReportChecks_editContractReportDocumentsTechnical_confirmReturnTechnical';
    } else if (status === 'draft') {
      confirmMsg =
        'contractReportChecks_editContractReportDocumentsTechnical_confirmDraftTechnical';
    } else if (status === 'actual') {
      confirmMsg =
        'contractReportChecks_editContractReportDocumentsTechnical_confirmActualTechnical';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLabel,
      validationAction: validationAction,
      resource: 'ContractReportTechnical',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportTechnical.version
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

ContractReportChecksDocumentsEditTechnicalCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportTechnical',
  'contractReportTechnical'
];

ContractReportChecksDocumentsEditTechnicalCtrl.$resolve = {
  contractReportTechnical: [
    'ContractReportTechnical',
    '$stateParams',
    function(ContractReportTechnical, $stateParams) {
      return ContractReportTechnical.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportChecksDocumentsEditTechnicalCtrl };
