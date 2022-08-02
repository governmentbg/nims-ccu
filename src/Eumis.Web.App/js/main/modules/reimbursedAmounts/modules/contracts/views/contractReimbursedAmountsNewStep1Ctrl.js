import angular from 'angular';

function ContractReimbursedAmountsNewStep1Ctrl($q, $scope, $state, scModal, scConfirm, Contract) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.reimbursedAmountsNewStep1Form.$validate().then(function() {
      if ($scope.reimbursedAmountsNewStep1Form.$valid) {
        return $state.go('root.contractReimbursedAmounts.newStep2', {
          cNum: $scope.model.contractNumber
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReimbursedAmounts.search');
  };

  $scope.chooseContract = function() {
    var modalInstance = scModal.open('chooseReimbursedAmountsContractModal', {
      programmeId: $scope.model.programmeId,
      contractNumber: $scope.model.contractNumber
    });

    modalInstance.result.then(function(contract) {
      $scope.model.programmeId = contract.programmeId;
      $scope.model.contractNumber = contract.regNumber;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.isValidContractNum = function(contractNumber) {
    if (!contractNumber) {
      return $q.resolve();
    }

    return Contract.isRegNumExisting({
      programmeId: $scope.model.programmeId,
      contractNum: contractNumber
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

ContractReimbursedAmountsNewStep1Ctrl.$inject = [
  '$q',
  '$scope',
  '$state',
  'scModal',
  'scConfirm',
  'Contract'
];

export { ContractReimbursedAmountsNewStep1Ctrl };
