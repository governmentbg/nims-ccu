function ProcedureMassCommunicationDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureMassCommunicationDocument,
  procedureMassCommunicationDocument
) {
  $scope.communicationId = $stateParams.id;
  $scope.procedureMassCommunicationDocument = procedureMassCommunicationDocument;
  $scope.editMode = undefined;
  $scope.status = $scope.procedureMassCommunicationInfo.status;

  $scope.edit = function() {
    $scope.editMode = !$scope.editMode;
  };

  $scope.save = function() {
    return $scope.editProcedureMassCommunicationDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureMassCommunicationDocumentsForm.$valid) {
        return ProcedureMassCommunicationDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procedureMassCommunicationDocument
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.deleteDocument = function() {
    return scConfirm({
      confirmMessage:
        'procedureMassCommunications_procedureMassCommunicationDocumentsEdit_sendMessage',
      resource: 'ProcedureMassCommunicationDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureMassCommunicationDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedureMassCommunications.view.documents.search');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

ProcedureMassCommunicationDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureMassCommunicationDocument',
  'procedureMassCommunicationDocument'
];

ProcedureMassCommunicationDocumentsEditCtrl.$resolve = {
  procedureMassCommunicationDocument: [
    'ProcedureMassCommunicationDocument',
    '$stateParams',
    function(ProcedureMassCommunicationDocument, $stateParams) {
      return ProcedureMassCommunicationDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcedureMassCommunicationDocumentsEditCtrl };
