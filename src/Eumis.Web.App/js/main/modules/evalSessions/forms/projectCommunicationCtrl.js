function ProjectCommunicationCtrl($scope) {
  $scope.isvalidEndingDate = function(questionEndingDate) {
    if (!questionEndingDate) {
      return true;
    } else if (
      new Date(questionEndingDate).setHours(0, 0, 0, 0) <
      new Date($scope.model.questionDate).setHours(0, 0, 0, 0)
    ) {
      return false;
    } else {
      return true;
    }
  };
}

ProjectCommunicationCtrl.$inject = ['$scope', 'scFormParams'];

export { ProjectCommunicationCtrl };
