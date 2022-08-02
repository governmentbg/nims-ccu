function MessagesDraftCtrl($scope, $state, $stateParams, messagesQuery) {
  $scope.messagesQuery = messagesQuery;
  $scope.page = $stateParams.pd;

  $scope.view = function(id) {
    return $state.go('root.messages.draft.edit', { id: id });
  };

  $scope.pageChange = function(page) {
    return $state.go('root.messages.draft', {
      pd: page
    });
  };
}

MessagesDraftCtrl.$inject = ['$scope', '$state', '$stateParams', 'messagesQuery'];

MessagesDraftCtrl.$resolve = {
  messagesQuery: [
    'Message',
    'pager',
    '$stateParams',
    function(Message, pager, $stateParams) {
      var params = pager.getOffsetAndLimit($stateParams.pd);

      return Message.getDraftMessages(params).$promise;
    }
  ]
};

export { MessagesDraftCtrl };
