function NewsDataCtrl($scope, scFormParams, News) {
  $scope.isNew = scFormParams.isNew;

  if ($scope.model.type === 'portal') {
    $scope.isPortalNews = true;
  } else {
    $scope.isPortalNews = false;
  }

  $scope.dateToValid = function(dateTo) {
    if (!$scope.model.dateFrom || !dateTo) {
      return true;
    }

    return new Date(dateTo).getTime() >= new Date($scope.model.dateFrom).getTime();
  };

  $scope.addFile = function() {
    return News.newFile().$promise.then(function(file) {
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

  $scope.typeChange = function() {
    if ($scope.model.type === 'portal') {
      $scope.isPortalNews = true;
      $scope.model.emailNotification = false;
    } else {
      $scope.isPortalNews = false;
    }
  };
}

NewsDataCtrl.$inject = ['$scope', 'scFormParams', 'News'];

export { NewsDataCtrl };
