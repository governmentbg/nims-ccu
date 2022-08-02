import _ from 'lodash';

function ProjectDossierContractApprovedAmountsViewCtrl(
  $scope,
  $stateParams,
  csdNameCreator,
  $interpolate,
  approvedAmounts,
  contractReportCorrections,
  contractReportFinancialCorrections,
  contractReportFinancialCSDs
) {
  $scope.approvedAmounts = approvedAmounts;
  $scope.contractReportCorrections = contractReportCorrections;
  $scope.contractReportFinancialCorrections = contractReportFinancialCorrections;
  $scope.contractReportFinancialCSDs = _.forEach(contractReportFinancialCSDs, function(item) {
    csdNameCreator(item);
  });

  $scope.contractReportApprovedAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportApprovedAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportFinancialCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportFinancialCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportFinancialCSDsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportFinancialCSDs/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractApprovedAmountsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  'csdNameCreator',
  '$interpolate',
  'approvedAmounts',
  'contractReportCorrections',
  'contractReportFinancialCorrections',
  'contractReportFinancialCSDs'
];

ProjectDossierContractApprovedAmountsViewCtrl.$resolve = {
  approvedAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportApprovedAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportFinancialCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportFinancialCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportFinancialCSDs: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportFinancialCSDs({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractApprovedAmountsViewCtrl };
