import _ from 'lodash';

function ActuallyPaidAmountsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  actuallyPaidAmounts
) {
  $scope.actuallyPaidAmountsExportUrl = $interpolate(
    'api/actuallyPaidAmounts/excelExport?' +
      'contractId={{contractId}}&paymentReason={{paymentReason}}'
  )({
    contractId: $stateParams.contractId,
    paymentReason: $stateParams.paymentReason
  });

  $scope.filters = {
    contractId: null,
    paymentReason: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.actuallyPaidAmounts = actuallyPaidAmounts;

  $scope.search = function() {
    return $state.go('root.actuallyPaidAmounts.search', {
      contractId: $scope.filters.contractId,
      paymentReason: $scope.filters.paymentReason
    });
  };
}

ActuallyPaidAmountsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'actuallyPaidAmounts'
];

ActuallyPaidAmountsSearchCtrl.$resolve = {
  actuallyPaidAmounts: [
    '$stateParams',
    'ActuallyPaidAmount',
    function($stateParams, ActuallyPaidAmount) {
      return ActuallyPaidAmount.query($stateParams).$promise;
    }
  ]
};

export { ActuallyPaidAmountsSearchCtrl };
