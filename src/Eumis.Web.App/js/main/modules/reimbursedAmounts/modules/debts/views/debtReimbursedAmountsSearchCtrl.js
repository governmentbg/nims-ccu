import _ from 'lodash';

function DebtReimbursedAmountsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  reimbursedAmounts
) {
  $scope.debtReimbursedAmountsExportUrl = $interpolate(
    'api/debtReimbursedAmounts/excelExport?' + 'contractId={{contractId}}&type={{type}}'
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
    return $state.go('root.debtReimbursedAmounts.search', {
      contractId: $scope.filters.contractId,
      type: $scope.filters.type
    });
  };
}

DebtReimbursedAmountsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'reimbursedAmounts'
];

DebtReimbursedAmountsSearchCtrl.$resolve = {
  reimbursedAmounts: [
    '$stateParams',
    'DebtReimbursedAmount',
    function($stateParams, DebtReimbursedAmount) {
      return DebtReimbursedAmount.query($stateParams).$promise;
    }
  ]
};

export { DebtReimbursedAmountsSearchCtrl };
