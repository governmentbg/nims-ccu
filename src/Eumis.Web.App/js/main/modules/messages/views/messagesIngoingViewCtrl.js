import _ from 'lodash';

function MessagesIngoingViewCtrl($scope, $sce, $state, $stateParams, scConfirm, message) {
  $scope.message = message;
  $scope.content = $sce.trustAsHtml(message.content);

  $scope.archive = function() {
    return scConfirm({
      confirmMessage: 'messages_viewIngoing_confirmArchive',
      resource: 'Message',
      action: 'archive',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.messages.inbox', { pi: 1 }, { reload: true });
      }
    });
  };
}

MessagesIngoingViewCtrl.$inject = [
  '$scope',
  '$sce',
  '$state',
  '$stateParams',
  'scConfirm',
  'message'
];

MessagesIngoingViewCtrl.$resolve = {
  message: [
    'Message',
    'MessageFile',
    '$stateParams',
    function(Message, MessageFile, $stateParams) {
      return Message.getIngoingMessage({
        id: $stateParams.id
      }).$promise.then(function(result) {
        _.forEach(result.files, function(item) {
          item.url = MessageFile.getUrl({
            id: result.messageId,
            fileKey: item.key
          });
        });

        return result;
      });
    }
  ]
};

export { MessagesIngoingViewCtrl };
