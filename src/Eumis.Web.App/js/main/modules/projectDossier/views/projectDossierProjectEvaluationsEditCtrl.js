function ProjectDossierProjectEvaluationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  evalSessionEvaluation
) {
  $scope.evalSessionEvaluation = evalSessionEvaluation;

  $scope.back = function() {
    return $state.go('root.projectDossier.view.project', {
      id: $stateParams.id
    });
  };
}

ProjectDossierProjectEvaluationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'evalSessionEvaluation'
];

ProjectDossierProjectEvaluationsEditCtrl.$resolve = {
  evalSessionEvaluation: [
    '$stateParams',
    'ProjectDossier',
    function($stateParams, ProjectDossier) {
      return ProjectDossier.getProjectEvalSessionEvaluation({
        id: $stateParams.id,
        ind: $stateParams.eid
      }).$promise;
    }
  ]
};

export { ProjectDossierProjectEvaluationsEditCtrl };
