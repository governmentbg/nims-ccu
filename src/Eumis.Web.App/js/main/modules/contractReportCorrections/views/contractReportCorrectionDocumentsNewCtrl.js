function ContractReportCorrectionDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReportCorrectionDocument,
  newDocument
) {
  $scope.newDocument = newDocument;

  $scope.save = function() {
    return $scope.newDocForm.$validate().then(function() {
      if ($scope.newDocForm.$valid) {
        return ContractReportCorrectionDocument.save(
          {
            id: $stateParams.id
          },
          $scope.newDocument
        ).$promise.then(function() {
          return $state.go('root.contractReportCorrections.view.docs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportCorrections.view.docs.search');
  };
}

ContractReportCorrectionDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReportCorrectionDocument',
  'newDocument'
];

ContractReportCorrectionDocumentsNewCtrl.$resolve = {
  newDocument: [
    '$stateParams',
    'ContractReportCorrectionDocument',
    function($stateParams, ContractReportCorrectionDocument) {
      return ContractReportCorrectionDocument.newDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCorrectionDocumentsNewCtrl };
