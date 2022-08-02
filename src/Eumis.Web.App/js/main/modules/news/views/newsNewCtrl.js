function NewsNewCtrl($scope, $state, News, news) {
  $scope.news = news;

  $scope.save = function() {
    return $scope.newNewsForm.$validate().then(function() {
      if ($scope.newNewsForm.$valid) {
        return News.save($scope.news).$promise.then(function(result) {
          return $state.go('root.news.edit', {
            id: result.newsId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.news.search');
  };
}

NewsNewCtrl.$inject = ['$scope', '$state', 'News', 'news'];

NewsNewCtrl.$resolve = {
  news: [
    'News',
    function(News) {
      return News.newNews().$promise;
    }
  ]
};

export { NewsNewCtrl };
