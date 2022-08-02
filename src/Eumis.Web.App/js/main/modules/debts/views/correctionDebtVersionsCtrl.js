function CorrectionDebtVersionsCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  correctionDebtVersions
) {
  $scope.correctionDebtVersions = correctionDebtVersions;
  $scope.correctionDebtIsDeleted = $scope.correctionDebtInfo.status === 'removed';
  $scope.correctionDebtId = $stateParams.id;

  $scope.newAmendment = function() {
    return scConfirm({
      validationAction: 'canCreate',
      resource: 'CorrectionDebtVersion',
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.correctionDebts.view.versions.edit', {
          ind: result.result.correctionDebtVersionId
        });
      }
    });
  };
}

CorrectionDebtVersionsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'correctionDebtVersions'
];

CorrectionDebtVersionsCtrl.$resolve = {
  correctionDebtVersions: [
    '$stateParams',
    'CorrectionDebtVersion',
    function($stateParams, CorrectionDebtVersion) {
      return CorrectionDebtVersion.query($stateParams).$promise;
    }
  ]
};

export { CorrectionDebtVersionsCtrl };
