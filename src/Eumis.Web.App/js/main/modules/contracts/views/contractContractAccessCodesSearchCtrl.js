function ContractContractAccessCodesSearchCtrl(
  $scope,
  $state,
  $stateParams,
  ContractContractAccessCode,
  contractAccessCodes,
  accessCodesInfo
) {
  $scope.contractAccessCodes = contractAccessCodes;

  $scope.showCodes = accessCodesInfo.showAccessCodes;
}

ContractContractAccessCodesSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractContractAccessCode',
  'contractAccessCodes',
  'accessCodesInfo'
];

ContractContractAccessCodesSearchCtrl.$resolve = {
  contractAccessCodes: [
    '$stateParams',
    'ContractContractAccessCode',
    function($stateParams, ContractContractAccessCode) {
      return ContractContractAccessCode.query({
        id: $stateParams.id
      }).$promise;
    }
  ],
  accessCodesInfo: [
    '$stateParams',
    'ContractContractAccessCode',
    function($stateParams, ContractAccessCode) {
      return ContractAccessCode.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractContractAccessCodesSearchCtrl };
