function ChoosePaymentTypeModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ContractReportPayment
) {
  $scope.model = {};
  $scope.contractReportId = scModalParams.contractReportId;
  $scope.errors = null;

  $scope.ok = function() {
    return $scope.choosePaymentTypeForm.$validate().then(function() {
      if ($scope.choosePaymentTypeForm.$valid) {
        return ContractReportPayment.canCreate(
          {
            id: $scope.contractReportId,
            type: $scope.model.type
          },
          {}
        ).$promise.then(function(result) {
          if (!result.errors.length) {
            return ContractReportPayment.save(
              {
                id: $scope.contractReportId,
                type: $scope.model.type
              },
              {}
            ).$promise.then(function(result) {
              return $uibModalInstance.close(result.contractReportPaymentId);
            });
          } else {
            $scope.errors = result.errors;
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

ChoosePaymentTypeModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ContractReportPayment'
];

export { ChoosePaymentTypeModalCtrl };
