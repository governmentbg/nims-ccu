function ContractOffersViewCtrl($scope, $state, $stateParams, contractOffer) {
  $scope.offer = contractOffer;
}

ContractOffersViewCtrl.$inject = ['$scope', '$state', '$stateParams', 'contractOffer'];

ContractOffersViewCtrl.$resolve = {
  contractOffer: [
    'ContractOffers',
    '$stateParams',
    function(ContractOffers, $stateParams) {
      return ContractOffers.get({
        id: $stateParams.id,
        oid: $stateParams.oid
      }).$promise;
    }
  ]
};

export { ContractOffersViewCtrl };
