function ContractAccessCodesSearchCtrl($scope, contractAccessCodes, accessCodesInfo) {
  $scope.contractAccessCodes = contractAccessCodes;
  $scope.accessCodesExportUrl = 'api/contractAccessCodes/excelExport';

  $scope.showCodes = accessCodesInfo.showAccessCodes;
}

ContractAccessCodesSearchCtrl.$inject = ['$scope', 'contractAccessCodes', 'accessCodesInfo'];

ContractAccessCodesSearchCtrl.$resolve = {
  contractAccessCodes: [
    '$stateParams',
    'ContractAccessCode',
    function($stateParams, ContractAccessCode) {
      return ContractAccessCode.query({
        id: $stateParams.id
      }).$promise;
    }
  ],
  accessCodesInfo: [
    'ContractAccessCode',
    function(ContractAccessCode) {
      return ContractAccessCode.getInfo().$promise;
    }
  ]
};

export { ContractAccessCodesSearchCtrl };
