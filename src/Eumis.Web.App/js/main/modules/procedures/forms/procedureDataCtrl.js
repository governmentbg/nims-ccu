function ProcedureDataCtrl($scope) {
  $scope.isValidProjectMaxAmount = function(projectMaxAmount) {
    if (
      $scope.model.projectMinAmount === undefined ||
      $scope.model.projectMinAmount === null ||
      projectMaxAmount === undefined ||
      projectMaxAmount === null
    ) {
      return true;
    } else {
      return projectMaxAmount >= $scope.model.projectMinAmount;
    }
  };

  $scope.setLocalActionGroup = function(isIntroducedByLAG) {
    if (!isIntroducedByLAG) {
      $scope.model.localActionGroupId = null;
    }
  };
}

ProcedureDataCtrl.$inject = ['$scope'];

export { ProcedureDataCtrl };
