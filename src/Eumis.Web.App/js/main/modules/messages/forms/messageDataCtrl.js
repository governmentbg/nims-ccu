function MessageDataCtrl($scope, Message) {
  $scope.addFile = function() {
    return Message.newFile().$promise.then(function(file) {
      $scope.model.files.push(file);
    });
  };

  $scope.removeFile = function(file, fileInd) {
    if (file.status === 'added') {
      $scope.model.files.splice(fileInd, 1);
    } else {
      file.status = 'removed';
    }
  };
}

MessageDataCtrl.$inject = ['$scope', 'Message'];

export { MessageDataCtrl };
