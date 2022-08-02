function CheckBlankTopicsNewCtrl($scope, $state, CheckBlankTopic, newTopic) {
  $scope.newTopic = newTopic;

  $scope.save = function() {
    return $scope.newCheckBlankTopicForm.$validate().then(function() {
      if ($scope.newCheckBlankTopicForm.$valid) {
        return CheckBlankTopic.save($scope.newTopic).$promise.then(function() {
          return $state.go('root.map.checkBlankTopics.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.checkBlankTopics.search');
  };
}

CheckBlankTopicsNewCtrl.$inject = ['$scope', '$state', 'CheckBlankTopic', 'newTopic'];

CheckBlankTopicsNewCtrl.$resolve = {
  newTopic: [
    'CheckBlankTopic',
    function(CheckBlankTopic) {
      return CheckBlankTopic.newCheckBlankTopic().$promise;
    }
  ]
};

export { CheckBlankTopicsNewCtrl };
