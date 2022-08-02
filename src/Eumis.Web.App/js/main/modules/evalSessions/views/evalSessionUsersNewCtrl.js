import angular from 'angular';

function EvalSessionUsersNewCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  EvalSessionUser,
  newEvalSessionUser
) {
  $scope.newEvalSessionUser = newEvalSessionUser;

  $scope.save = function() {
    return $scope.newEvalSessionUserForm.$validate().then(function() {
      if ($scope.newEvalSessionUserForm.$valid) {
        return EvalSessionUser.canAdd(
          {
            id: $stateParams.id,
            userId: $scope.newEvalSessionUser.userId,
            userType: $scope.newEvalSessionUser.type
          },
          {}
        ).$promise.then(function(result) {
          if (!result.errors.length) {
            return EvalSessionUser.save(
              { id: $stateParams.id },
              $scope.newEvalSessionUser
            ).$promise.then(function() {
              return $state.go('root.evalSessions.view.users.search');
            });
          } else {
            var modalInstance = scModal.open('validationErrorsModal', {
              errors: result.errors
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.users.search');
  };
}

EvalSessionUsersNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'EvalSessionUser',
  'newEvalSessionUser'
];

EvalSessionUsersNewCtrl.$resolve = {
  newEvalSessionUser: [
    '$stateParams',
    'EvalSessionUser',
    function($stateParams, EvalSessionUser) {
      return EvalSessionUser.newEvalSessionUser({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { EvalSessionUsersNewCtrl };
