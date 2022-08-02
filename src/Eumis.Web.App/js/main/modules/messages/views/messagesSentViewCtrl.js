import _ from 'lodash';

function MessagesSentViewCtrl($scope, $sce, message) {
  $scope.message = message;
  $scope.content = $sce.trustAsHtml(message.content);
}

MessagesSentViewCtrl.$inject = ['$scope', '$sce', 'message'];

MessagesSentViewCtrl.$resolve = {
  message: [
    'Message',
    'MessageFile',
    '$stateParams',
    function(Message, MessageFile, $stateParams) {
      return Message.getSentMessage({
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

export { MessagesSentViewCtrl };
