function ProcedureMassCommunicationDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureMassCommunicationDocument,
  procedureMassCommunicationDocument
) {
  $scope.communicationId = $stateParams.id;
  $scope.procedureMassCommunicationDocument = procedureMassCommunicationDocument;

  $scope.save = function() {
    return $scope.newProcedureMassCommunicationDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureMassCommunicationDocumentsForm.$valid) {
        return ProcedureMassCommunicationDocument.save(
          { id: $stateParams.id },
          $scope.procedureMassCommunicationDocument
        ).$promise.then(function() {
          return $state.go('root.procedureMassCommunications.view.documents.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedureMassCommunications.view.documents.search');
  };
}

ProcedureMassCommunicationDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureMassCommunicationDocument',
  'procedureMassCommunicationDocument'
];

ProcedureMassCommunicationDocumentsNewCtrl.$resolve = {
  procedureMassCommunicationDocument: [
    'ProcedureMassCommunicationDocument',
    '$stateParams',
    function(ProcedureMassCommunicationDocuments, $stateParams) {
      return ProcedureMassCommunicationDocuments.newDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureMassCommunicationDocumentsNewCtrl };
