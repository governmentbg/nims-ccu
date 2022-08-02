function ChangePasswordModalCtrl($q, $scope, $uibModalInstance, User) {
  $scope.passwords = {};
  $scope.passwordRegex = new RegExp(window.eumisConfiguration.passwordRegex);
  $scope.passwordInvalidFormatMessage = window.eumisConfiguration.passwordInvalidFormatMessage;

  $scope.save = function() {
    return $scope.changePasswordForm.$validate().then(function() {
      if ($scope.changePasswordForm.$valid) {
        return User.changePassword($scope.passwords).$promise.then(function() {
          return $uibModalInstance.close();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };

  $scope.matchPasswords = function(confirmNewPassword) {
    if (!$scope.passwords.newPassword) {
      return true;
    }
    return $scope.passwords.newPassword === confirmNewPassword;
  };

  $scope.checkPassword = function(password) {
    if (!password) {
      return $q.resolve();
    }
    return User.isCorrectPassword(JSON.stringify(password)).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

ChangePasswordModalCtrl.$inject = ['$q', '$scope', '$uibModalInstance', 'User'];

export { ChangePasswordModalCtrl };
