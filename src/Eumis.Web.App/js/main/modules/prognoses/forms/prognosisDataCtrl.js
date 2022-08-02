import angular from 'angular';

function PrognosisDataCtrl($scope, scModal, moneyOperation) {
  var calcParams = {};
  switch ($scope.model.level) {
    case 'programme':
      calcParams.programmeId = $scope.model.programmeId;
      break;
    case 'programmePriority':
      calcParams.programmePriorityId = $scope.model.programmePriorityId;
      break;
    case 'procedure':
      calcParams.procedureId = $scope.model.procedureId;
      break;
  }

  $scope.calculateContracted = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.contractedEuAmount = result.euAmount;
      $scope.model.contractedBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.contractedEuAmount, model.contractedBgAmount]',
    function() {
      $scope.model.contractedBfpAmount = moneyOperation.addAmounts(
        $scope.model.contractedEuAmount,
        $scope.model.contractedBgAmount
      );
    },
    true
  );

  $scope.calculatePayment = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.paymentEuAmount = result.euAmount;
      $scope.model.paymentBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.paymentEuAmount, model.paymentBgAmount]',
    function() {
      $scope.model.paymentBfpAmount = moneyOperation.addAmounts(
        $scope.model.paymentEuAmount,
        $scope.model.paymentBgAmount
      );
    },
    true
  );

  $scope.calculateAdvancePayment = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.advancePaymentEuAmount = result.euAmount;
      $scope.model.advancePaymentBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.advancePaymentEuAmount, model.advancePaymentBgAmount]',
    function() {
      $scope.model.advancePaymentBfpAmount = moneyOperation.addAmounts(
        $scope.model.advancePaymentEuAmount,
        $scope.model.advancePaymentBgAmount
      );
    },
    true
  );

  $scope.calculateVerAdvancePayment = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.advanceVerPaymentEuAmount = result.euAmount;
      $scope.model.advanceVerPaymentBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.advanceVerPaymentEuAmount, model.advanceVerPaymentBgAmount]',
    function() {
      $scope.model.advanceVerPaymentBfpAmount = moneyOperation.addAmounts(
        $scope.model.advanceVerPaymentEuAmount,
        $scope.model.advanceVerPaymentBgAmount
      );
    },
    true
  );

  $scope.calculateIntermediatePayment = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.intermediatePaymentEuAmount = result.euAmount;
      $scope.model.intermediatePaymentBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.intermediatePaymentEuAmount, model.intermediatePaymentBgAmount]',
    function() {
      $scope.model.intermediatePaymentBfpAmount = moneyOperation.addAmounts(
        $scope.model.intermediatePaymentEuAmount,
        $scope.model.intermediatePaymentBgAmount
      );
    },
    true
  );

  $scope.calculateFinalPayment = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.finalPaymentEuAmount = result.euAmount;
      $scope.model.finalPaymentBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.finalPaymentEuAmount, model.finalPaymentBgAmount]',
    function() {
      $scope.model.finalPaymentBfpAmount = moneyOperation.addAmounts(
        $scope.model.finalPaymentEuAmount,
        $scope.model.finalPaymentBgAmount
      );
    },
    true
  );

  $scope.calculateApproved = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.approvedEuAmount = result.euAmount;
      $scope.model.approvedBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.approvedEuAmount, model.approvedBgAmount]',
    function() {
      $scope.model.approvedBfpAmount = moneyOperation.addAmounts(
        $scope.model.approvedEuAmount,
        $scope.model.approvedBgAmount
      );
    },
    true
  );

  $scope.calculateCertified = function() {
    var modalInstance = scModal.open('bfpCalculatorModal', calcParams);

    modalInstance.result.then(function(result) {
      $scope.model.certifiedEuAmount = result.euAmount;
      $scope.model.certifiedBgAmount = result.bgAmount;
    }, angular.noop);

    return modalInstance.opened;
  };
  $scope.$watch(
    '[model.certifiedEuAmount, model.certifiedBgAmount]',
    function() {
      $scope.model.certifiedBfpAmount = moneyOperation.addAmounts(
        $scope.model.certifiedEuAmount,
        $scope.model.certifiedBgAmount
      );
    },
    true
  );
}

PrognosisDataCtrl.$inject = ['$scope', 'scModal', 'moneyOperation'];

export { PrognosisDataCtrl };
