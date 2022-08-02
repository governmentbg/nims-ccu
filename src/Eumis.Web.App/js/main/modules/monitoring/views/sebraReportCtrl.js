function SebraReportCtrl($scope) {
  $scope.uploadMode = false;

  $scope.filters = {
    programmeId: null,
    procedureId: null,
    type: null,
    fromDate: null,
    toDate: null,
    fromNumber: null,
    toNumber: null,
    sendername: null,
    acc: null,
    o1: null
  };

  $scope.changeMode = function() {
    $scope.uploadMode = !$scope.uploadMode;
  };
}

SebraReportCtrl.$inject = ['$scope'];

export { SebraReportCtrl };
