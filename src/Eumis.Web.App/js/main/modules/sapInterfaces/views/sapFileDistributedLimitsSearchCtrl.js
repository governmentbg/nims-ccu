function SapFileDistributedLimitsSearchCtrl($scope, $stateParams, distributedLimits) {
  $scope.distributedLimits = distributedLimits;
  $scope.sapFileStatus = $scope.sapFileInfo.status;
}

SapFileDistributedLimitsSearchCtrl.$inject = ['$scope', '$stateParams', 'distributedLimits'];

SapFileDistributedLimitsSearchCtrl.$resolve = {
  distributedLimits: [
    'SapFile',
    '$stateParams',
    function(SapFile, $stateParams) {
      return SapFile.getDistributedLimits({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { SapFileDistributedLimitsSearchCtrl };
