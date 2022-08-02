function ProjectDossierContractVersionsEditCtrl($scope, $state, $stateParams, version) {
  $scope.version = version;
}

ProjectDossierContractVersionsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'version'];

ProjectDossierContractVersionsEditCtrl.$resolve = {
  version: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      return ProjectDossierContract.getContractVersion({
        id: $stateParams.id,
        ind: $stateParams.contractId,
        index: $stateParams.vid
      }).$promise;
    }
  ]
};

export { ProjectDossierContractVersionsEditCtrl };
