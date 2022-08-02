function ProcedureDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureDocument,
  document,
  scConfirm
) {
  $scope.document = document;
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureDocumentForm.$validate().then(function() {
      if ($scope.editProcedureDocumentForm.$valid) {
        return ProcedureDocument.update(
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
      resource: 'ProcedureDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.allDocs.search');
      }
    });
  };
}

ProcedureDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureDocument',
  'document',
  'scConfirm'
];

ProcedureDocumentsEditCtrl.$resolve = {
  document: [
    'ProcedureDocument',
    '$stateParams',
    function(ProcedureDocument, $stateParams) {
      return ProcedureDocument.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureDocumentsEditCtrl };
