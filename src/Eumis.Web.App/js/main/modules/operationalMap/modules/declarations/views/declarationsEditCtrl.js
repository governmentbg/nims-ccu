import angular from 'angular';

function DeclarationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  Declaration,
  declaration
) {
  $scope.editMode = null;
  $scope.declaration = declaration;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editDeclarationForm.$validate().then(function() {
      if ($scope.editDeclarationForm.$valid) {
        return Declaration.update(
          {
            id: $stateParams.id
          },
          $scope.declaration
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.publish = function() {
    var modalInstance = scModal.open('publishDeclarationModal', {
      declarationId: $scope.declaration.declarationId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.draft = function() {
    return scConfirm({
      confirmMessage: 'declarations_editForm_draftConfirm',
      resource: 'Declaration',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.declaration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.archive = function() {
    return scConfirm({
      confirmMessage: 'declarations_editForm_archiveConfirm',
      resource: 'Declaration',
      action: 'archive',
      params: {
        id: $stateParams.id,
        version: $scope.declaration.version
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
      resource: 'Declaration',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.declaration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.declarations.search', $stateParams, {
          reload: true
        });
      }
    });
  };
}

DeclarationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'Declaration',
  'declaration'
];

DeclarationsEditCtrl.$resolve = {
  declaration: [
    'Declaration',
    '$stateParams',
    function(Declaration, $stateParams) {
      return Declaration.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { DeclarationsEditCtrl };
