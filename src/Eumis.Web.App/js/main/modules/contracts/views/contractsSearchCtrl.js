import _ from 'lodash';

function ContractsSearchCtrl($scope, $state, $stateParams, $interpolate, contracts) {
  $scope.contractsExportUrl = $interpolate(
    'api/contracts/excelExport?' +
      'procedureId={{procedureId}}&programmePriorityId={{programmePriorityId}}'
  )({
    procedureId: $stateParams.procedureId,
    programmePriorityId: $stateParams.programmePriorityId
  });

  $scope.filters = {
    procedureId: null,
    programmePriorityId: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.contracts = contracts;

  $scope.search = function() {
    return $state.go('root.contracts.search', {
      procedureId: $scope.filters.procedureId,
      programmePriorityId: $scope.filters.programmePriorityId
    });
  };
}

ContractsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', '$interpolate', 'contracts'];

ContractsSearchCtrl.$resolve = {
  contracts: [
    '$stateParams',
    'Contract',
    function($stateParams, Contract) {
      return Contract.query({
        procedureId: $stateParams.procedureId,
        programmePriorityId: $stateParams.programmePriorityId
      }).$promise;
    }
  ]
};

export { ContractsSearchCtrl };
