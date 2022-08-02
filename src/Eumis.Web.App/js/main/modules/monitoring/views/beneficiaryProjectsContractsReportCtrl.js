function BeneficiaryProjectsContractsReportCtrl($scope, Monitoring) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null,
    currency: null,
    companyTypeId: null,
    companyLegalTypeId: null
  };

  $scope.displayResult = false;

  $scope.search = function() {
    $scope.displayResult = false;

    return Monitoring.getBeneficiaryProjectsContractsReport({
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      currency: $scope.filters.currency,
      companyTypeId: $scope.filters.companyTypeId,
      companyLegalTypeId: $scope.filters.companyLegalTypeId
    }).$promise.then(function(result) {
      $scope.beneficiaryProjectsContracts = result;
      $scope.displayResult = true;
      $scope.exportUrl =
        'api/monitoringReports/beneficiaryProjectsContracts/export?' +
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
        '&companyTypeId=' +
        $scope.filters.companyTypeId +
        '&companyLegalTypeId=' +
        $scope.filters.companyLegalTypeId;
    });
  };
}

BeneficiaryProjectsContractsReportCtrl.$inject = ['$scope', 'Monitoring'];

export { BeneficiaryProjectsContractsReportCtrl };
