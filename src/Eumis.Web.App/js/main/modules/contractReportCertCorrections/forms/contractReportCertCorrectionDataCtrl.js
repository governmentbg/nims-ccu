import angular from 'angular';

function ContractReportCertCorrectionDataCtrl($scope, scModal, moneyOperation) {
  $scope.calculate = function() {
    var params = {},
      modalInstance;

    if ($scope.model.type === 'paymentCertified' || $scope.model.type === 'contractCertified') {
      params.contractId = $scope.model.contractId;
      params.programmePriorityId = $scope.model.programmePriorityId;
    } else if ($scope.model.type === 'programeCertified') {
      params.programmeId = $scope.model.programmeId;
    } else if ($scope.model.type === 'programePriorityCertified') {
      params.programmePriorityId = $scope.model.programmePriorityId;
    } else if ($scope.model.type === 'procedureCertified') {
      params.procedureId = $scope.model.procedureId;
      params.programmePriorityId = $scope.model.programmePriorityId;
    }

    modalInstance = scModal.open('bfpCalculatorModal', params);

    modalInstance.result.then(function(result) {
      $scope.model.certifiedEuAmount = result.euAmount;
      $scope.model.certifiedBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.$watch(
    '[model.certifiedEuAmount, model.certifiedBgAmount]',
    function() {
      $scope.model.certifiedBfpTotalAmount = moneyOperation.addAmounts(
        $scope.model.certifiedEuAmount,
        $scope.model.certifiedBgAmount
      );
    },
    true
  );
  $scope.$watch(
    '[model.certifiedBfpTotalAmount, model.certifiedSelfAmount]',
    function() {
      $scope.model.certifiedTotalAmount = moneyOperation.addAmounts(
        $scope.model.certifiedBfpTotalAmount,
        $scope.model.certifiedSelfAmount
      );
    },
    true
  );
}

ContractReportCertCorrectionDataCtrl.$inject = ['$scope', 'scModal', 'moneyOperation'];

export { ContractReportCertCorrectionDataCtrl };
