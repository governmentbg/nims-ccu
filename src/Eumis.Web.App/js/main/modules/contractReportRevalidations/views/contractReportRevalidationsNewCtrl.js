function ContractReportRevalidationsNewCtrl(
  $scope,
  $state,
  ContractReportRevalidation,
  newContractReportRevalidation
) {
  $scope.newContractReportRevalidation = newContractReportRevalidation;

  $scope.save = function() {
    return $scope.contractReportRevalidationsNewForm.$validate().then(function() {
      if ($scope.contractReportRevalidationsNewForm.$valid) {
        return ContractReportRevalidation.save($scope.newContractReportRevalidation).$promise.then(
          function(result) {
            $state.go('root.contractReportRevalidations.view.basicData', {
              id: result.contractReportRevalidationId
            });
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportRevalidations.search');
  };

  $scope.typeChanged = function() {
    $scope.newContractReportRevalidation.programmeId = null;
    $scope.newContractReportRevalidation.programmePriorityId = null;
    $scope.newContractReportRevalidation.procedureId = null;
    $scope.newContractReportRevalidation.contractId = null;
    $scope.newContractReportRevalidation.contractReportPaymentId = null;
  };
}

ContractReportRevalidationsNewCtrl.$inject = [
  '$scope',
  '$state',
  'ContractReportRevalidation',
  'newContractReportRevalidation'
];

ContractReportRevalidationsNewCtrl.$resolve = {
  newContractReportRevalidation: [
    'ContractReportRevalidation',
    function(ContractReportRevalidation) {
      return ContractReportRevalidation.newContractReportRevalidation().$promise;
    }
  ]
};

export { ContractReportRevalidationsNewCtrl };
