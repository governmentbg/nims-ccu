function ContractsNewStep2Ctrl($scope, $state, scConfirm, project) {
  $scope.procedureId = project.procedureId;
  $scope.project = project;

  $scope.save = function() {
    return $scope.contractsNewStep2Form.$validate().then(function() {
      if ($scope.contractsNewStep2Form.$valid) {
        return scConfirm({
          resource: 'Contract',
          validationAction: 'canCreate',
          action: 'save',
          params: {
            projectId: project.projectId,
            programmeId: $scope.programmeId,
            registrationType: $scope.registrationType,
            attachedContractId: $scope.attachedContractId
          }
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.contracts.editNew', {
              id: result.result.contractId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.search');
  };
}

ContractsNewStep2Ctrl.$inject = ['$scope', '$state', 'scConfirm', 'project'];

ContractsNewStep2Ctrl.$resolve = {
  project: [
    'Project',
    '$stateParams',
    function(Project, $stateParams) {
      return Project.getContractProjectByNumber({
        projectNum: $stateParams.pNum
      }).$promise;
    }
  ]
};

export { ContractsNewStep2Ctrl };
