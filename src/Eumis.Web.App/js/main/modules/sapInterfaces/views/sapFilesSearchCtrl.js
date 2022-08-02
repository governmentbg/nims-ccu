import _ from 'lodash';

function SapFilesSearchCtrl($scope, $state, $stateParams, sapFiles) {
  $scope.filters = {
    status: null,
    type: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.sapFiles = sapFiles;

  $scope.search = function() {
    return $state.go('root.sapFiles.search', {
      status: $scope.filters.status,
      type: $scope.filters.type
    });
  };
}

SapFilesSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'sapFiles'];

SapFilesSearchCtrl.$resolve = {
  sapFiles: [
    '$stateParams',
    'SapFile',
    function($stateParams, SapFile) {
      return SapFile.query($stateParams).$promise;
    }
  ]
};

export { SapFilesSearchCtrl };
