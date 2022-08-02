import _ from 'lodash';

function FIReimbursedAmountsSearchCtrl($scope, $state, $stateParams, reimbursedAmounts) {
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
    return $state.go('root.fiReimbursedAmounts.search', {
      contractId: $scope.filters.contractId,
      type: $scope.filters.type
    });
  };
}

FIReimbursedAmountsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'reimbursedAmounts'];

FIReimbursedAmountsSearchCtrl.$resolve = {
  reimbursedAmounts: [
    '$stateParams',
    'FIReimbursedAmount',
    function($stateParams, FIReimbursedAmount) {
      return FIReimbursedAmount.query($stateParams).$promise;
    }
  ]
};

export { FIReimbursedAmountsSearchCtrl };
