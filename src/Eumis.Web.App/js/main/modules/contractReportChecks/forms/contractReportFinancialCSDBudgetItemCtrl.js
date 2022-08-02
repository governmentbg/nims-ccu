import angular from 'angular';
import _ from 'lodash';

function ContractReportFinancialCSDBudgetItemCtrl(
  $scope,
  scFormParams,
  scModal,
  moneyOperation,
  ContractReportFinancialCSDFile
) {
  $scope.company =
    $scope.model.contractReportFinancialCSD.companyUinType +
    ' ' +
    $scope.model.contractReportFinancialCSD.companyUin +
    ' ' +
    $scope.model.contractReportFinancialCSD.companyName;

  $scope.approvedEdit = scFormParams.approvedEdit;
  $scope.certEdit = scFormParams.certEdit;

  $scope.calculate = function(type) {
    var modalInstance = scModal.open('bfpCalculatorModal', {
      contractId: $scope.model.contractId,
      contractBudgetLevel3AmountId: $scope.model.contractBudgetLevel3AmountId
    });

    modalInstance.result.then(function(result) {
      if (type === 'unapproved') {
        $scope.model.unapprovedEuAmount = result.euAmount;
        $scope.model.unapprovedBgAmount = result.bgAmount;
      } else if (type === 'unapprovedByCorrection') {
        $scope.model.unapprovedByCorrectionEuAmount = result.euAmount;
        $scope.model.unapprovedByCorrectionBgAmount = result.bgAmount;
      } else if (type === 'uncertifiedApproved') {
        $scope.model.uncertifiedApprovedEuAmount = result.euAmount;
        $scope.model.uncertifiedApprovedBgAmount = result.bgAmount;
      }
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.model.contractReportFinancialCSD.files = _.map(
    $scope.model.contractReportFinancialCSD.files,
    function(file) {
      file.url = ContractReportFinancialCSDFile.getUrl({
        id: $scope.model.contractReportFinancialCSDBudgetItemId,
        fileKey: file.key
      });

      return file;
    }
  );

  $scope.correctionTypeChanged = function() {
    $scope.model.irregularityId = null;
    $scope.model.financialCorrectionId = null;
  };

  $scope.isBfpAmountChanged = function(bfpTotalAmount) {
    if (bfpTotalAmount !== $scope.model.originalBfpTotalAmount) {
      return false;
    }
    return true;
  };

  $scope.isTotalAmountChanged = function(totalAmount) {
    if (totalAmount !== $scope.model.originalTotalAmount) {
      return false;
    }
    return true;
  };

  if ($scope.approvedEdit) {
    $scope.$watch(
      '[model.euAmount, model.bgAmount]',
      function() {
        $scope.model.bfpTotalAmount = moneyOperation.addAmounts(
          $scope.model.euAmount,
          $scope.model.bgAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.bfpTotalAmount, model.selfAmount]',
      function() {
        $scope.model.totalAmount = moneyOperation.addAmounts(
          $scope.model.bfpTotalAmount,
          $scope.model.selfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.unapprovedByCorrectionBfpTotalAmount, model.unapprovedByCorrectionSelfAmount]',
      function() {
        $scope.model.unapprovedByCorrectionTotalAmount = moneyOperation.addAmounts(
          $scope.model.unapprovedByCorrectionBfpTotalAmount,
          $scope.model.unapprovedByCorrectionSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.bfpTotalAmount, model.unapprovedBfpTotalAmount, ' +
        'model.unapprovedByCorrectionBfpTotalAmount]',
      function() {
        $scope.model.approvedBfpTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.bfpTotalAmount,
          moneyOperation.addAmounts(
            $scope.model.unapprovedBfpTotalAmount,
            $scope.model.unapprovedByCorrectionBfpTotalAmount
          )
        );
      },
      true
    );
    $scope.$watch(
      '[model.selfAmount, model.unapprovedSelfAmount, model.unapprovedByCorrectionSelfAmount]',
      function() {
        $scope.model.approvedSelfAmount = moneyOperation.subtractAmounts(
          $scope.model.selfAmount,
          moneyOperation.addAmounts(
            $scope.model.unapprovedSelfAmount,
            $scope.model.unapprovedByCorrectionSelfAmount
          )
        );
      },
      true
    );
    $scope.$watch(
      '[model.totalAmount, model.unapprovedTotalAmount, model.unapprovedByCorrectionTotalAmount]',
      function() {
        $scope.model.approvedTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.originalBfpTotalAmount,
          moneyOperation.addAmounts(
            $scope.model.unapprovedTotalAmount,
            $scope.model.unapprovedByCorrectionTotalAmount
          )
        );
      },
      true
    );
  }

  if ($scope.certEdit) {
    $scope.$watch(
      '[model.uncertifiedApprovedBfpTotalAmount, model.uncertifiedApprovedSelfAmount]',
      function() {
        $scope.model.uncertifiedApprovedTotalAmount = moneyOperation.addAmounts(
          $scope.model.uncertifiedApprovedBfpTotalAmount,
          $scope.model.uncertifiedApprovedSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.approvedBfpTotalAmount, model.uncertifiedApprovedBfpTotalAmount]',
      function() {
        $scope.model.certifiedApprovedBfpTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.approvedBfpTotalAmount,
          $scope.model.uncertifiedApprovedBfpTotalAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.approvedSelfAmount, model.uncertifiedApprovedSelfAmount]',
      function() {
        $scope.model.certifiedApprovedSelfAmount = moneyOperation.subtractAmounts(
          $scope.model.approvedSelfAmount,
          $scope.model.uncertifiedApprovedSelfAmount
        );
      },
      true
    );
    $scope.$watch(
      '[model.approvedTotalAmount, model.uncertifiedApprovedTotalAmount]',
      function() {
        $scope.model.certifiedApprovedTotalAmount = moneyOperation.subtractAmounts(
          $scope.model.approvedTotalAmount,
          $scope.model.uncertifiedApprovedTotalAmount
        );
      },
      true
    );
  }
}

ContractReportFinancialCSDBudgetItemCtrl.$inject = [
  '$scope',
  'scFormParams',
  'scModal',
  'moneyOperation',
  'ContractReportFinancialCSDFile'
];

export { ContractReportFinancialCSDBudgetItemCtrl };
