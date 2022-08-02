function MessagesArchiveCtrl($scope, $state, $stateParams, messagesQuery) {
  $scope.messagesQuery = messagesQuery;
  $scope.page = $stateParams.pa;

  $scope.view = function(id) {
    return $state.go('root.messages.archive.view', { id: id });
  };

  $scope.pageChange = function(page) {
    return $state.go('root.messages.archive', {
      pa: page
    });
  };
}

MessagesArchiveCtrl.$inject = ['$scope', '$state', '$stateParams', 'messagesQuery'];

MessagesArchiveCtrl.$resolve = {
  messagesQuery: [
    'Message',
    'pager',
    '$stateParams',
    function(Message, pager, $stateParams) {
      var params = pager.getOffsetAndLimit($stateParams.pa);

      return Message.getArchivedMessages(params).$promise;
    }
  ]
};

export { MessagesArchiveCtrl };
