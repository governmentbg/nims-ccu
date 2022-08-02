import angular from 'angular';

function ServiceContractsNewStep1Ctrl($scope, $state, scModal, uinValidation) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.serviceContractsNewStep1Form.$validate().then(function() {
      if ($scope.serviceContractsNewStep1Form.$valid) {
        return $state.go('root.contracts.serviceContracts.newStep2', {
          pId: $scope.model.procedureId,
          uinType: $scope.model.uinTypeId,
          uin: $scope.model.uin
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.search');
  };

  $scope.chooseCompany = function() {
    var modalInstance = scModal.open('chooseCompanyModal', {
      uinTypeId: $scope.model.uinTypeId,
      uin: $scope.model.uin
    });

    modalInstance.result.then(function(company) {
      $scope.model.uinTypeId = company.uinTypeId;
      $scope.model.uin = company.uin;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.uinValid = function(uin) {
    return uinValidation.uinValid(uin, $scope.model.uinTypeId);
  };
}

ServiceContractsNewStep1Ctrl.$inject = ['$scope', '$state', 'scModal', 'uinValidation'];

export { ServiceContractsNewStep1Ctrl };
