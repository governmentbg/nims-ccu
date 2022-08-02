import _ from 'lodash';

function ProcedureMassCommunicationDocumentsSearchCtrl(
  $scope,
  $stateParams,
  $state,
  scConfirm,
  procedureMassCommunicationDocuments,
  ProcedureMassCommunicationFile
) {
  $scope.communicationId = $stateParams.id;
  $scope.massCommunicationVersion = $scope.procedureMassCommunicationInfo.version;
  $scope.status = $scope.procedureMassCommunicationInfo.status;

  $scope.documents = _.map(procedureMassCommunicationDocuments, function(item) {
    if (item.file) {
      item.file.url = ProcedureMassCommunicationFile.getUrl({
        id: item.procedureMassCommunicationId,
        fileKey: item.file.key
      });
    }
    return item;
  });

  $scope.delItem = function(documentId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureMassCommunicationDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: documentId,
        version: $scope.massCommunicationVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProcedureMassCommunicationDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$state',
  'scConfirm',
  'procedureMassCommunicationDocuments',
  'ProcedureMassCommunicationFile'
];

ProcedureMassCommunicationDocumentsSearchCtrl.$resolve = {
  procedureMassCommunicationDocuments: [
    '$stateParams',
    'ProcedureMassCommunicationDocument',
    function($stateParams, ProcedureMassCommunicationDocument) {
      return ProcedureMassCommunicationDocument.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureMassCommunicationDocumentsSearchCtrl };
