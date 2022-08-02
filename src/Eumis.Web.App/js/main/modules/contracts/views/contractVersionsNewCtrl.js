function ContractVersionsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractVersion,
  newContractVersion
) {
  $scope.newContractVersion = newContractVersion;

  $scope.save = function() {
    return $scope.newContractVersionForm.$validate().then(function() {
      if ($scope.newContractVersionForm.$valid) {
        return ContractVersion.save(
          {
            id: $stateParams.id
          },
          $scope.newContractVersion
        ).$promise.then(function(result) {
          return $state.go(
            'root.contracts.view.amendments.versions.edit',
            {
              id: result.contractId,
              vid: result.contractVersionId
            },
            {
              reload: true
            }
          );
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.view.amendments.search');
  };
}

ContractVersionsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractVersion',
  'newContractVersion'
];

ContractVersionsNewCtrl.$resolve = {
  newContractVersion: [
    'ContractVersion',
    '$stateParams',
    function(ContractVersion, $stateParams) {
      return ContractVersion.newContractVersion({
        id: $stateParams.id,
        type: $stateParams.t
      }).$promise;
    }
  ]
};

export { ContractVersionsNewCtrl };
