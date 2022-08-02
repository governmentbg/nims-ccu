function DeclarationDataCtrl($scope, Declaration) {
  $scope.addFile = function() {
    return Declaration.newFile().$promise.then(function(file) {
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

DeclarationDataCtrl.$inject = ['$scope', 'Declaration'];

export { DeclarationDataCtrl };
