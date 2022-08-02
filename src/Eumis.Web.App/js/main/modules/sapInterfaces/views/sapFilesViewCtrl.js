function SapFilesViewCtrl($scope, sapFileInfo) {
  $scope.sapFileInfo = sapFileInfo;
  $scope.tabList = {
    sapFiles_view_data: 'root.sapFiles.view.edit'
  };
  if ($scope.sapFileInfo.type === 'distributedLimits') {
    $scope.tabList['sapFiles_view_distributedLimits'] = 'root.sapFiles.view.distributedLimits';
  }
  if ($scope.sapFileInfo.type === 'paidAmounts') {
    $scope.tabList['sapFiles_view_paidAmounts'] = 'root.sapFiles.view.paidAmounts';
  }
}

SapFilesViewCtrl.$inject = ['$scope', 'sapFileInfo'];

SapFilesViewCtrl.$resolve = {
  sapFileInfo: [
    'SapFile',
    '$stateParams',
    function(SapFile, $stateParams) {
      return SapFile.getInfo({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { SapFilesViewCtrl };
