import _ from 'lodash';

function ContractReportPaymentCheckCtrl($scope, scFormParams, moneyOperation, Nomenclatures) {
  _.forEach($scope.model.contractReportPaymentCheckAmounts, function(amount) {
    Nomenclatures.get({
      alias: 'programmePriorities',
      valueAlias: amount.programmePriorityId
    }).$promise.then(function(pPriority) {
      amount.header = pPriority.name;
    });
  });

  $scope.paidAmountChanged = function(index) {
    if (
      $scope.model.contractReportPaymentCheckAmounts[index].paidEuAmount !== null &&
      $scope.model.contractReportPaymentCheckAmounts[index].paidEuAmount !== undefined &&
      $scope.model.contractReportPaymentCheckAmounts[index].paidBgAmount !== null &&
      $scope.model.contractReportPaymentCheckAmounts[index].paidBgAmount !== undefined
    ) {
      $scope.model.contractReportPaymentCheckAmounts[index].paidBfpTotalAmount =
        $scope.model.contractReportPaymentCheckAmounts[index].paidEuAmount +
        $scope.model.contractReportPaymentCheckAmounts[index].paidBgAmount;
    } else {
      $scope.model.contractReportPaymentCheckAmounts[index].paidBfpTotalAmount = null;
    }
  };
}

ContractReportPaymentCheckCtrl.$inject = [
  '$scope',
  'scFormParams',
  'moneyOperation',
  'Nomenclatures'
];

export { ContractReportPaymentCheckCtrl };
