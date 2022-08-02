function ContractReportRevalidationDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ContractReportRevalidationDocument,
  newDocument
) {
  $scope.newDocument = newDocument;

  $scope.save = function() {
    return $scope.newDocForm.$validate().then(function() {
      if ($scope.newDocForm.$valid) {
        return ContractReportRevalidationDocument.save(
          {
            id: $stateParams.id
          },
          $scope.newDocument
        ).$promise.then(function() {
          return $state.go('root.contractReportRevalidations.view.docs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportRevalidations.view.docs.search');
  };
}

ContractReportRevalidationDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ContractReportRevalidationDocument',
  'newDocument'
];

ContractReportRevalidationDocumentsNewCtrl.$resolve = {
  newDocument: [
    '$stateParams',
    'ContractReportRevalidationDocument',
    function($stateParams, ContractReportRevalidationDocument) {
      return ContractReportRevalidationDocument.newDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportRevalidationDocumentsNewCtrl };
