import _ from 'lodash';

function NewsSearchCtrl($scope, $state, $stateParams, news) {
  $scope.news = news;

  $scope.filters = {
    dateFrom: null,
    dateTo: null,
    type: null,
    status: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.news.search', $scope.filters);
  };
}

NewsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'news'];

NewsSearchCtrl.$resolve = {
  news: [
    '$stateParams',
    'News',
    function($stateParams, News) {
      return News.query($stateParams).$promise;
    }
  ]
};

export { NewsSearchCtrl };
