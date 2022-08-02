function ActuallyPaidAmountsEditCtrl($scope, $state, $stateParams, ActuallyPaidAmount, paidAmount) {
  $scope.editMode = null;
  $scope.paidAmount = paidAmount;
  $scope.status = $scope.paidAmountInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editPaidAmountData.$validate().then(function() {
      if ($scope.editPaidAmountData.$valid) {
        return ActuallyPaidAmount.update(
          {
            id: $stateParams.id
          },
          $scope.paidAmount
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

ActuallyPaidAmountsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ActuallyPaidAmount',
  'paidAmount'
];

ActuallyPaidAmountsEditCtrl.$resolve = {
  paidAmount: [
    'ActuallyPaidAmount',
    '$stateParams',
    function(ActuallyPaidAmount, $stateParams) {
      return ActuallyPaidAmount.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ActuallyPaidAmountsEditCtrl };
