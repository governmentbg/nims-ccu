import _ from 'lodash';

function ContractReportTechnicalCorrectionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportTechnicalCorrections
) {
  $scope.contractId = $stateParams.id;
  $scope.contractReportTechnicalCorrections = contractReportTechnicalCorrections;

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
    return $state.go('root.contractReportTechnicalCorrections.search', {
      contractRegNum: $scope.filters.contractRegNum,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate
    });
  };
}

ContractReportTechnicalCorrectionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportTechnicalCorrections'
];

ContractReportTechnicalCorrectionsSearchCtrl.$resolve = {
  contractReportTechnicalCorrections: [
    '$stateParams',
    'ContractReportTechnicalCorrection',
    function($stateParams, ContractReportTechnicalCorrection) {
      return ContractReportTechnicalCorrection.query($stateParams).$promise;
    }
  ]
};

export { ContractReportTechnicalCorrectionsSearchCtrl };
