function ProcedureLocationCtrl($scope) {
  $scope.changedNutsLevel = function() {
    $scope.model.countryId = null;
    $scope.model.nuts1Id = null;
    $scope.model.nuts2Id = null;
    $scope.model.districtId = null;
    $scope.model.municipalityId = null;
    $scope.model.settlementId = null;
  };
}

ProcedureLocationCtrl.$inject = ['$scope'];

export { ProcedureLocationCtrl };
