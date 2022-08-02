import angular from 'angular';

function ActuallyPaidAmountsNewStep1Ctrl($q, $scope, $state, scModal, scConfirm, Contract) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.paidAmountsNewForm.$validate().then(function() {
      if ($scope.paidAmountsNewForm.$valid) {
        return scConfirm({
          validationAction: 'canCreate',
          resource: 'ActuallyPaidAmount',
          params: {
            contractRegNumber: $scope.model.contractNumber
          }
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.actuallyPaidAmounts.newStep2', {
              cNum: $scope.model.contractNumber
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.actuallyPaidAmounts.search');
  };

  $scope.chooseContract = function() {
    var modalInstance = scModal.open('chooseActuallyPaidAmountContractModal', {
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

ActuallyPaidAmountsNewStep1Ctrl.$inject = [
  '$q',
  '$scope',
  '$state',
  'scModal',
  'scConfirm',
  'Contract'
];

export { ActuallyPaidAmountsNewStep1Ctrl };
