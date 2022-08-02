import angular from 'angular';

function NewsEditCtrl($scope, $state, $stateParams, scConfirm, scModal, News, news) {
  $scope.editMode = null;
  $scope.news = news;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editNewsForm.$validate().then(function() {
      if ($scope.editNewsForm.$valid) {
        return News.update(
          {
            id: $stateParams.id
          },
          $scope.news
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.publish = function() {
    if ($scope.news.type === 'portal') {
      return News.publish(
        {
          id: $scope.news.newsId
        },
        $scope.news
      ).$promise.then(function() {
        $state.partialReload();
      });
    } else {
      var modalInstance = scModal.open('publishNewsModal', {
        newsId: $scope.news.newsId
      });

      modalInstance.result.then(function() {
        return $state.partialReload();
      }, angular.noop);

      return modalInstance.opened;
    }
  };

  $scope.draft = function() {
    return scConfirm({
      confirmMessage: 'news_editForm_draftConfirm',
      resource: 'News',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.news.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.archive = function() {
    return scConfirm({
      confirmMessage: 'news_editForm_archiveConfirm',
      resource: 'News',
      action: 'archive',
      params: {
        id: $stateParams.id,
        version: $scope.news.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'News',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.news.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.news.search', $stateParams, {
          reload: true
        });
      }
    });
  };
}

NewsEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'scConfirm', 'scModal', 'News', 'news'];

NewsEditCtrl.$resolve = {
  news: [
    'News',
    '$stateParams',
    function(News, $stateParams) {
      return News.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { NewsEditCtrl };
