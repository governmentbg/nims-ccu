import angular from 'angular';

function ContractsNewStep1Ctrl($q, $scope, $state, scModal, Project) {
  $scope.model = {};

  $scope.next = function() {
    return $scope.contractsNewStep1Form.$validate().then(function() {
      if ($scope.contractsNewStep1Form.$valid) {
        return $state.go('root.contracts.newStep2', {
          pNum: $scope.model.projectNumber
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.search');
  };

  $scope.chooseProject = function() {
    var modalInstance = scModal.open('chooseContractProjectModal', {
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
}

ContractsNewStep1Ctrl.$inject = ['$q', '$scope', '$state', 'scModal', 'Project'];

export { ContractsNewStep1Ctrl };
