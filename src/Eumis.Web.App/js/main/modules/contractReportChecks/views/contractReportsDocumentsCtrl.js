function ContractReportChecksDocumentsCtrl(
  $scope,
  $state,
  $stateParams,
  contractReportFinancials,
  contractReportTechnicals,
  contractReportPayments
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportSource = $scope.contractReportInfo.source;
  $scope.contractReportFinancials = contractReportFinancials;
  $scope.contractReportTechnicals = contractReportTechnicals;
  $scope.contractReportPayments = contractReportPayments;
}

ContractReportChecksDocumentsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contractReportFinancials',
  'contractReportTechnicals',
  'contractReportPayments'
];

ContractReportChecksDocumentsCtrl.$resolve = {
  contractReportFinancials: [
    '$stateParams',
    'ContractReportFinancial',
    function($stateParams, ContractReportFinancial) {
      return ContractReportFinancial.query($stateParams).$promise;
    }
  ],
  contractReportTechnicals: [
    '$stateParams',
    'ContractReportTechnical',
    function($stateParams, ContractReportTechnical) {
      return ContractReportTechnical.query($stateParams).$promise;
    }
  ],
  contractReportPayments: [
    '$stateParams',
    'ContractReportPayment',
    function($stateParams, ContractReportPayment) {
      return ContractReportPayment.query($stateParams).$promise;
    }
  ]
};

export { ContractReportChecksDocumentsCtrl };
