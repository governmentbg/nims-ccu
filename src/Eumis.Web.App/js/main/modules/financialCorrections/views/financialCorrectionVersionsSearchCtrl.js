function FinancialCorrectionVersionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  financialCorrectionVersions
) {
  $scope.financialCorrectionVersions = financialCorrectionVersions;
  $scope.financialCorrectionOrderNum = $scope.financialCorrectionInfo.orderNum;
  $scope.financialCorrectionIsDeleted = $scope.financialCorrectionInfo.status === 'removed';
  $scope.financialCorrectionId = $stateParams.id;

  $scope.newAmendment = function() {
    return scConfirm({
      validationAction: 'canCreate',
      resource: 'FinancialCorrectionVersion',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.financialCorrections.view.versions.edit', {
          ind: result.result.financialCorrectionVersionId
        });
      }
    });
  };
}

FinancialCorrectionVersionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'financialCorrectionVersions'
];

FinancialCorrectionVersionsSearchCtrl.$resolve = {
  financialCorrectionVersions: [
    '$stateParams',
    'FinancialCorrectionVersion',
    function($stateParams, FinancialCorrectionVersion) {
      return FinancialCorrectionVersion.query($stateParams).$promise;
    }
  ]
};

export { FinancialCorrectionVersionsSearchCtrl };
