function ProjectDossierContractPhysicalExecutionViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  activities,
  indicators
) {
  $scope.activities = activities;
  $scope.indicators = indicators;

  $scope.activitiesExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/physicalExecutionActivities/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.indicatorsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/physicalExecutionIndicators/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractPhysicalExecutionViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'activities',
  'indicators'
];

ProjectDossierContractPhysicalExecutionViewCtrl.$resolve = {
  activities: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractPhysicalExecutionActivities({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  indicators: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractPhysicalExecutionIndicators({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractPhysicalExecutionViewCtrl };
