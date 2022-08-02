function ContractReportDocumentsEditPaymentCtrl(
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

  $scope.draftPayment = function() {
    return scConfirm({
      confirmMessage:
        'contractReports_editContractReportDocumentsPayment_confirmPaymentChangeStatus',
      resource: 'ContractReportPayment',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportPayment.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReportPayment',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportPayment.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReports.view.documents.search');
      }
    });
  };

  $scope.back = function() {
    return $state.go('root.contractReports.view.documents.search');
  };
}

ContractReportDocumentsEditPaymentCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportPayment',
  'contractReportPayment'
];

ContractReportDocumentsEditPaymentCtrl.$resolve = {
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

export { ContractReportDocumentsEditPaymentCtrl };
