import angular from 'angular';

function ProjectDossierCtrl($q, $scope, $state, scModal, scConfirm, Project) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.projectDossierForm.$validate().then(function() {
      if ($scope.projectDossierForm.$valid) {
        return scConfirm({
          resource: 'Project',
          validationAction: 'isProjectValidForProjectDossier',
          action: 'getProjectDossierProjectByNumber',
          params: {
            projectNum: $scope.model.projectNumber
          }
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.projectDossier.view.project', {
              id: result.result.projectId,
              contractId: $scope.model.contractId
            });
          }
        });
      }
    });
  };

  $scope.chooseProject = function() {
    var modalInstance = scModal.open('chooseProjectDossierProjectModal', {
      procedureId: $scope.model.procedureId,
      projectNumber: $scope.model.projectNumber
    });

    modalInstance.result.then(function(project) {
      $scope.model.procedureId = project.procedureId;
      $scope.model.projectNumber = project.regNumber;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.isValidProjectNum = function(projectNumber) {
    if (!projectNumber) {
      return $q.resolve();
    }

    return Project.isRegNumExisting({
      procedureId: $scope.model.procedureId,
      projectNum: projectNumber
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };

  $scope.contractChanged = function() {
    if ($scope.projectInfo && !$scope.model.projectNumber) {
      $scope.model.projectNumber = $scope.projectInfo.projectNum;
    }
  };
}

ProjectDossierCtrl.$inject = ['$q', '$scope', '$state', 'scModal', 'scConfirm', 'Project'];

export { ProjectDossierCtrl };
