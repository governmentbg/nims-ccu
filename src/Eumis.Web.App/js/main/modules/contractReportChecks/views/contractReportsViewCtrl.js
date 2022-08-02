import _ from 'lodash';
function ContractReportChecksViewCtrl(
  $scope,
  l10n,
  $interpolate,
  contractInfo,
  contractReportInfo
) {
  $scope.contractInfo = contractInfo;
  $scope.contractReportInfo = contractReportInfo;
  $scope.titleText = $interpolate(l10n.get('contractReportChecks_viewContractReport_title'))({
    contractName: contractInfo.name,
    reportStatus: contractReportInfo.statusDescription
  });

  $scope.tabList = {
    contractReportChecks_tabs_contract: 'root.contractReportChecks.view.contract',
    contractReportChecks_tabs_report: 'root.contractReportChecks.view.data',
    contractReportChecks_tabs_documents: 'root.contractReportChecks.view.documents',
    contractReportChecks_tabs_checks: 'root.contractReportChecks.view.checks',
    contractReportChecks_tabs_csds: 'root.contractReportChecks.view.csds',
    contractReportChecks_tabs_advPaymentAmounts: 'root.contractReportChecks.view.advPaymentAmounts',
    contractReportChecks_tabs_advNVPaymentAmounts:
      'root.contractReportChecks.view.advNVPaymentAmounts'
  };

  if ($scope.contractInfo.isIndicatorSectionVisible) {
    _.assign($scope.tabList, {
      contractReportChecks_tabs_indicators: 'root.contractReportChecks.view.indicators'
    });
  }

  _.assign($scope.tabList, {
    contractReportChecks_tabs_attachedDocs: 'root.contractReportChecks.view.attachedDocs',
    contractReportChecks_tabs_paymentChecks: 'root.contractReportChecks.view.paymentChecks',
    contractReportChecks_tabs_paymentRequests: 'root.contractReportChecks.view.paymentRequests'
  });
}

ContractReportChecksViewCtrl.$inject = [
  '$scope',
  'l10n',
  '$interpolate',
  'contractInfo',
  'contractReportInfo'
];

ContractReportChecksViewCtrl.$resolve = {
  contractInfo: [
    'Contract',
    '$stateParams',
    function(Contract, $stateParams) {
      return Contract.getInfoForReport({ id: $stateParams.id }).$promise;
    }
  ],
  contractReportInfo: [
    'ContractReport',
    '$stateParams',
    function(ContractReport, $stateParams) {
      return ContractReport.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractReportChecksViewCtrl };
