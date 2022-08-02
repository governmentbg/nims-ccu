function CorrectionDebtVersionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  CorrectionDebtVersion,
  correctionDebtVersion
) {
  $scope.editMode = null;
  $scope.correctionDebtVersion = correctionDebtVersion;
  $scope.correctionDebtStatus = $scope.correctionDebtInfo.status;

  $scope.save = function() {
    return $scope.editCorrectionDebtVersionForm.$validate().then(function() {
      if ($scope.editCorrectionDebtVersionForm.$valid) {
        return CorrectionDebtVersion.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.correctionDebtVersion
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'actual') {
      confirmMsg = 'correctionDebts_editCorrectionDebtVersion_actualReason';
      validationAction = 'canChangeStatusToActual';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'CorrectionDebtVersion',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.correctionDebtVersion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'CorrectionDebtVersion',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.correctionDebtVersion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.correctionDebts.view.versions');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };
}

CorrectionDebtVersionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'CorrectionDebtVersion',
  'correctionDebtVersion'
];

CorrectionDebtVersionsEditCtrl.$resolve = {
  correctionDebtVersion: [
    'CorrectionDebtVersion',
    '$stateParams',
    function(CorrectionDebtVersion, $stateParams) {
      return CorrectionDebtVersion.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { CorrectionDebtVersionsEditCtrl };
