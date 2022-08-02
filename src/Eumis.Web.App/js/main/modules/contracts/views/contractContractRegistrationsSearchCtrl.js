import _ from 'lodash';

function ContractContractRegistrationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  ContractContractRegistration,
  ContractContractRegistrationFile,
  contractRegs
) {
  $scope.contractId = $stateParams.id;
  $scope.contractRegs = contractRegs;

  $scope.contractRegs = _.map(contractRegs, function(item) {
    if (item.file) {
      item.file.url = ContractContractRegistrationFile.getUrl({
        id: $scope.contractId,
        fileKey: item.file.key
      });
    }
    return item;
  });
}

ContractContractRegistrationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'ContractContractRegistration',
  'ContractContractRegistrationFile',
  'contractRegs'
];

ContractContractRegistrationsSearchCtrl.$resolve = {
  contractRegs: [
    '$stateParams',
    'ContractContractRegistration',
    function($stateParams, ContractContractRegistration) {
      return ContractContractRegistration.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractContractRegistrationsSearchCtrl };
