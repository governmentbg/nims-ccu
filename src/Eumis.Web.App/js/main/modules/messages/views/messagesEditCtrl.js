function MessagesEditCtrl($scope, $state, $stateParams, scConfirm, Message, message) {
  $scope.editMode = null;
  $scope.message = message;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editMessageForm.$validate().then(function() {
      if ($scope.editMessageForm.$valid) {
        return Message.update(
          {
            id: $stateParams.id
          },
          $scope.message
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.send = function() {
    return Message.sendMessage(
      {
        id: $stateParams.id,
        version: $scope.message.version
      },
      {}
    ).$promise.then(function() {
      return $state.go('root.messages.draft', { pd: 1 }, { reload: true });
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Message',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.message.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.messages.draft', { pd: 1 }, { reload: true });
      }
    });
  };
}

MessagesEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'scConfirm', 'Message', 'message'];

MessagesEditCtrl.$resolve = {
  message: [
    'Message',
    '$stateParams',
    function(Message, $stateParams) {
      return Message.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { MessagesEditCtrl };
