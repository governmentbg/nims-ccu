function UsersEditCtrl($scope, $state, $stateParams, scConfirm, User, user) {
  $scope.user = user;

  var userCommand = function(method, msg, validationAction) {
    return scConfirm({
      confirmMessage: msg,
      resource: 'User',
      action: method,
      validationAction: validationAction,
      params: {
        id: $stateParams.id,
        version: $scope.user.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.deleteUser = function() {
    return userCommand('deleteUser', 'users_edit_confirmDelete', null);
  };

  $scope.recover = function() {
    return userCommand('recover', 'users_edit_confirmRecover', 'canRecover');
  };

  $scope.lock = function() {
    return userCommand('lock', 'users_edit_confirmLock', null);
  };

  $scope.unlock = function() {
    return userCommand('unlock', 'users_edit_confirmUnlock', null);
  };
}

UsersEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'scConfirm', 'User', 'user'];

UsersEditCtrl.$resolve = {
  user: [
    '$stateParams',
    'User',
    function($stateParams, User) {
      return User.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { UsersEditCtrl };
