function MessagesViewCtrl($scope, $state, count) {
  $scope.count = count;

  $scope.isActive = function(item) {
    return $state.includes('root.messages.' + item);
  };
}

MessagesViewCtrl.$inject = ['$scope', '$state', 'count'];

MessagesViewCtrl.$resolve = {
  count: [
    'Message',
    function(Message) {
      return Message.getCount().$promise;
    }
  ]
};

export { MessagesViewCtrl };
