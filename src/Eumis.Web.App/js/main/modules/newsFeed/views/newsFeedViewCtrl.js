import _ from 'lodash';

function NewsFeedViewCtrl($scope, $sce, news) {
  $scope.news = news;
  $scope.content = $sce.trustAsHtml(news.content);
}

NewsFeedViewCtrl.$inject = ['$scope', '$sce', 'news'];

NewsFeedViewCtrl.$resolve = {
  news: [
    '$stateParams',
    'News',
    'NewsFeedFile',
    function($stateParams, News, NewsFeedFile) {
      return News.getNewsFeed({
        id: $stateParams.id
      }).$promise.then(function(result) {
        _.forEach(result.files, function(item) {
          if (item.file) {
            item.file.url = NewsFeedFile.getUrl({
              id: result.newsId,
              fileKey: item.file.key
            });
          }
        });

        return result;
      });
    }
  ]
};

export { NewsFeedViewCtrl };
