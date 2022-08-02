function ProgrammeDeclarationItemsEditCtrl(
  $scope,
  $state,
  $stateParams,
  declarationItem,
  scConfirm
) {
  $scope.declarationItem = declarationItem;
  $scope.editMode = null;
  $scope.programmeStatus = $scope.info.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammeDeclarationItemForm.$validate().then(function() {
      if ($scope.editProgrammeDeclarationItemForm.$valid) {
        return scConfirm({
          resource: 'ProgrammeDeclarationItem',
          validationAction: 'canUpdate',
          action: 'update',
          params: {
            id: $stateParams.id,
            ind: $stateParams.ind,
            did: $stateParams.did,
            version: $scope.declarationItem.version
          },
          data: $scope.declarationItem
        }).then(function(result) {
          if (result.executed) {
            return $state.partialReload();
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProgrammeDeclarationItem',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        did: $stateParams.did,
        version: $scope.declarationItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.programmes.view.declarations.edit');
      }
    });
  };
  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProgrammeDeclarationItem',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        did: $stateParams.did,
        version: $scope.declarationItem.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ProgrammeDeclarationItem',
      action: 'activate',
      validationAction: 'canActivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        did: $stateParams.did,
        version: $scope.declarationItem.version,
        orderNum: $scope.declarationItem.orderNum
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProgrammeDeclarationItemsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'declarationItem',
  'scConfirm'
];

ProgrammeDeclarationItemsEditCtrl.$resolve = {
  declarationItem: [
    'ProgrammeDeclarationItem',
    '$stateParams',
    function(ProgrammeDeclarationItem, $stateParams) {
      return ProgrammeDeclarationItem.get({
        id: $stateParams.id,
        ind: $stateParams.ind,
        did: $stateParams.did
      }).$promise;
    }
  ]
};

export { ProgrammeDeclarationItemsEditCtrl };
