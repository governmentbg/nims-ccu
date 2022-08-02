function ContractReportChecksDocumentsEditPaymentCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportPayment,
  contractReportPayment
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportPayment = contractReportPayment;

  $scope.reportUpdated = function() {
    return $state.partialReload();
  };

  $scope.changePaymentStatus = function(status) {
    var noteLabel = null,
      confirmMsg = null,
      validationAction = null;

    if (status === 'returned') {
      noteLabel = 'contractReportChecks_editContractReportDocumentsPayment_returnedReason';
      validationAction = 'canChangeStatusToReturned';
      confirmMsg = 'contractReportChecks_editContractReportDocumentsPayment_confirmReturnPayment';
    } else if (status === 'draft') {
      confirmMsg = 'contractReportChecks_editContractReportDocumentsPayment_confirmDraftPayment';
    } else if (status === 'actual') {
      confirmMsg = 'contractReportChecks_editContractReportDocumentsPayment_confirmActualPayment';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLabel,
      validationAction: validationAction,
      resource: 'ContractReportPayment',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportPayment.version
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

ContractReportChecksDocumentsEditPaymentCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportPayment',
  'contractReportPayment'
];

ContractReportChecksDocumentsEditPaymentCtrl.$resolve = {
  contractReportPayment: [
    'ContractReportPayment',
    '$stateParams',
    function(ContractReportPayment, $stateParams) {
      return ContractReportPayment.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportChecksDocumentsEditPaymentCtrl };
