function ContractReportRevalidationsViewCtrl(
  $scope,
  $interpolate,
  l10n,
  contractReportRevalidationInfo
) {
  $scope.infoText = $interpolate(
    l10n.get('contractReportRevalidations_' + 'viewContractReportRevalidation_statusInfo')
  )({
    status: contractReportRevalidationInfo.statusDescr,
    programme: contractReportRevalidationInfo.programmeCode
  });

  $scope.contractReportRevalidationInfo = contractReportRevalidationInfo;
}

ContractReportRevalidationsViewCtrl.$inject = [
  '$scope',
  '$interpolate',
  'l10n',
  'contractReportRevalidationInfo'
];

ContractReportRevalidationsViewCtrl.$resolve = {
  contractReportRevalidationInfo: [
    'ContractReportRevalidation',
    '$stateParams',
    function(ContractReportRevalidation, $stateParams) {
      return ContractReportRevalidation.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportRevalidationsViewCtrl };
