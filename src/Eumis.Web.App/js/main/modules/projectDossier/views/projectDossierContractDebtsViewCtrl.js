function ProjectDossierContractDebtsViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  contractDebts,
  correctionDebts
) {
  $scope.contractDebts = contractDebts;
  $scope.correctionDebts = correctionDebts;

  $scope.contractDebtsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractDebts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.correctionDebtsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/correctionDebts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractDebtsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'contractDebts',
  'correctionDebts'
];

ProjectDossierContractDebtsViewCtrl.$resolve = {
  contractDebts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractDebts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  correctionDebts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getCorrectionDebts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractDebtsViewCtrl };
