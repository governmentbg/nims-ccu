function ProgrammeApplicationDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammeApplicationDocument,
  document,
  scConfirm,
  relatedProcedures
) {
  $scope.document = document;
  $scope.editMode = null;
  $scope.programmeStatus = $scope.info.status;
  $scope.relatedProcedures = relatedProcedures;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammeApplicationDocumentForm.$validate().then(function() {
      if ($scope.editProgrammeApplicationDocumentForm.$valid) {
        return ProgrammeApplicationDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.document
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteDocument = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProgrammeApplicationDocument',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
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
      resource: 'ProgrammeApplicationDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
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
      resource: 'ProgrammeApplicationDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProgrammeApplicationDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammeApplicationDocument',
  'document',
  'scConfirm',
  'relatedProcedures'
];

ProgrammeApplicationDocumentsEditCtrl.$resolve = {
  document: [
    'ProgrammeApplicationDocument',
    '$stateParams',
    function(ProgrammeApplicationDocument, $stateParams) {
      return ProgrammeApplicationDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise;
    }
  ],
  relatedProcedures: [
    'ProgrammeApplicationDocument',
    '$stateParams',
    function(ProgrammeApplicationDocument, $stateParams) {
      return ProgrammeApplicationDocument.getRelatedProcedures({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProgrammeApplicationDocumentsEditCtrl };
