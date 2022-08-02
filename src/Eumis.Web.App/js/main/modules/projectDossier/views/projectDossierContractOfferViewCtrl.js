function ProjectDossierContractOfferViewCtrl($scope, $state, $stateParams, contractOffer) {
  $scope.offer = contractOffer;
}

ProjectDossierContractOfferViewCtrl.$inject = ['$scope', '$state', '$stateParams', 'contractOffer'];

ProjectDossierContractOfferViewCtrl.$resolve = {
  contractOffer: [
    'ProjectDossierContract',
    '$stateParams',
    function(ProjectDossierContract, $stateParams) {
      return ProjectDossierContract.getContractProcurementOffer({
        id: $stateParams.id,
        ind: $stateParams.contractId,
        index: $stateParams.oid
      }).$promise;
    }
  ]
};

export { ProjectDossierContractOfferViewCtrl };
