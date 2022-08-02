function ContractAccessCodesEditCtrl($scope, $state, contractAccessCode) {
  $scope.contractAccessCode = contractAccessCode;

  $scope.back = function() {
    return $state.go('root.contractAccessCodes.search');
  };
}

ContractAccessCodesEditCtrl.$inject = ['$scope', '$state', 'contractAccessCode'];

ContractAccessCodesEditCtrl.$resolve = {
  contractAccessCode: [
    'ContractAccessCode',
    '$stateParams',
    function(ContractAccessCode, $stateParams) {
      return ContractAccessCode.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractAccessCodesEditCtrl };
