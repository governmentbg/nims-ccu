import _ from 'lodash';

function ProjectDossierContractSpotChecksViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  structuredDocument,
  internalEnvironmentSpotChecks,
  technicalReportSpotChecks
) {
  $scope.internalEnvironmentSpotChecks = internalEnvironmentSpotChecks;
  $scope.technicalReportSpotChecks = _.map(technicalReportSpotChecks, function(item) {
    if (item.xmlGid) {
      item.viewXmlUrl = structuredDocument.getUrl('contractReportTechnical', 'view', item.xmlGid);
    }

    return item;
  });

  $scope.internalEnvironmentSpotChecksExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/internalEnvironmentSpotChecks/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.technicalReportSpotChecksExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/technicalReportSpotChecks/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractSpotChecksViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'structuredDocument',
  'internalEnvironmentSpotChecks',
  'technicalReportSpotChecks'
];

ProjectDossierContractSpotChecksViewCtrl.$resolve = {
  internalEnvironmentSpotChecks: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractInternalEnvironmentSpotChecks({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  technicalReportSpotChecks: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractTechnicalReportSpotChecks({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractSpotChecksViewCtrl };
