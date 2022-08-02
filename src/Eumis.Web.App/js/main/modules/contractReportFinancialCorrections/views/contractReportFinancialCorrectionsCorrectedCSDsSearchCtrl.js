import _ from 'lodash';

function ContractReportFinancialCorrectionsCorrectedCSDsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  scConfirm,
  contractReportFinancialCorrectionCSDs
) {
  $scope.contractReportFinancialCorrectionId = $stateParams.id;

  $scope.contractReportFinancialCorrectionCSDs = _.forEach(
    contractReportFinancialCorrectionCSDs,
    function(item) {
      csdNameCreator(item);
    }
  );

  $scope.filters = {
    csd: null,
    company: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.contractReportFinancialCorrections.view.correctedCsds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportFinancialCorrectionsCorrectedCSDsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'scConfirm',
  'contractReportFinancialCorrectionCSDs'
];

ContractReportFinancialCorrectionsCorrectedCSDsSearchCtrl.$resolve = {
  contractReportFinancialCorrectionCSDs: [
    '$stateParams',
    'ContractReportFinancialCorrectionCSD',
    function($stateParams, ContractReportFinancialCorrectionCSD) {
      return ContractReportFinancialCorrectionCSD.query($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsCorrectedCSDsSearchCtrl };
