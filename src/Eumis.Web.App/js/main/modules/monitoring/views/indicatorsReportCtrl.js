function IndicatorsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    toDate: null,
    contractExecutionStatus: null,
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

    return Monitoring.geIndicatorsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      toDate: $scope.filters.toDate,
      contractExecutionStatus: $scope.filters.contractExecutionStatus,
      countryId: $scope.filters.countryId,
      nuts1Id: $scope.filters.nuts1Id,
      nuts2Id: $scope.filters.nuts2Id,
      districtId: $scope.filters.districtId,
      municipalityId: $scope.filters.municipalityId,
      settlementId: $scope.filters.settlementId,
      protectedZoneId: $scope.filters.protectedZoneId
    }).$promise.then(function(result) {
      $scope.indicators = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/indicators/export?' +
        'programmeId=' +
        $scope.filters.programmeId +
        '&programmePriorityId=' +
        $scope.filters.programmePriorityId +
        '&procedureId=' +
        $scope.filters.procedureId +
        '&toDate=' +
        $scope.filters.toDate +
        '&contractExecutionStatus=' +
        $scope.filters.contractExecutionStatus +
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

IndicatorsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { IndicatorsReportCtrl };
