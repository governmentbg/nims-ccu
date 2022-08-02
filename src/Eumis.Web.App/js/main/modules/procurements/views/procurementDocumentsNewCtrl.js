function ProcurementDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcurementDocument,
  procurementDocument
) {
  $scope.procurementDocument = procurementDocument;

  $scope.save = function() {
    return $scope.newProcurementDocumentForm.$validate().then(function() {
      if ($scope.newProcurementDocumentForm.$valid) {
        return ProcurementDocument.save(
          {
            id: $stateParams.id
          },
          $scope.procurementDocument
        ).$promise.then(function() {
          return $state.go('root.procurements.view.documents.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procurements.view.documents.search');
  };
}

ProcurementDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcurementDocument',
  'procurementDocument'
];

ProcurementDocumentsNewCtrl.$resolve = {
  procurementDocument: [
    '$stateParams',
    'ProcurementDocument',
    function($stateParams, ProcurementDocument) {
      return ProcurementDocument.newDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcurementDocumentsNewCtrl };
