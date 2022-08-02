function ProjectRegistrationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  Project,
  projectRegistration
) {
  $scope.projectRegistration = projectRegistration;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProjectRegistrationDataForm.$validate().then(function() {
      if ($scope.editProjectRegistrationDataForm.$valid) {
        return Project.update({ id: $stateParams.id }, $scope.projectRegistration).$promise.then(
          function() {
            return $state.partialReload();
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.withdraw = function() {
    return scConfirm({
      confirmMessage: 'projects_projectRegistationEdit_confirmWithdraw',
      resource: 'Project',
      validationAction: 'canWithdraw',
      action: 'withdraw',
      params: {
        id: $stateParams.id,
        version: $scope.projectRegistration.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProjectRegistrationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'Project',
  'projectRegistration'
];

ProjectRegistrationsEditCtrl.$resolve = {
  projectRegistration: [
    'Project',
    '$stateParams',
    function(Project, $stateParams) {
      return Project.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProjectRegistrationsEditCtrl };
