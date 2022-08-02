import _ from 'lodash';

function ContractReportChecksReportsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  scConfirm,
  contractReports
) {
  $scope.contractId = $stateParams.id;
  $scope.contractReports = contractReports;

  $scope.contractReportChecksExportUrl = $interpolate(
    'api/contractReportChecks/excelExport?' + 'contractRegNum={{contractRegNum}}'
  )({
    contractRegNum: $stateParams.contractRegNum
  });

  $scope.filters = {
    contractRegNum: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.contractReportChecks.search', {
      contractRegNum: $scope.filters.contractRegNum
    });
  };
}

ContractReportChecksReportsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'scConfirm',
  'contractReports'
];

ContractReportChecksReportsSearchCtrl.$resolve = {
  contractReports: [
    '$stateParams',
    'ContractReport',
    function($stateParams, ContractReport) {
      return ContractReport.getReportsForCheck($stateParams).$promise;
    }
  ]
};

export { ContractReportChecksReportsSearchCtrl };
