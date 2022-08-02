function ProjectDossierContractReimbursedAmountsViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  debtReimbursedAmounts,
  contractReimbursedAmounts,
  fiReimbursedAmounts
) {
  $scope.debtReimbursedAmounts = debtReimbursedAmounts;
  $scope.contractReimbursedAmounts = contractReimbursedAmounts;
  $scope.fiReimbursedAmounts = fiReimbursedAmounts;

  $scope.debtReimbursedAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/debtReimbursedAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReimbursedAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReimbursedAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.fiReimbursedAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/fiReimbursedAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractReimbursedAmountsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'debtReimbursedAmounts',
  'contractReimbursedAmounts',
  'fiReimbursedAmounts'
];

ProjectDossierContractReimbursedAmountsViewCtrl.$resolve = {
  debtReimbursedAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getDebtReimbursedAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReimbursedAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReimbursedAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  fiReimbursedAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getFiReimbursedAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractReimbursedAmountsViewCtrl };
