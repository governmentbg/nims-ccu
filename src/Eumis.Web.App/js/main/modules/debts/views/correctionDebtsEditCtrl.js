function CorrectionDebtsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  CorrectionDebt,
  correctionDebt
) {
  $scope.editMode = null;
  $scope.correctionDebt = correctionDebt;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editCorrectionDebtForm.$validate().then(function() {
      if ($scope.editCorrectionDebtForm.$valid) {
        return CorrectionDebt.update(
          {
            id: $stateParams.id
          },
          $scope.correctionDebt
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.delDebt = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'CorrectionDebt',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.correctionDebt.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.correctionDebts.search');
      }
    });
  };

  $scope.deactivateDebt = function() {
    return scConfirm({
      confirmMessage: 'correctionDebts_editCorrectionDebt_deactivateConfirm',
      noteLabel: 'correctionDebts_editCorrectionDebt_deactivateMsg',
      resource: 'CorrectionDebt',
      action: 'cancel',
      params: {
        id: $stateParams.id,
        version: $scope.correctionDebt.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

CorrectionDebtsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'CorrectionDebt',
  'correctionDebt'
];

CorrectionDebtsEditCtrl.$resolve = {
  correctionDebt: [
    'CorrectionDebt',
    '$stateParams',
    function(CorrectionDebt, $stateParams) {
      return CorrectionDebt.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { CorrectionDebtsEditCtrl };
