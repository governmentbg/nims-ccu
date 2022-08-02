import _ from 'lodash';

function ContractReportsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  scConfirm,
  contractReports
) {
  $scope.contractId = $stateParams.id;
  $scope.contractReports = contractReports;

  $scope.contractReportsExportUrl = $interpolate(
    'api/contractReports/excelExport?' + 'contractRegNum={{contractRegNum}}'
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
    return $state.go('root.contractReports.search', {
      contractRegNum: $scope.filters.contractRegNum
    });
  };
}

ContractReportsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'scConfirm',
  'contractReports'
];

ContractReportsSearchCtrl.$resolve = {
  contractReports: [
    '$stateParams',
    'ContractReport',
    function($stateParams, ContractReport) {
      return ContractReport.query($stateParams).$promise;
    }
  ]
};

export { ContractReportsSearchCtrl };
