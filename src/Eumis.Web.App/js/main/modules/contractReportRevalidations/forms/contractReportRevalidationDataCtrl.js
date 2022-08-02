import angular from 'angular';

function ContractReportRevalidationDataCtrl($scope, scFormParams, scModal, moneyOperation) {
  $scope.approvedEdit = scFormParams.approvedEdit;
  $scope.certEdit = scFormParams.certEdit;

  $scope.calculate = function(type) {
    var params = {},
      modalInstance;
    if ($scope.model.type === 'paymentRevalidated' || $scope.model.type === 'contractRevalidated') {
      params.contractId = $scope.model.contractId;
      params.programmePriorityId = $scope.model.programmePriorityId;
    } else if ($scope.model.type === 'programeRevalidated') {
      params.programmeId = $scope.model.programmeId;
    } else if ($scope.model.type === 'programePriorityRevalidated') {
      params.programmePriorityId = $scope.model.programmePriorityId;
    } else if ($scope.model.type === 'procedureRevalidated') {
      params.procedureId = $scope.model.procedureId;
      params.programmePriorityId = $scope.model.programmePriorityId;
    }

    modalInstance = scModal.open('bfpCalculatorModal', params);

    modalInstance.result.then(function(result) {
      if (type === 'revalidated') {
        $scope.model.revalidatedEuAmount = result.euAmount;
        $scope.model.revalidatedBgAmount = result.bgAmount;
      } else if (type === 'cert') {
        $scope.model.uncertifiedRevalidatedEuAmount = result.euAmount;
        $scope.model.uncertifiedRevalidatedBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  if ($scope.approvedEdit) {
    $scope.$watch(
      '[model.revalidatedEuAmount, model.revalidatedBgAmount]',
      function() {
        $scope.model.revalidatedBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.revalidatedEuAmount,
          $scope.model.revalidatedBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedBfpTotalAmount, model.revalidatedSelfAmount]',
      function() {
        $scope.model.revalidatedTotalAmount = moneyOperation.addAmounts(
          $scope.model.revalidatedBfpTotalAmount,
          $scope.model.revalidatedSelfAmount
        );
      },
      true
    );
  }

  if ($scope.certEdit) {
    $scope.$watch(
      '[model.uncertifiedRevalidatedEuAmount, model.uncertifiedRevalidatedBgAmount]',
      function() {
        $scope.model.uncertifiedRevalidatedBfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.uncertifiedRevalidatedEuAmount,
          $scope.model.uncertifiedRevalidatedBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.uncertifiedRevalidatedBfpTotalAmount, model.uncertifiedRevalidatedSelfAmount]',
      function() {
        $scope.model.uncertifiedRevalidatedTotalAmount = moneyOperation.addAmounts(
          $scope.model.uncertifiedRevalidatedBfpTotalAmount,
          $scope.model.uncertifiedRevalidatedSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedEuAmount, model.uncertifiedRevalidatedEuAmount]',
      function() {
        $scope.model.certifiedRevalidatedEuAmount = moneyOperation.subtractAmounts(
          $scope.model.revalidatedEuAmount,
          $scope.model.uncertifiedRevalidatedEuAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedBgAmount, model.uncertifiedRevalidatedBgAmount]',
      function() {
        $scope.model.certifiedRevalidatedBgAmount = moneyOperation.subtractAmounts(
          $scope.model.revalidatedBgAmount,
          $scope.model.uncertifiedRevalidatedBgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedBfpTotalAmount, model.uncertifiedRevalidatedBfpTotalAmount]',
      function() {
        $scope.model.certifiedRevalidatedBfpTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.revalidatedBfpTotalAmount,
          $scope.model.uncertifiedRevalidatedBfpTotalAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedSelfAmount, model.uncertifiedRevalidatedSelfAmount]',
      function() {
        $scope.model.certifiedRevalidatedSelfAmount = moneyOperation.subtractAmounts(
          $scope.model.revalidatedSelfAmount,
          $scope.model.uncertifiedRevalidatedSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedTotalAmount, model.uncertifiedRevalidatedTotalAmount]',
      function() {
        $scope.model.certifiedRevalidatedTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.revalidatedTotalAmount,
          $scope.model.uncertifiedRevalidatedTotalAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.revalidatedCrossAmount, model.uncertifiedRevalidatedCrossAmount]',
      function() {
        $scope.model.certifiedRevalidatedCrossAmount = moneyOperation.subtractAmounts(
          $scope.model.revalidatedCrossAmount,
          $scope.model.uncertifiedRevalidatedCrossAmount
        );
      },
      true
    );
  }
}

ContractReportRevalidationDataCtrl.$inject = [
  '$scope',
  'scFormParams',
  'scModal',
  'moneyOperation'
];

export { ContractReportRevalidationDataCtrl };
