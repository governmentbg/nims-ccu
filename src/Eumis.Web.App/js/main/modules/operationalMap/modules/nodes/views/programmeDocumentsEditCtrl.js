function ProgrammeDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammeDocument,
  document,
  scConfirm
) {
  $scope.document = document;
  $scope.editMode = null;
  $scope.programmeStatus = $scope.info.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammeDocumentForm.$validate().then(function() {
      if ($scope.editProgrammeDocumentForm.$valid) {
        return ProgrammeDocument.update(
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
      resource: 'ProgrammeDocument',
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
}

ProgrammeDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammeDocument',
  'document',
  'scConfirm'
];

ProgrammeDocumentsEditCtrl.$resolve = {
  document: [
    'ProgrammeDocument',
    '$stateParams',
    function(ProgrammeDocument, $stateParams) {
      return ProgrammeDocument.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProgrammeDocumentsEditCtrl };
