function NewsFeedSearchCtrl($scope, $state, news) {
  $scope.news = news;

  $scope.view = function(id) {
    return $state.go('root.newsFeed.view', { id: id });
  };
}

NewsFeedSearchCtrl.$inject = ['$scope', '$state', 'news'];

NewsFeedSearchCtrl.$resolve = {
  news: [
    'News',
    function(News) {
      return News.getNewsFeeds().$promise;
    }
  ]
};

export { NewsFeedSearchCtrl };
