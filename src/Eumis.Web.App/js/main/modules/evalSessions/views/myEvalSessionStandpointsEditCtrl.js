function MyEvalSessionStandpointsEditCtrl($scope, $state, evalSessionStandpoint) {
  $scope.evalSessionStandpoint = evalSessionStandpoint;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.standpointUpdated = function() {
    return $state.partialReload();
  };
}

MyEvalSessionStandpointsEditCtrl.$inject = ['$scope', '$state', 'evalSessionStandpoint'];

MyEvalSessionStandpointsEditCtrl.$resolve = {
  evalSessionStandpoint: [
    '$stateParams',
    'MyEvalSessionStandpoint',
    function($stateParams, MyEvalSessionStandpoint) {
      return MyEvalSessionStandpoint.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { MyEvalSessionStandpointsEditCtrl };
