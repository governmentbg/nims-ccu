function MessagesNewCtrl($scope, $state, Message, message) {
  $scope.message = message;

  $scope.save = function() {
    return $scope.newMessageForm.$validate().then(function() {
      if ($scope.newMessageForm.$valid) {
        return Message.save($scope.message).$promise.then(function(result) {
          return $state.go(
            'root.messages.draft.edit',
            {
              id: result.messageId
            },
            {
              reload: true
            }
          );
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go($state.previous);
  };
}

MessagesNewCtrl.$inject = ['$scope', '$state', 'Message', 'message'];

MessagesNewCtrl.$resolve = {
  message: [
    'Message',
    function(Message) {
      return Message.newMessage().$promise;
    }
  ]
};

export { MessagesNewCtrl };
