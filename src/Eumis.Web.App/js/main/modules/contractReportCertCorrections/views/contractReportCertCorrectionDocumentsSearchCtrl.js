import _ from 'lodash';

function ContractReportCertCorrectionDocumentsSearchCtrl($scope, $stateParams, documents) {
  $scope.documents = documents;
  $scope.contractReportCertCorrectionId = $stateParams.id;
  $scope.status = $scope.contractReportCertCorrectionInfo.status;
}

ContractReportCertCorrectionDocumentsSearchCtrl.$inject = ['$scope', '$stateParams', 'documents'];

ContractReportCertCorrectionDocumentsSearchCtrl.$resolve = {
  documents: [
    'ContractReportCertCorrectionDocument',
    'ContractReportCertCorrectionFile',
    '$stateParams',
    function(ContractReportCertCorrectionDocument, ContractReportCertCorrectionFile, $stateParams) {
      return ContractReportCertCorrectionDocument.query({
        id: $stateParams.id
      }).$promise.then(function(docs) {
        return _.map(docs, function(doc) {
          if (doc.file) {
            doc.file.url = ContractReportCertCorrectionFile.getUrl({
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

export { ContractReportCertCorrectionDocumentsSearchCtrl };
