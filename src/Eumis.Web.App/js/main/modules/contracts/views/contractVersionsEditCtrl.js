function ContractVersionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ContractVersion,
  scConfirm,
  version
) {
  $scope.versionId = $stateParams.vid;
  $scope.version = version;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.technicalEdit = function() {
    $scope.editMode = 'technicalEdit';
  };

  $scope.save = function() {
    var action = null;

    if ($scope.editMode === 'technicalEdit') {
      action = ContractVersion.technicalEdit;
    } else {
      action = ContractVersion.update;
    }

    return $scope.editVersionForm.$validate().then(function() {
      if ($scope.editVersionForm.$valid) {
        return action(
          {
            id: $stateParams.id,
            vid: $stateParams.vid
          },
          $scope.version
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractVersion',
      action: 'remove',
      params: {
        id: $stateParams.id,
        vid: $stateParams.vid,
        version: $scope.version.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.amendments.search', $stateParams, {
          reload: true
        });
      }
    });
  };

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contracts_versionsEdit_confirmDraft',
      resource: 'ContractVersion',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        vid: $stateParams.vid,
        version: $scope.version.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.check = function() {
    return scConfirm({
      confirmMessage: 'contracts_versionsEdit_confirmCheck',
      resource: 'ContractVersion',
      action: 'markAsChecked',
      params: {
        id: $stateParams.id,
        vid: $stateParams.vid,
        version: $scope.version.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.versionUpdated = function() {
    return $state.partialReload();
  };
}

ContractVersionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractVersion',
  'scConfirm',
  'version'
];

ContractVersionsEditCtrl.$resolve = {
  version: [
    'ContractVersion',
    '$stateParams',
    function(ContractVersion, $stateParams) {
      return ContractVersion.get({
        id: $stateParams.id,
        vid: $stateParams.vid
      }).$promise;
    }
  ]
};

export { ContractVersionsEditCtrl };
