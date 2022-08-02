function ContractReportDocumentsEditTechnicalCtrl(
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

  $scope.draftTechnical = function() {
    return scConfirm({
      confirmMessage:
        'contractReports_editContractReportDocumentsTechnical_confirmTechnicalChangeStatus',
      resource: 'ContractReportTechnical',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportTechnical.version
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
      resource: 'ContractReportTechnical',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: contractReportTechnical.version
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

ContractReportDocumentsEditTechnicalCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportTechnical',
  'contractReportTechnical'
];

ContractReportDocumentsEditTechnicalCtrl.$resolve = {
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

export { ContractReportDocumentsEditTechnicalCtrl };
