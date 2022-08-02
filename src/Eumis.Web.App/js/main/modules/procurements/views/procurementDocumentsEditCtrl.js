function ProcurementDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcurementDocument,
  procurementDocument
) {
  $scope.editMode = null;
  $scope.procurementId = $stateParams.id;
  $scope.procurementDocument = procurementDocument;
  $scope.status = $scope.info.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcurementDocumentForm.$validate().then(function() {
      if ($scope.editProcurementDocumentForm.$valid) {
        return ProcurementDocument.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.procurementDocument
        ).$promise.then(function() {
          $state.partialReload();
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
      resource: 'ProcurementDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procurementDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procurements.view.documents.search');
      }
    });
  };
}

ProcurementDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcurementDocument',
  'procurementDocument'
];

ProcurementDocumentsEditCtrl.$resolve = {
  procurementDocument: [
    'ProcurementDocument',
    '$stateParams',
    function(ProcurementDocument, $stateParams) {
      return ProcurementDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcurementDocumentsEditCtrl };
