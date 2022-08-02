import angular from 'angular';

function UserTypeCtrl($scope, $state, $stateParams, scFormParams, scModal) {
  $scope.isNew = scFormParams.isNew;

  $scope.viewPermissionTemplate = function() {
    if (!!$scope.model.permissionTemplateId) {
      var modalInstance = scModal.open('permissionTemplateModal', {
        permissionTemplateId: $scope.model.permissionTemplateId
      });
      modalInstance.result.catch(angular.noop);
      return modalInstance.opened;
    }
  };
}

UserTypeCtrl.$inject = ['$scope', '$state', '$stateParams', 'scFormParams', 'scModal'];

export { UserTypeCtrl };
