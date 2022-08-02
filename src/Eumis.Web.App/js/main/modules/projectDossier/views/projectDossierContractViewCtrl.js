function ProjectDossierContractViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  contractVersions,
  contractProcurements,
  contractSpendingPlans,
  contractOffers,
  contractCommunications
) {
  $scope.projectId = $stateParams.id;
  $scope.contract = $scope.contractData;

  $scope.contractVersions = contractVersions;
  $scope.contractProcurements = contractProcurements;
  $scope.contractSpendingPlans = contractSpendingPlans;
  $scope.contractOffers = contractOffers;
  $scope.contractCommunications = contractCommunications;

  $scope.contractVersionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractVersions/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractProcurementsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractProcurements/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractSpendingPlansExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractSpendingPlans/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractOffersExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractOffers/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractCommunicationsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractCommunications/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'contractVersions',
  'contractProcurements',
  'contractSpendingPlans',
  'contractOffers',
  'contractCommunications'
];

ProjectDossierContractViewCtrl.$resolve = {
  contractVersions: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractVersions({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractProcurements: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractProcurements({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractSpendingPlans: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractSpendingPlans({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractOffers: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      return ProjectDossierContract.getContractProcurementOffers({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractCommunications: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractCommunications({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractViewCtrl };
