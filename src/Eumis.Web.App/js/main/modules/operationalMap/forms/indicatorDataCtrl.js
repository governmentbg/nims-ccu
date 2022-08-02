function IndicatorDataCtrl($q, $scope, scFormParams, Indicator) {
  $scope.editMode = scFormParams.editMode;
  $scope.mapNodeIndicatorType = scFormParams.mapNodeIndicatorType;
  $scope.procedureId = scFormParams.procedureId;

  $scope.isUnique = function(code) {
    if (
      $scope.readonly ||
      $scope.editMode ||
      code === null ||
      code === undefined ||
      $scope.model.programmeId === null ||
      $scope.model.programmeId === undefined ||
      $scope.model.type === null ||
      $scope.model.type === undefined ||
      $scope.model.kind === null ||
      $scope.model.kind === undefined
    ) {
      return $q.resolve();
    }
    return Indicator.isUnique({
      code: code,
      programmeId: $scope.model.programmeId,
      type: $scope.model.type,
      kind: $scope.model.kind
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

IndicatorDataCtrl.$inject = ['$q', '$scope', 'scFormParams', 'Indicator'];

export { IndicatorDataCtrl };
