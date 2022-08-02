function ContractReportCertCorrectionDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReportCertCorrectionDocument,
  newDocument
) {
  $scope.newDocument = newDocument;

  $scope.save = function() {
    return $scope.newDocForm.$validate().then(function() {
      if ($scope.newDocForm.$valid) {
        return ContractReportCertCorrectionDocument.save(
          {
            id: $stateParams.id
          },
          $scope.newDocument
        ).$promise.then(function() {
          return $state.go('root.contractReportCertCorrections.view.docs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportCertCorrections.view.docs.search');
  };
}

ContractReportCertCorrectionDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReportCertCorrectionDocument',
  'newDocument'
];

ContractReportCertCorrectionDocumentsNewCtrl.$resolve = {
  newDocument: [
    '$stateParams',
    'ContractReportCertCorrectionDocument',
    function($stateParams, ContractReportCertCorrectionDocument) {
      return ContractReportCertCorrectionDocument.newDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCertCorrectionDocumentsNewCtrl };
