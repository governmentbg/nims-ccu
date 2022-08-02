function MessagesSentCtrl($scope, $state, $stateParams, messagesQuery) {
  $scope.messagesQuery = messagesQuery;
  $scope.page = $stateParams.ps;

  $scope.view = function(id) {
    return $state.go('root.messages.sent.view', { id: id });
  };

  $scope.pageChange = function(page) {
    return $state.go('root.messages.sent', {
      ps: page
    });
  };
}

MessagesSentCtrl.$inject = ['$scope', '$state', '$stateParams', 'messagesQuery'];

MessagesSentCtrl.$resolve = {
  messagesQuery: [
    'Message',
    'pager',
    '$stateParams',
    function(Message, pager, $stateParams) {
      var params = pager.getOffsetAndLimit($stateParams.ps);

      return Message.getSentMessages(params).$promise;
    }
  ]
};

export { MessagesSentCtrl };
