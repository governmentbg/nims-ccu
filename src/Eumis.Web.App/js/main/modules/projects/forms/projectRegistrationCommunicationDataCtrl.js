function ProjectRegistrationCommunicationDataCtrl($scope) {
  $scope.isValidEndingDate = function(questionEndingDate) {
    if (!questionEndingDate) {
      return true;
    } else if (
      new Date(questionEndingDate).setHours(0, 0, 0, 0) < new Date(Date.now()).setHours(0, 0, 0, 0)
    ) {
      return false;
    } else {
      return true;
    }
  };
}

ProjectRegistrationCommunicationDataCtrl.$inject = ['$scope'];

export { ProjectRegistrationCommunicationDataCtrl };
