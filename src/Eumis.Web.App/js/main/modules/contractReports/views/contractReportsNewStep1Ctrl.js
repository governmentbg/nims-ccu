import angular from 'angular';

function ContractReportsNewStep1Ctrl(
  $q,
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  Contract
) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.contractsNewStep1Form.$validate().then(function() {
      if ($scope.contractsNewStep1Form.$valid) {
        return $state.go('root.contractReports.newStep2', {
          cNum: $scope.model.contractNumber
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReports.search');
  };

  $scope.chooseContract = function() {
    var modalInstance = scModal.open('chooseContractReportContractModal', {
      procedureId: $scope.model.procedureId,
      contractNumber: $scope.model.contractNumber
    });

    modalInstance.result.then(function(contract) {
      $scope.model.procedureId = contract.procedureId;
      $scope.model.contractNumber = contract.regNumber;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.isValidContractNum = function(contractNumber) {
    if (!contractNumber) {
      return $q.resolve();
    }

    return Contract.isRegNumExisting({
      procedureId: $scope.model.procedureId,
      contractNum: contractNumber
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

ContractReportsNewStep1Ctrl.$inject = [
  '$q',
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'Contract'
];

export { ContractReportsNewStep1Ctrl };
