function CheckBlankTopicsSearchCtrl($scope, checkBlankTopics) {
  $scope.checkBlankTopics = checkBlankTopics;
}

CheckBlankTopicsSearchCtrl.$inject = ['$scope', 'checkBlankTopics'];

CheckBlankTopicsSearchCtrl.$resolve = {
  checkBlankTopics: [
    'CheckBlankTopic',
    function(CheckBlankTopic) {
      return CheckBlankTopic.query().$promise;
    }
  ]
};

export { CheckBlankTopicsSearchCtrl };
