function ProcedureAppGuidelinesNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureAppGuideline,
  appGuideline
) {
  $scope.procedureId = $stateParams.id;
  $scope.appGuideline = appGuideline;

  $scope.save = function() {
    return $scope.newProcedureAppGuidelinesForm.$validate().then(function() {
      if ($scope.newProcedureAppGuidelinesForm.$valid) {
        return ProcedureAppGuideline.save(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.appGuideline
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

ProcedureAppGuidelinesNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureAppGuideline',
  'appGuideline'
];

ProcedureAppGuidelinesNewCtrl.$resolve = {
  appGuideline: [
    'ProcedureAppGuideline',
    '$stateParams',
    function(ProcedureAppGuideline, $stateParams) {
      return ProcedureAppGuideline.newAppGuideline({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureAppGuidelinesNewCtrl };
