function ContractAmendmentsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractVersions,
  contractProcurements,
  contractSpendingPlans,
  contractOffers
) {
  $scope.contractId = $stateParams.id;
  $scope.contractVersions = contractVersions;
  $scope.contractProcurements = contractProcurements;
  $scope.contractSpendingPlans = contractSpendingPlans;
  $scope.contractOffers = contractOffers;

  $scope.newContractVersion = function(type) {
    return scConfirm({
      resource: 'ContractVersion',
      validationAction: 'canCreate',
      params: {
        id: $stateParams.id,
        type: type
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.amendments.versions.new', {
          id: $stateParams.id,
          t: type
        });
      }
    });
  };

  $scope.newContractProcurement = function() {
    return scConfirm({
      resource: 'ContractProcurement',
      validationAction: 'canCreate',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.amendments.procurements.new', {
          id: $stateParams.id
        });
      }
    });
  };

  $scope.newContractSpendingPlan = function() {
    return scConfirm({
      resource: 'ContractSpendingPlan',
      validationAction: 'canCreate',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.amendments.spendingPlans.new', {
          id: $stateParams.id
        });
      }
    });
  };
}

ContractAmendmentsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractVersions',
  'contractProcurements',
  'contractSpendingPlans',
  'contractOffers'
];

ContractAmendmentsSearchCtrl.$resolve = {
  contractVersions: [
    '$stateParams',
    'ContractFile',
    'ContractVersion',
    function($stateParams, ContractFile, ContractVersion) {
      return ContractVersion.query({
        id: $stateParams.id
      }).$promise;
    }
  ],
  contractProcurements: [
    '$stateParams',
    'ContractProcurement',
    function($stateParams, ContractProcurement) {
      return ContractProcurement.query({
        id: $stateParams.id
      }).$promise;
    }
  ],
  contractOffers: [
    '$stateParams',
    'ContractOffers',
    function($stateParams, ContractOffers) {
      return ContractOffers.query({
        id: $stateParams.id
      }).$promise;
    }
  ],
  contractSpendingPlans: [
    '$stateParams',
    'ContractSpendingPlan',
    function($stateParams, ContractSpendingPlan) {
      return ContractSpendingPlan.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractAmendmentsSearchCtrl };
