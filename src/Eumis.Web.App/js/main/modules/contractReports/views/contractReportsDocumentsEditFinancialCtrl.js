function ContractReportDocumentsEditFinancialCtrl(
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

  $scope.draftFinancial = function() {
    return scConfirm({
      confirmMessage:
        'contractReports_editContractReportDocumentsFinancial_confirmFinancialChangeStatus',
      resource: 'ContractReportFinancial',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportFinancial.version
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
      resource: 'ContractReportFinancial',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportFinancial.version
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

ContractReportDocumentsEditFinancialCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportFinancial',
  'contractReportFinancial'
];

ContractReportDocumentsEditFinancialCtrl.$resolve = {
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

export { ContractReportDocumentsEditFinancialCtrl };
