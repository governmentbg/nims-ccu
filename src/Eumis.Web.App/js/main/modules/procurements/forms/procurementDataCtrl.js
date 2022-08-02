function ProcurementDataCtrl($scope) {
  $scope.clearErrandType = function() {
    $scope.model.errandTypeId = undefined;
  };
}

ProcurementDataCtrl.$inject = ['$scope'];

export { ProcurementDataCtrl };
