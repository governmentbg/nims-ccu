function ProjectDossierContractSpendingPlansEditCtrl($scope, $state, $stateParams, spendingPlan) {
  $scope.spendingPlan = spendingPlan;
}

ProjectDossierContractSpendingPlansEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'spendingPlan'
];

ProjectDossierContractSpendingPlansEditCtrl.$resolve = {
  spendingPlan: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      return ProjectDossierContract.getContractSpendingPlan({
        id: $stateParams.id,
        ind: $stateParams.contractId,
        index: $stateParams.spid
      }).$promise;
    }
  ]
};

export { ProjectDossierContractSpendingPlansEditCtrl };
