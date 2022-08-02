function MessagesInboxCtrl($scope, $state, $stateParams, Message, messagesQuery) {
  $scope.messagesQuery = messagesQuery;
  $scope.page = $stateParams.pi;

  $scope.view = function(message) {
    if (!message.recieveDate) {
      return Message.markAsRecieved(
        {
          id: message.messageId
        },
        {}
      ).$promise.then(function() {
        return $state.go(
          'root.messages.inbox.view',
          {
            id: message.messageId
          },
          {
            reload: true
          }
        );
      });
    } else {
      return $state.go('root.messages.inbox.view', { id: message.messageId });
    }
  };

  $scope.pageChange = function(page) {
    return $state.go('root.messages.inbox', {
      pi: page
    });
  };
}

MessagesInboxCtrl.$inject = ['$scope', '$state', '$stateParams', 'Message', 'messagesQuery'];

MessagesInboxCtrl.$resolve = {
  messagesQuery: [
    'Message',
    'pager',
    '$stateParams',
    function(Message, pager, $stateParams) {
      var params = pager.getOffsetAndLimit($stateParams.pi);

      return Message.getInboxMessages(params).$promise;
    }
  ]
};

export { MessagesInboxCtrl };
