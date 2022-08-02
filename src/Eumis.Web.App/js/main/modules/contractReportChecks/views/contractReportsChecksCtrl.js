function ContractReportChecksCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  structuredDocument,
  contractReportTechnicalChecks,
  contractReportFinancialChecks
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportSource = $scope.contractReportInfo.source;
  $scope.contractReportTechnicalChecks = contractReportTechnicalChecks;
  $scope.contractReportFinancialChecks = contractReportFinancialChecks;

  $scope.newCheck = function(type) {
    var reportResource;

    if (type === 'financial') {
      reportResource = 'ContractReportFinancialCheck';
    } else if (type === 'technical') {
      reportResource = 'ContractReportTechnicalCheck';
    }

    return scConfirm({
      resource: reportResource,
      validationAction: 'canCreate',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        if (type === 'financial') {
          return $state.go('root.contractReportChecks.view.checks.editFinancial', {
            id: $stateParams.id,
            ind: result.result.contractReportFinancialCheckId
          });
        } else if (type === 'technical') {
          return $state.go('root.contractReportChecks.view.checks.editTechnical', {
            id: $stateParams.id,
            ind: result.result.contractReportTechnicalCheckId
          });
        }
      }
    });
  };
}

ContractReportChecksCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'structuredDocument',
  'contractReportTechnicalChecks',
  'contractReportFinancialChecks'
];

ContractReportChecksCtrl.$resolve = {
  contractReportTechnicalChecks: [
    '$stateParams',
    'ContractReportTechnicalCheck',
    function($stateParams, ContractReportTechnicalCheck) {
      return ContractReportTechnicalCheck.query($stateParams).$promise;
    }
  ],
  contractReportFinancialChecks: [
    '$stateParams',
    'ContractReportFinancialCheck',
    function($stateParams, ContractReportFinancialCheck) {
      return ContractReportFinancialCheck.query($stateParams).$promise;
    }
  ]
};

export { ContractReportChecksCtrl };
