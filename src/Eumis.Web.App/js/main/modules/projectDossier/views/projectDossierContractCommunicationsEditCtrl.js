function ProjectDossierContractCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  contractCommunication
) {
  $scope.contractCommunication = contractCommunication;
}

ProjectDossierContractCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractCommunication'
];

ProjectDossierContractCommunicationsEditCtrl.$resolve = {
  contractCommunication: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      return ProjectDossierContract.getContractCommunication({
        id: $stateParams.id,
        ind: $stateParams.contractId,
        index: $stateParams.cid
      }).$promise;
    }
  ]
};

export { ProjectDossierContractCommunicationsEditCtrl };
