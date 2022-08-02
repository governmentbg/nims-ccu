import _ from 'lodash';

function ContractReportCorrectionDocumentsSearchCtrl(
  $scope,
  $stateParams,
  structuredDocument,
  documents
) {
  $scope.documents = documents;
  $scope.contractReportCorrectionId = $stateParams.id;
  $scope.status = $scope.contractReportCorrectionInfo.status;
}

ContractReportCorrectionDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'structuredDocument',
  'documents'
];

ContractReportCorrectionDocumentsSearchCtrl.$resolve = {
  documents: [
    'ContractReportCorrectionDocument',
    'ContractReportCorrectionFile',
    '$stateParams',
    function(ContractReportCorrectionDocument, ContractReportCorrectionFile, $stateParams) {
      return ContractReportCorrectionDocument.query({
        id: $stateParams.id
      }).$promise.then(function(docs) {
        return _.map(docs, function(doc) {
          if (doc.file) {
            doc.file.url = ContractReportCorrectionFile.getUrl({
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

export { ContractReportCorrectionDocumentsSearchCtrl };
