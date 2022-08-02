function ChooseUserModalCtrl($scope, $uibModalInstance, NotificationSetting) {
  $scope.ok = function() {
    return NotificationSetting.copyUserSettings({
      uid: this.model.user
    }).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseUserModalCtrl.$inject = ['$scope', '$uibModalInstance', 'NotificationSetting'];

export { ChooseUserModalCtrl };
