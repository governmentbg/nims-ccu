function ProjectDossierContractPaidAmountsViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  requestedAmounts,
  actuallyPaidAmounts
) {
  $scope.requestedAmounts = requestedAmounts;
  $scope.actuallyPaidAmounts = actuallyPaidAmounts;

  $scope.requestedAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/requestedAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.actuallyPaidAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/actuallyPaidAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractPaidAmountsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'requestedAmounts',
  'actuallyPaidAmounts'
];

ProjectDossierContractPaidAmountsViewCtrl.$resolve = {
  requestedAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportRequestedAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  actuallyPaidAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractActuallyPaidAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractPaidAmountsViewCtrl };
