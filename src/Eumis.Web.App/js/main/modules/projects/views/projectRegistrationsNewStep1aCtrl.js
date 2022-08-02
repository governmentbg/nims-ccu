import angular from 'angular';

function ProjectRegistrationsNewStep1aCtrl($scope, $state, scModal, uinValidation) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.projectRegistrationsNewStep1aForm.$validate().then(function() {
      if ($scope.projectRegistrationsNewStep1aForm.$valid) {
        return $state.go('root.projects.newStep2', {
          pId: $scope.model.procedureId,
          uinType: $scope.model.uinTypeId,
          uin: $scope.model.uin
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.projects.search');
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

ProjectRegistrationsNewStep1aCtrl.$inject = ['$scope', '$state', 'scModal', 'uinValidation'];

export { ProjectRegistrationsNewStep1aCtrl };
