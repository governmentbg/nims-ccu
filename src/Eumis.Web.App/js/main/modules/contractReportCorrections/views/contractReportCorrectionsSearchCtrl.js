import _ from 'lodash';

function ContractReportCorrectionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  contractReportCorrections
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

  $scope.contractReportCorrections = contractReportCorrections;

  $scope.search = function() {
    return $state.go('root.contractReportCorrections.search', {
      programmeId: $scope.filters.programmeId,
      type: $scope.filters.type,
      status: $scope.filters.status
    });
  };
}

ContractReportCorrectionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractReportCorrections'
];

ContractReportCorrectionsSearchCtrl.$resolve = {
  contractReportCorrections: [
    '$stateParams',
    'ContractReportCorrection',
    function($stateParams, ContractReportCorrection) {
      return ContractReportCorrection.query($stateParams).$promise;
    }
  ]
};

export { ContractReportCorrectionsSearchCtrl };
