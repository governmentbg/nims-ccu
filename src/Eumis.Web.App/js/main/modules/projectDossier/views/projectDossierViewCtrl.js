import _ from 'lodash';

function ProjectDossierViewCtrl($scope, l10n, $interpolate, projectRegData, contractData) {
  $scope.projectRegData = projectRegData;
  $scope.contractData = contractData;

  $scope.titleText = $interpolate(l10n.get('projectDossier_view_titleText'))({
    projectRegNumber: projectRegData.regNumber,
    contractRegNumber: contractData.regNumber
  });

  $scope.tabList = {
    projectDossier_tabs_project: 'root.projectDossier.view.project'
  };

  if (contractData.regNumber) {
    _.assign($scope.tabList, {
      projectDossier_tabs_contract: 'root.projectDossier.view.contract',
      projectDossier_tabs_paidAmounts: 'root.projectDossier.view.paidAmounts',
      projectDossier_tabs_debts: 'root.projectDossier.view.debts',
      projectDossier_tabs_reimbursedAmounts: 'root.projectDossier.view.reimbursedAmounts',
      projectDossier_tabs_financialCorrections: 'root.projectDossier.view.financialCorrections',
      projectDossier_tabs_approvedAmounts: 'root.projectDossier.view.approvedAmounts',
      projectDossier_tabs_certifiedAmounts: 'root.projectDossier.view.certifiedAmounts',
      projectDossier_tabs_physicalExecution: 'root.projectDossier.view.physicalExecution',
      projectDossier_tabs_spotChecks: 'root.projectDossier.view.spotChecks',
      projectDossier_tabs_irregularitiesAndSignals:
        'root.projectDossier.view.irregularitiesAndSignals',
      projectDossier_tabs_audits: 'root.projectDossier.view.audits'
    });
  }

  _.assign($scope.tabList, {
    projectDossier_tabs_documents: 'root.projectDossier.view.documents'
  });
}

ProjectDossierViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'projectRegData',
  'contractData'
];

ProjectDossierViewCtrl.$resolve = {
  projectRegData: [
    'ProjectDossier',
    '$stateParams',
    function(ProjectDossier, $stateParams) {
      return ProjectDossier.getProject({ id: $stateParams.id }).$promise;
    }
  ],
  contractData: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      if ($stateParams.contractId !== null && $stateParams.contractId !== undefined) {
        return ProjectDossierContract.get({
          id: $stateParams.id,
          ind: $stateParams.contractId
        }).$promise;
      } else {
        return {};
      }
    }
  ]
};

export { ProjectDossierViewCtrl };
