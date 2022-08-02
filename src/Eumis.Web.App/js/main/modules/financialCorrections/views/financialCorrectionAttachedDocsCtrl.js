function FinancialCorrectionAttachedDocsCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  financialCorrectionContractReports,
  financialCorrectionContractReportCorrections,
  financialCorrectionContractDebts,
  financialCorrectionCertReports,
  financialCorrectionIrregularities
) {
  $scope.financialCorrectionContractReports = financialCorrectionContractReports;
  $scope.financialCorrectionContractReportCorrections = financialCorrectionContractReportCorrections;
  $scope.financialCorrectionContractDebts = financialCorrectionContractDebts;
  $scope.financialCorrectionCertReports = financialCorrectionCertReports;
  $scope.financialCorrectionIrregularities = financialCorrectionIrregularities;
  $scope.financialCorrectionOrderNum = $scope.financialCorrectionInfo.orderNum;
  $scope.financialCorrectionIsDeleted = $scope.financialCorrectionInfo.status === 'removed';
  $scope.financialCorrectionId = $stateParams.id;
}

FinancialCorrectionAttachedDocsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'financialCorrectionContractReports',
  'financialCorrectionContractReportCorrections',
  'financialCorrectionContractDebts',
  'financialCorrectionCertReports',
  'financialCorrectionIrregularities'
];

FinancialCorrectionAttachedDocsCtrl.$resolve = {
  financialCorrectionContractReports: [
    '$stateParams',
    'FinancialCorrection',
    function($stateParams, FinancialCorrection) {
      return FinancialCorrection.getContractReports($stateParams).$promise;
    }
  ],
  financialCorrectionContractReportCorrections: [
    '$stateParams',
    'FinancialCorrection',
    function($stateParams, FinancialCorrection) {
      return FinancialCorrection.getContractReportCorrections($stateParams).$promise;
    }
  ],
  financialCorrectionContractDebts: [
    '$stateParams',
    'FinancialCorrection',
    function($stateParams, FinancialCorrection) {
      return FinancialCorrection.getContractDebts($stateParams).$promise;
    }
  ],
  financialCorrectionCertReports: [
    '$stateParams',
    'FinancialCorrection',
    function($stateParams, FinancialCorrection) {
      return FinancialCorrection.getCertReports($stateParams).$promise;
    }
  ],
  financialCorrectionIrregularities: [
    '$stateParams',
    'FinancialCorrection',
    function($stateParams, FinancialCorrection) {
      return FinancialCorrection.getIrregularities($stateParams).$promise;
    }
  ]
};

export { FinancialCorrectionAttachedDocsCtrl };
