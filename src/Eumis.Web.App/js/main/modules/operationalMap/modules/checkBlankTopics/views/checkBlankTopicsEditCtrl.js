function CheckBlankTopicsEditCtrl($scope, $state, $stateParams, scConfirm, CheckBlankTopic, topic) {
  $scope.editMode = null;
  $scope.topic = topic;

  $scope.save = function() {
    return $scope.editTopicForm.$validate().then(function() {
      if ($scope.editTopicForm.$valid) {
        return CheckBlankTopic.update({ id: $stateParams.id }, $scope.topic).$promise.then(
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

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'CheckBlankTopic',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.topic.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.checkBlankTopics.search');
      }
    });
  };
}

CheckBlankTopicsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'CheckBlankTopic',
  'topic'
];

CheckBlankTopicsEditCtrl.$resolve = {
  topic: [
    'CheckBlankTopic',
    '$stateParams',
    function(CheckBlankTopic, $stateParams) {
      return CheckBlankTopic.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { CheckBlankTopicsEditCtrl };
