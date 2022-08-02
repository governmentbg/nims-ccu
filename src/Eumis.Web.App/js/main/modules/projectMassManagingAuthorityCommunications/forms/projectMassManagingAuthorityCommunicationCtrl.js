function ProjectMassManagingAuthorityCommunicationDataCtrl($scope) {
  $scope.isValidEndingDate = function(questionEndingDate) {
    if (questionEndingDate) {
      if (new Date(questionEndingDate).setHours(0, 0, 0, 0) < new Date().setHours(0, 0, 0, 0)) {
        return false;
      } else {
        return true;
      }
    } else {
      return true;
    }
  };
}

ProjectMassManagingAuthorityCommunicationDataCtrl.$inject = ['$scope'];

export { ProjectMassManagingAuthorityCommunicationDataCtrl };
