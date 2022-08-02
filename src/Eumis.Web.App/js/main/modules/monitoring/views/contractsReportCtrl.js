function ContractsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null,
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
  $scope.resultIsClipped = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getContractsReport({
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
      $scope.contracts = result.items;
      $scope.displayResult = true;
      $scope.resultIsClipped = result.resultIsClipped;
      $scope.exportUrl =
        'api/monitoringReports/contracts/export?' +
        'programmeId=' +
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

ContractsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { ContractsReportCtrl };
