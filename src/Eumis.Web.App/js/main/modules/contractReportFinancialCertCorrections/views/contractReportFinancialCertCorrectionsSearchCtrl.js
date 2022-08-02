import _ from 'lodash';

function ContractReportFinancialCertCorrectionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportFinancialCertCorrections
) {
  $scope.contractId = $stateParams.id;
  $scope.contractReportFinancialCertCorrections = contractReportFinancialCertCorrections;

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
    return $state.go('root.contractReportFinancialCertCorrections.search', {
      contractRegNum: $scope.filters.contractRegNum,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate
    });
  };
}

ContractReportFinancialCertCorrectionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportFinancialCertCorrections'
];

ContractReportFinancialCertCorrectionsSearchCtrl.$resolve = {
  contractReportFinancialCertCorrections: [
    '$stateParams',
    'ContractReportFinancialCertCorrection',
    function($stateParams, ContractReportFinancialCertCorrection) {
      return ContractReportFinancialCertCorrection.query($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsSearchCtrl };
