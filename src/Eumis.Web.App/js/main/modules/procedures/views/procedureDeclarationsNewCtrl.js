function ProcedureDeclarationsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureDeclaration,
  procedureDeclaration
) {
  $scope.procedureId = $stateParams.id;
  $scope.procedureDeclaration = procedureDeclaration;

  $scope.save = function() {
    return $scope.newProcedureDeclarationForm.$validate().then(function() {
      if ($scope.newProcedureDeclarationForm.$valid) {
        return ProcedureDeclaration.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procedureDeclaration
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

ProcedureDeclarationsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureDeclaration',
  'procedureDeclaration'
];

ProcedureDeclarationsNewCtrl.$resolve = {
  procedureDeclaration: [
    'ProcedureDeclaration',
    '$stateParams',
    function(ProcedureDeclaration, $stateParams) {
      return ProcedureDeclaration.newProcedureDeclaration({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureDeclarationsNewCtrl };
