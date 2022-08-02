function ProjectDossierContractIrregularitiesAndSignalsViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  irregularities,
  irregularitySignals
) {
  $scope.irregularities = irregularities;
  $scope.irregularitySignals = irregularitySignals;

  $scope.irregularitiesExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/irregularities/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.irregularitySignalsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/irregularitySignals/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractIrregularitiesAndSignalsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'irregularities',
  'irregularitySignals'
];

ProjectDossierContractIrregularitiesAndSignalsViewCtrl.$resolve = {
  irregularities: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractIrregularities({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  irregularitySignals: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractIrregularitySignals({
        id: $stateParams.id,
        contractId: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractIrregularitiesAndSignalsViewCtrl };
