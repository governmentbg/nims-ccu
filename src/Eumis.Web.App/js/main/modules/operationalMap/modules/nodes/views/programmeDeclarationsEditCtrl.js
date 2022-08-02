import * as angular from 'angular';

function ProgrammeDeclarationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProgrammeDeclaration,
  declaration,
  scConfirm,
  relatedProcedures,
  declarationItems
) {
  $scope.declaration = declaration;
  $scope.editMode = null;
  $scope.programmeStatus = $scope.info.status;

  $scope.relatedProcedures = relatedProcedures;
  $scope.declarationItems = declarationItems;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammeDeclarationForm.$validate().then(function() {
      if ($scope.editProgrammeDeclarationForm.$valid) {
        return ProgrammeDeclaration.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.declaration
        ).$promise.then(function() {
          return $state.partialReload();
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
      resource: 'ProgrammeDeclaration',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.declaration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.programmes.view.documents.search');
      }
    });
  };
  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProgrammeDeclaration',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.declaration.version
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
      resource: 'ProgrammeDeclaration',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.declaration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.loadItems = function() {
    const modalInstance = scModal.open('loadProgrammeDeclarationItemsModal', {
      programmeId: $stateParams.id,
      programmeDeclarationId: $stateParams.ind
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };
}

ProgrammeDeclarationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProgrammeDeclaration',
  'declaration',
  'scConfirm',
  'relatedProcedures',
  'declarationItems'
];

ProgrammeDeclarationsEditCtrl.$resolve = {
  declaration: [
    'ProgrammeDeclaration',
    '$stateParams',
    function(ProgrammeDeclaration, $stateParams) {
      return ProgrammeDeclaration.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ],
  relatedProcedures: [
    'ProgrammeDeclaration',
    '$stateParams',
    function(ProgrammeDeclaration, $stateParams) {
      return ProgrammeDeclaration.getRelatedProcedures({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ],
  declarationItems: [
    '$stateParams',
    'ProgrammeDeclarationItem',
    function($stateParams, ProgrammeDeclarationItem) {
      return ProgrammeDeclarationItem.query({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProgrammeDeclarationsEditCtrl };
