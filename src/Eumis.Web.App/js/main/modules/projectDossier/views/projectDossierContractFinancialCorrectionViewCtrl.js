function ProjectDossierContractFinancialCorrectionViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  financialCorrections,
  flatFinancialCorrections
) {
  $scope.financialCorrections = financialCorrections;
  $scope.flatFinancialCorrections = flatFinancialCorrections;

  $scope.financialCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/financialCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.flatFinancialCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/flatFinancialCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractFinancialCorrectionViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'financialCorrections',
  'flatFinancialCorrections'
];

ProjectDossierContractFinancialCorrectionViewCtrl.$resolve = {
  financialCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractFinancialCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  flatFinancialCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractFlatFinancialCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractFinancialCorrectionViewCtrl };
