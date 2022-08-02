import _ from 'lodash';

function ContractReportFinancialRevalidationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportFinancialRevalidations
) {
  $scope.contractId = $stateParams.id;
  $scope.contractReportFinancialRevalidations = contractReportFinancialRevalidations;

  $scope.filters = {
    contractRegNum: null,
    fromDate: null,
    toDate: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.contractReportFinancialRevalidations.search', {
      contractRegNum: $scope.filters.contractRegNum,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate
    });
  };
}

ContractReportFinancialRevalidationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportFinancialRevalidations'
];

ContractReportFinancialRevalidationsSearchCtrl.$resolve = {
  contractReportFinancialRevalidations: [
    '$stateParams',
    'ContractReportFinancialRevalidation',
    function($stateParams, ContractReportFinancialRevalidation) {
      return ContractReportFinancialRevalidation.query($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsSearchCtrl };
