import _ from 'lodash';

function ContractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  scConfirm,
  contractReportFinancialCertCorrectionCSDs
) {
  $scope.contractReportFinancialCertCorrectionId = $stateParams.id;

  $scope.contractReportFinancialCertCorrectionCSDs = _.forEach(
    contractReportFinancialCertCorrectionCSDs,
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
    return $state.go('root.contractReportFinancialCertCorrections.view.correctedCsds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'scConfirm',
  'contractReportFinancialCertCorrectionCSDs'
];

ContractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl.$resolve = {
  contractReportFinancialCertCorrectionCSDs: [
    '$stateParams',
    'ContractReportFinancialCertCorrectionCSD',
    function($stateParams, ContractReportFinancialCertCorrectionCSD) {
      return ContractReportFinancialCertCorrectionCSD.query($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsCorrectedCSDsSearchCtrl };
