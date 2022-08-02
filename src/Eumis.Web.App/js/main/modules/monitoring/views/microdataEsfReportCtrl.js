function MicrodataEsfReportCtrl($scope) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    toDate: null
  };
}

MicrodataEsfReportCtrl.$inject = ['$scope'];

export { MicrodataEsfReportCtrl };
