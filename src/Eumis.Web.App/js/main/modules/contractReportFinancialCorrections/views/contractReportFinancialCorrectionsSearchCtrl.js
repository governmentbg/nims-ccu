import _ from 'lodash';

function ContractReportFinancialCorrectionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportFinancialCorrections
) {
  $scope.contractId = $stateParams.id;
  $scope.contractReportFinancialCorrections = contractReportFinancialCorrections;

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
    return $state.go('root.contractReportFinancialCorrections.search', {
      contractRegNum: $scope.filters.contractRegNum,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate
    });
  };
}

ContractReportFinancialCorrectionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportFinancialCorrections'
];

ContractReportFinancialCorrectionsSearchCtrl.$resolve = {
  contractReportFinancialCorrections: [
    '$stateParams',
    'ContractReportFinancialCorrection',
    function($stateParams, ContractReportFinancialCorrection) {
      return ContractReportFinancialCorrection.query($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsSearchCtrl };
