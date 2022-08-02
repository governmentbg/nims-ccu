function FinancialCorrectionVersionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  FinancialCorrectionVersion,
  financialCorrectionVersion
) {
  $scope.editMode = null;
  $scope.financialCorrectionVersion = financialCorrectionVersion;
  $scope.financialCorrectionStatus = $scope.financialCorrectionInfo.status;

  $scope.save = function() {
    return $scope.editFinancialCorrectionVersionForm.$validate().then(function() {
      if ($scope.editFinancialCorrectionVersionForm.$valid) {
        return FinancialCorrectionVersion.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.financialCorrectionVersion
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
      confirmMsg = 'financialCorrections_editFinancialCorrectionVersion_actualReason';
      validationAction = 'canChangeStatusToActual';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'FinancialCorrectionVersion',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.financialCorrectionVersion.version
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
      resource: 'FinancialCorrectionVersion',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.financialCorrectionVersion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.financialCorrections.view.versions.search');
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

FinancialCorrectionVersionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'FinancialCorrectionVersion',
  'financialCorrectionVersion'
];

FinancialCorrectionVersionsEditCtrl.$resolve = {
  financialCorrectionVersion: [
    'FinancialCorrectionVersion',
    '$stateParams',
    function(FinancialCorrectionVersion, $stateParams) {
      return FinancialCorrectionVersion.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { FinancialCorrectionVersionsEditCtrl };
