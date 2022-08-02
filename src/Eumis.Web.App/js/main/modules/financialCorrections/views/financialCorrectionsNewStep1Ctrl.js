import angular from 'angular';

function FinancialCorrectionsNewStep1Ctrl($q, $scope, $state, scModal, scConfirm, Contract) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.financialCorrectionsNewStep1Form.$validate().then(function() {
      if ($scope.financialCorrectionsNewStep1Form.$valid) {
        return scConfirm({
          resource: 'FinancialCorrection',
          validationAction: 'canCreate',
          params: {
            contractRegNumber: $scope.model.contractNumber
          }
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.financialCorrections.newStep2', {
              cNum: $scope.model.contractNumber
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.financialCorrections.search');
  };

  $scope.chooseContract = function() {
    var modalInstance = scModal.open('chooseFinancialCorrectionContractModal', {
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

FinancialCorrectionsNewStep1Ctrl.$inject = [
  '$q',
  '$scope',
  '$state',
  'scModal',
  'scConfirm',
  'Contract'
];

export { FinancialCorrectionsNewStep1Ctrl };
