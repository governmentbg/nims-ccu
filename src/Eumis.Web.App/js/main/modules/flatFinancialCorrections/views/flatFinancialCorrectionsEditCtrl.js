function FlatFinancialCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  FlatFinancialCorrection,
  flatFinancialCorrection
) {
  $scope.editMode = null;
  $scope.flatFinancialCorrection = flatFinancialCorrection;

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'actual') {
      confirmMsg = 'flatFinancialCorrections_editFlatFinancialCorrection_actualReason';
      validationAction = 'canChangeStatusToActual';
    }

    if (status === 'draft') {
      confirmMsg = 'flatFinancialCorrections_editFlatFinancialCorrection_draftReason';
      validationAction = 'canChangeStatusToDraft';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'FlatFinancialCorrection',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        version: flatFinancialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editFlatFinancialCorrectionForm.$validate().then(function() {
      if ($scope.editFlatFinancialCorrectionForm.$valid) {
        return FlatFinancialCorrection.update(
          { id: $stateParams.id },
          $scope.flatFinancialCorrection
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'FlatFinancialCorrection',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.flatFinancialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.flatFinancialCorrections.search');
      }
    });
  };
}

FlatFinancialCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'FlatFinancialCorrection',
  'flatFinancialCorrection'
];

FlatFinancialCorrectionsEditCtrl.$resolve = {
  flatFinancialCorrection: [
    'FlatFinancialCorrection',
    '$stateParams',
    function(FlatFinancialCorrection, $stateParams) {
      return FlatFinancialCorrection.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { FlatFinancialCorrectionsEditCtrl };
