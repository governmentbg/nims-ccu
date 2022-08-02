function BudgetLevelsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null,
    budgetLevel: null,
    currency: null,
    nutsLevel: null,
    countryId: null,
    nuts1Id: null,
    nuts2Id: null,
    districtId: null,
    municipalityId: null,
    settlementId: null,
    protectedZoneId: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return $scope.budgetLevelsReportForm.$validate().then(function() {
      if ($scope.budgetLevelsReportForm.$valid) {
        return Monitoring.getBudgetLevelsReport({
          budgetLevel: $scope.filters.budgetLevel,
          programmeId: $scope.filters.programmeId,
          programmePriorityId: $scope.filters.programmePriorityId,
          procedureId: $scope.filters.procedureId,
          fromDate: $scope.filters.fromDate,
          toDate: $scope.filters.toDate,
          currency: $scope.filters.currency,
          countryId: $scope.filters.countryId,
          nuts1Id: $scope.filters.nuts1Id,
          nuts2Id: $scope.filters.nuts2Id,
          districtId: $scope.filters.districtId,
          municipalityId: $scope.filters.municipalityId,
          settlementId: $scope.filters.settlementId,
          protectedZoneId: $scope.filters.protectedZoneId
        }).$promise.then(function(result) {
          $scope.budgetLevels = result;
          $scope.displayResult = true;
          $scope.exportUrl =
            'api/monitoringReports/budgetLevels/export?' +
            'budgetLevel=' +
            $scope.filters.budgetLevel +
            '&programmeId=' +
            $scope.filters.programmeId +
            '&programmePriorityId=' +
            $scope.filters.programmePriorityId +
            '&procedureId=' +
            $scope.filters.procedureId +
            '&fromDate=' +
            $scope.filters.fromDate +
            '&toDate=' +
            $scope.filters.toDate +
            '&currency=' +
            $scope.filters.currency +
            '&countryId=' +
            $scope.filters.countryId +
            '&nuts1Id=' +
            $scope.filters.nuts1Id +
            '&nuts2Id=' +
            $scope.filters.nuts2Id +
            '&districtId=' +
            $scope.filters.districtId +
            '&municipalityId=' +
            $scope.filters.municipalityId +
            '&settlementId=' +
            $scope.filters.settlementId +
            '&protectedZoneId=' +
            $scope.filters.protectedZoneId;
        });
      }
    });
  };

  $scope.changedNutsLevel = function() {
    $scope.filters.countryId = null;
    $scope.filters.nuts1Id = null;
    $scope.filters.nuts2Id = null;
    $scope.filters.districtId = null;
    $scope.filters.municipalityId = null;
    $scope.filters.settlementId = null;
  };
}

BudgetLevelsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { BudgetLevelsReportCtrl };
