function ContractRegistrationsSearchCtrl($scope, $stateParams, contractRegs) {
  $scope.contractRegsExportUrl = 'api/contractRegistrations/excelExport';

  $scope.contractRegs = contractRegs;
}

ContractRegistrationsSearchCtrl.$inject = ['$scope', '$stateParams', 'contractRegs'];

ContractRegistrationsSearchCtrl.$resolve = {
  contractRegs: [
    'ContractRegistration',
    function(ContractRegistration) {
      return ContractRegistration.query().$promise;
    }
  ]
};

export { ContractRegistrationsSearchCtrl };
