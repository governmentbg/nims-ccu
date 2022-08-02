function ProjectDossierContractCertifiedAmountsViewCtrl(
  $scope,
  $stateParams,
  $interpolate,
  certifiedAmounts,
  contractReportCertCorrections,
  contractReportFinancialCertCorrections,
  contractReportFinancialCorrections,
  contractReportCorrections,
  contractReportCertAuthorityFinancialCorrections,
  contractReportCertAuthorityCorrections,
  contractReportRevalidations,
  contractReportFinancialRevalidations
) {
  $scope.certifiedAmounts = certifiedAmounts;
  $scope.contractReportCertCorrections = contractReportCertCorrections;
  $scope.contractReportFinancialCertCorrections = contractReportFinancialCertCorrections;
  $scope.contractReportFinancialCorrections = contractReportFinancialCorrections;
  $scope.contractReportCorrections = contractReportCorrections;
  $scope.contractReportCertAuthorityFinancialCorrections = contractReportCertAuthorityFinancialCorrections;
  $scope.contractReportCertAuthorityCorrections = contractReportCertAuthorityCorrections;
  $scope.contractReportRevalidations = contractReportRevalidations;
  $scope.contractReportFinancialRevalidations = contractReportFinancialRevalidations;

  $scope.contractReportCertifiedAmountsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCertifiedAmounts/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportRevalidationsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportRevalidations/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportFinancialRevalidationsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportFinancialRevalidations/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportFinancialCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCertifiedAmountFinancialCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCertifiedAmountCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportCertAuthorityFinancialCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCertAuthorityFinancialCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportCertAuthorityCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCertAuthorityCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportCertCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportCertCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });

  $scope.contractReportFinancialCertCorrectionsExportUrl = $interpolate(
    'api/projectDossier/{{id}}/contract/{{contractId}}/contractReportFinancialCertCorrections/excelExport'
  )({
    id: $stateParams.id,
    contractId: $stateParams.contractId
  });
}

ProjectDossierContractCertifiedAmountsViewCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'certifiedAmounts',
  'contractReportCertCorrections',
  'contractReportFinancialCertCorrections',
  'contractReportFinancialCorrections',
  'contractReportCorrections',
  'contractReportCertAuthorityFinancialCorrections',
  'contractReportCertAuthorityCorrections',
  'contractReportRevalidations',
  'contractReportFinancialRevalidations'
];

ProjectDossierContractCertifiedAmountsViewCtrl.$resolve = {
  certifiedAmounts: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCertifiedAmounts({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportCertCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCertCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportFinancialCertCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportFinancialCertCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportFinancialCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCertifiedAmountFinancialCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCertifiedAmountCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportCertAuthorityFinancialCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCertAuthorityFinancialCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportCertAuthorityCorrections: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportCertAuthorityCorrections({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportRevalidations: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportRevalidations({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ],
  contractReportFinancialRevalidations: [
    '$stateParams',
    'ProjectDossierContract',
    function($stateParams, ProjectDossierContract) {
      return ProjectDossierContract.getContractReportFinancialRevalidations({
        id: $stateParams.id,
        ind: $stateParams.contractId
      }).$promise;
    }
  ]
};

export { ProjectDossierContractCertifiedAmountsViewCtrl };
