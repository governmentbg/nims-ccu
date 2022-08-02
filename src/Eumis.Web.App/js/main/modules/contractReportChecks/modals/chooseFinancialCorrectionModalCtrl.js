function ChooseFinancialCorrectionModalCtrl(
  $scope,
  scConfirm,
  $uibModalInstance,
  scModalParams,
  ContractReportAttachedFinancialCorrection,
  corrections
) {
  $scope.corrections = corrections;

  $scope.choose = function(correction) {
    return scConfirm({
      validationAction: 'canAttach',
      resource: 'ContractReportAttachedFinancialCorrection',
      action: 'save',
      params: {
        id: scModalParams.contractReportId,
        contractReportFinancialCorrectionId: correction.contractReportFinancialCorrectionId,
        version: scModalParams.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $uibModalInstance.close(result.result.contractReportFinancialCorrectionId);
      } else {
        return $uibModalInstance.dismiss('cancel');
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseFinancialCorrectionModalCtrl.$inject = [
  '$scope',
  'scConfirm',
  '$uibModalInstance',
  'scModalParams',
  'ContractReportAttachedFinancialCorrection',
  'corrections'
];

ChooseFinancialCorrectionModalCtrl.$resolve = {
  corrections: [
    'ContractReportAttachedFinancialCorrection',
    'scModalParams',
    function(ContractReportAttachedFinancialCorrection, scModalParams) {
      return ContractReportAttachedFinancialCorrection.getCorrections({
        id: scModalParams.contractReportId
      }).$promise;
    }
  ]
};

export { ChooseFinancialCorrectionModalCtrl };
