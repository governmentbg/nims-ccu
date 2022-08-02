function EvalSessionUsersSearchCtrl($scope, $stateParams, evalSessionUsers) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionDraft = $scope.evalSessionInfo.evalSessionStatusName === 'draft';
  $scope.evalSessionUsers = evalSessionUsers;
}

EvalSessionUsersSearchCtrl.$inject = ['$scope', '$stateParams', 'evalSessionUsers'];

EvalSessionUsersSearchCtrl.$resolve = {
  evalSessionUsers: [
    '$stateParams',
    'EvalSessionUser',
    function($stateParams, EvalSessionUser) {
      return EvalSessionUser.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { EvalSessionUsersSearchCtrl };
