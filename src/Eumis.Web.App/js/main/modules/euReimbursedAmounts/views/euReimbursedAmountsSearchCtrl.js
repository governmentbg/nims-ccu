import _ from 'lodash';

function EuReimbursedAmountsSearchCtrl($scope, $state, $stateParams, euReimbursedAmounts) {
  $scope.filters = {
    status: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.euReimbursedAmounts = euReimbursedAmounts;

  $scope.search = function() {
    return $state.go('root.euReimbursedAmounts.search', {
      status: $scope.filters.status
    });
  };
}

EuReimbursedAmountsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'euReimbursedAmounts'];

EuReimbursedAmountsSearchCtrl.$resolve = {
  euReimbursedAmounts: [
    '$stateParams',
    'EuReimbursedAmount',
    function($stateParams, EuReimbursedAmount) {
      return EuReimbursedAmount.query($stateParams).$promise;
    }
  ]
};

export { EuReimbursedAmountsSearchCtrl };
