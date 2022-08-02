import _ from 'lodash';

function ContractReportCertCorrectionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  contractReportCertCorrections
) {
  $scope.filters = {
    programmeId: null,
    type: null,
    status: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.contractReportCertCorrections = contractReportCertCorrections;

  $scope.search = function() {
    return $state.go('root.contractReportCertCorrections.search', {
      programmeId: $scope.filters.programmeId,
      type: $scope.filters.type,
      status: $scope.filters.status
    });
  };
}

ContractReportCertCorrectionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractReportCertCorrections'
];

ContractReportCertCorrectionsSearchCtrl.$resolve = {
  contractReportCertCorrections: [
    '$stateParams',
    'ContractReportCertCorrection',
    function($stateParams, ContractReportCertCorrection) {
      return ContractReportCertCorrection.query($stateParams).$promise;
    }
  ]
};

export { ContractReportCertCorrectionsSearchCtrl };
