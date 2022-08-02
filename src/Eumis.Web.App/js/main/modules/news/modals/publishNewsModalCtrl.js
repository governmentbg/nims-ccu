function PublishNewsModalCtrl($scope, $uibModalInstance, scModalParams, News, publication) {
  $scope.model = publication;

  $scope.dateToValid = function(dateTo) {
    if (!$scope.model.dateFrom || !dateTo) {
      return true;
    }

    return new Date(dateTo).getTime() >= new Date($scope.model.dateFrom).getTime();
  };

  $scope.publish = function() {
    return $scope.publishNewsForm.$validate().then(function() {
      if ($scope.publishNewsForm.$valid) {
        return News.publish(
          {
            id: scModalParams.newsId
          },
          $scope.model
        ).$promise.then(function() {
          return $uibModalInstance.close();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

PublishNewsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'News',
  'publication'
];

PublishNewsModalCtrl.$resolve = {
  publication: [
    'scModalParams',
    'News',
    function(scModalParams, News) {
      return News.newPublication({
        id: scModalParams.newsId
      }).$promise;
    }
  ]
};

export { PublishNewsModalCtrl };
