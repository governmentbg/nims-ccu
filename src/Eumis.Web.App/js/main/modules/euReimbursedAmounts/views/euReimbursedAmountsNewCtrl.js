function EuReimbursedAmountsNewCtrl($scope, $state, EuReimbursedAmount, newAmount) {
  $scope.newAmount = newAmount;

  $scope.save = function() {
    return $scope.euReimbursedAmountsNewForm.$validate().then(function() {
      if ($scope.euReimbursedAmountsNewForm.$valid) {
        return EuReimbursedAmount.save($scope.newAmount).$promise.then(function(result) {
          return $state.go('root.euReimbursedAmounts.view.amount', {
            id: result.euReimbursedAmountId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.euReimbursedAmounts.search');
  };
}

EuReimbursedAmountsNewCtrl.$inject = ['$scope', '$state', 'EuReimbursedAmount', 'newAmount'];

EuReimbursedAmountsNewCtrl.$resolve = {
  newAmount: [
    'EuReimbursedAmount',
    function(EuReimbursedAmount) {
      return EuReimbursedAmount.newAmount().$promise;
    }
  ]
};

export { EuReimbursedAmountsNewCtrl };
