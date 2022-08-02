function ProjectDossierContractProcurementsEditCtrl($scope, $state, $stateParams, procurement) {
  $scope.procurement = procurement;
}

ProjectDossierContractProcurementsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'procurement'
];

ProjectDossierContractProcurementsEditCtrl.$resolve = {
  procurement: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      return ProjectDossierContract.getContractProcurement({
        id: $stateParams.id,
        ind: $stateParams.contractId,
        index: $stateParams.pid
      }).$promise;
    }
  ]
};

export { ProjectDossierContractProcurementsEditCtrl };
