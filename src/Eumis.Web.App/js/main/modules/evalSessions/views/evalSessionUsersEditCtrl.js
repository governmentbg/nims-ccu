function EvalSessionUsersEditCtrl(
  $scope,
  $state,
  $stateParams,
  EvalSessionUser,
  evalSessionUser,
  scConfirm
) {
  $scope.evalSessionUser = evalSessionUser;
  var isSessionDraft = $scope.evalSessionInfo.evalSessionStatusName === 'draft';
  $scope.canEdit = isSessionDraft;
  $scope.canDelete = isSessionDraft && evalSessionUser.status === 'notActivated';
  $scope.canActivate = isSessionDraft && evalSessionUser.status === 'deactivated';
  $scope.canDeactivate = isSessionDraft && evalSessionUser.status === 'activated';
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.canEdit = false;
    $scope.canDelete = false;
    $scope.canActivate = false;
    $scope.canDeactivate = false;
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editEvalSessionUserForm.$validate().then(function() {
      if ($scope.editEvalSessionUserForm.$valid) {
        return EvalSessionUser.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.evalSessionUser
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteUser = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'EvalSessionUser',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionUser.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.users.search');
      }
    });
  };

  $scope.activate = function() {
    return EvalSessionUser.activate(
      {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionUser.version
      },
      {}
    ).$promise.then(function() {
      return $state.partialReload();
    });
  };

  $scope.deactivate = function() {
    return EvalSessionUser.deactivate(
      {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionUser.version
      },
      {}
    ).$promise.then(function() {
      return $state.partialReload();
    });
  };
}

EvalSessionUsersEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'EvalSessionUser',
  'evalSessionUser',
  'scConfirm'
];

EvalSessionUsersEditCtrl.$resolve = {
  evalSessionUser: [
    '$stateParams',
    'EvalSessionUser',
    function($stateParams, EvalSessionUser) {
      return EvalSessionUser.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { EvalSessionUsersEditCtrl };
