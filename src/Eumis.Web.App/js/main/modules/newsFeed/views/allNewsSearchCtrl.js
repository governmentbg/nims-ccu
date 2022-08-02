function AllNewsSearchCtrl($scope, $state, $stateParams, newsQuery) {
  $scope.newsQuery = newsQuery;
  $scope.page = $stateParams.p;

  $scope.view = function(id) {
    return $state.go('root.allNews.view', { id: id });
  };

  $scope.pageChange = function(page) {
    return $state.go('root.allNews.search', {
      p: page
    });
  };
}

AllNewsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'newsQuery'];

AllNewsSearchCtrl.$resolve = {
  newsQuery: [
    'News',
    '$stateParams',
    'pager',
    function(News, $stateParams, pager) {
      var params = pager.getOffsetAndLimit($stateParams.p);

      return News.getAllNews(params).$promise;
    }
  ]
};

export { AllNewsSearchCtrl };
