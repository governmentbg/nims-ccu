function PPriorityDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProgrammePriorityDocument,
  document,
  scConfirm
) {
  $scope.document = document;
  $scope.editMode = null;
  $scope.programmePriorityStatus = $scope.info.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammePriorityDocumentForm.$validate().then(function() {
      if ($scope.editProgrammePriorityDocumentForm.$valid) {
        return ProgrammePriorityDocument.update(
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
      resource: 'ProgrammePriorityDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.ppriorities.view.documents.search');
      }
    });
  };
}

PPriorityDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammePriorityDocument',
  'document',
  'scConfirm'
];

PPriorityDocumentsEditCtrl.$resolve = {
  document: [
    'ProgrammePriorityDocument',
    '$stateParams',
    function(ProgrammePriorityDocument, $stateParams) {
      return ProgrammePriorityDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { PPriorityDocumentsEditCtrl };
