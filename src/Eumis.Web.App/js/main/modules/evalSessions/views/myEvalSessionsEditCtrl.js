function MyEvalSessionsEditCtrl($scope, $state, $stateParams, evalSession) {
  $scope.evalSession = evalSession;
}

MyEvalSessionsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'evalSession'];

MyEvalSessionsEditCtrl.$resolve = {
  evalSession: [
    'MyEvalSession',
    '$stateParams',
    function(MyEvalSession, $stateParams) {
      return MyEvalSession.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { MyEvalSessionsEditCtrl };
