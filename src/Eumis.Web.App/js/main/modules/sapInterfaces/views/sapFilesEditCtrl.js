function SapFilesEditCtrl($scope, $state, $stateParams, scConfirm, sapFile) {
  $scope.sapFile = sapFile;

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'SapFile',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.sapFile.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.sapFiles.search');
      }
    });
  };

  $scope.import = function() {
    return scConfirm({
      confirmMessage: 'sapFiles_editSapFile_importConfirm',
      resource: 'SapFile',
      action: 'importSapFile',
      params: {
        id: $stateParams.id,
        version: $scope.sapFile.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

SapFilesEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'scConfirm', 'sapFile'];

SapFilesEditCtrl.$resolve = {
  sapFile: [
    'SapFile',
    '$stateParams',
    function(SapFile, $stateParams) {
      return SapFile.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { SapFilesEditCtrl };
