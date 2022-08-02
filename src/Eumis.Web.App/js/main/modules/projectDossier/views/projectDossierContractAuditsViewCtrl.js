import _ from 'lodash';

function ProjectDossierContractAuditsViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  structuredDocument,
  internalEnvironmentAudits,
  technicalReportAudits
) {
  $scope.internalEnvironmentAudits = internalEnvironmentAudits;
  $scope.technicalReportAudits = _.map(technicalReportAudits, function(item) {
    if (item.xmlGid) {
      item.viewXmlUrl = structuredDocument.getUrl('contractReportTechnical', 'view', item.xmlGid);
    }

    return item;
  });

  $scope.internalEnvironmentAuditsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/internalEnvironmentAudits/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.technicalReportAuditsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/technicalReportAudits/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractAuditsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'structuredDocument',
  'internalEnvironmentAudits',
  'technicalReportAudits'
];

ProjectDossierContractAuditsViewCtrl.$resolve = {
  internalEnvironmentAudits: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractInternalEnvironmentAudits({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  technicalReportAudits: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractTechnicalReportAudits({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractAuditsViewCtrl };
