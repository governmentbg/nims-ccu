import _ from 'lodash';

function ContractReportRevalidationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  contractReportRevalidations
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

  $scope.contractReportRevalidations = contractReportRevalidations;

  $scope.search = function() {
    return $state.go('root.contractReportRevalidations.search', {
      programmeId: $scope.filters.programmeId,
      type: $scope.filters.type,
      status: $scope.filters.status
    });
  };
}

ContractReportRevalidationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractReportRevalidations'
];

ContractReportRevalidationsSearchCtrl.$resolve = {
  contractReportRevalidations: [
    '$stateParams',
    'ContractReportRevalidation',
    function($stateParams, ContractReportRevalidation) {
      return ContractReportRevalidation.query($stateParams).$promise;
    }
  ]
};

export { ContractReportRevalidationsSearchCtrl };
