import _ from 'lodash';

function ContractReportRevalidationDocumentsSearchCtrl(
  $scope,
  $stateParams,
  structuredDocument,
  documents
) {
  $scope.documents = documents;
  $scope.contractReportRevalidationId = $stateParams.id;
  $scope.status = $scope.contractReportRevalidationInfo.status;
}

ContractReportRevalidationDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'structuredDocument',
  'documents'
];

ContractReportRevalidationDocumentsSearchCtrl.$resolve = {
  documents: [
    'ContractReportRevalidationDocument',
    'ContractReportRevalidationFile',
    '$stateParams',
    function(ContractReportRevalidationDocument, ContractReportRevalidationFile, $stateParams) {
      return ContractReportRevalidationDocument.query({
        id: $stateParams.id
      }).$promise.then(function(docs) {
        return _.map(docs, function(doc) {
          if (doc.file) {
            doc.file.url = ContractReportRevalidationFile.getUrl({
              id: $stateParams.id,
              fileKey: doc.file.key
            });
          }
          return doc;
        });
      });
    }
  ]
};

export { ContractReportRevalidationDocumentsSearchCtrl };
