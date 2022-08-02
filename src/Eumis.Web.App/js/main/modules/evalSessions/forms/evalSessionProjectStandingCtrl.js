function EvalSessionProjectStandingCtrl($q, $scope, EvalSessionProjectStanding, scFormParams) {
  $scope.isNew = scFormParams.isNew;

  $scope.isUniqueOrderNum = function(orderNum) {
    if (!orderNum || !$scope.isNew) {
      return $q.resolve();
    }
    return EvalSessionProjectStanding.isOrderNumUnique({
      id: $scope.model.evalSessionId,
      ind: $scope.model.projectId,
      isPreliminary: $scope.model.isPreliminary,
      orderNum: orderNum
    }).$promise.then(function(isUnique) {
      return isUnique ? $q.resolve() : $q.reject();
    });
  };

  $scope.statusChanged = function() {
    $scope.model.orderNum = null;
    $scope.model.rejectionReasonId = null;
  };
}

EvalSessionProjectStandingCtrl.$inject = [
  '$q',
  '$scope',
  'EvalSessionProjectStanding',
  'scFormParams'
];

export { EvalSessionProjectStandingCtrl };
