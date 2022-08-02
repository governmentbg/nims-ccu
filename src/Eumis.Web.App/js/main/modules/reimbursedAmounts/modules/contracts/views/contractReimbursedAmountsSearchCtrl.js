import _ from 'lodash';

function ContractReimbursedAmountsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  reimbursedAmounts
) {
  $scope.contractsReimbursedAmountsExportUrl = $interpolate(
    'api/contarctReimbursedAmounts/excelExport?' + 'contractId={{contractId}}&type={{type}}'
  )({
    contractId: $stateParams.contractId,
    type: $stateParams.type
  });

  $scope.filters = {
    contractId: null,
    type: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.reimbursedAmounts = reimbursedAmounts;

  $scope.search = function() {
    return $state.go('root.contractReimbursedAmounts.search', {
      contractId: $scope.filters.contractId,
      type: $scope.filters.type
    });
  };
}

ContractReimbursedAmountsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'reimbursedAmounts'
];

ContractReimbursedAmountsSearchCtrl.$resolve = {
  reimbursedAmounts: [
    '$stateParams',
    'ContractReimbursedAmount',
    function($stateParams, ContractReimbursedAmount) {
      return ContractReimbursedAmount.query($stateParams).$promise;
    }
  ]
};

export { ContractReimbursedAmountsSearchCtrl };
