function ProjectDossierProjectStandingsEditCtrl(
  $scope,
  $state,
  $stateParams,
  evalSessionProjectStanding
) {
  $scope.evalSessionProjectStanding = evalSessionProjectStanding;

  $scope.back = function() {
    return $state.go('root.projectDossier.view.project', {
      id: $stateParams.id
    });
  };
}

ProjectDossierProjectStandingsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'evalSessionProjectStanding'
];

ProjectDossierProjectStandingsEditCtrl.$resolve = {
  evalSessionProjectStanding: [
    '$stateParams',
    'ProjectDossier',
    function($stateParams, ProjectDossier) {
      return ProjectDossier.getProjectEvalSessionStanding({
        id: $stateParams.id,
        ind: $stateParams.sid
      }).$promise;
    }
  ]
};

export { ProjectDossierProjectStandingsEditCtrl };
