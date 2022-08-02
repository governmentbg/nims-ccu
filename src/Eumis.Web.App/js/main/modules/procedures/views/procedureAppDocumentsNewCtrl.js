function ProcedureAppDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureAppDocument,
  appDocument
) {
  $scope.procedureId = $stateParams.id;
  $scope.appDocument = appDocument;

  $scope.save = function() {
    return $scope.newProcedureAppDocumentsForm.$validate().then(function() {
      if ($scope.newProcedureAppDocumentsForm.$valid) {
        return ProcedureAppDocument.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.appDocument
        ).$promise.then(function() {
          return $state.go('root.procedures.view.allDocs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.allDocs.search');
  };
}

ProcedureAppDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureAppDocument',
  'appDocument'
];

ProcedureAppDocumentsNewCtrl.$resolve = {
  appDocument: [
    'ProcedureAppDocument',
    '$stateParams',
    function(ProcedureAppDocument, $stateParams) {
      return ProcedureAppDocument.newAppDocument({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureAppDocumentsNewCtrl };
