function ProcedureAppDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureAppDocument,
  appDocument,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.appDocument = appDocument;
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureAppDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.appDocument.version
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
      resource: 'ProcedureAppDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.appDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureAppDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureAppDocumentsForm.$valid) {
        return ProcedureAppDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.appDocument
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteAppDocument = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureAppDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.appDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.allDocs.search');
      }
    });
  };
}

ProcedureAppDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureAppDocument',
  'appDocument',
  'scConfirm'
];

ProcedureAppDocumentsEditCtrl.$resolve = {
  appDocument: [
    'ProcedureAppDocument',
    '$stateParams',
    function(ProcedureAppDocument, $stateParams) {
      return ProcedureAppDocument.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureAppDocumentsEditCtrl };
