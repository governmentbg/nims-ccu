function SapFilePaidAmountsSearchCtrl($scope, $stateParams, paidAmounts) {
  $scope.paidAmounts = paidAmounts;
  $scope.sapFileStatus = $scope.sapFileInfo.status;
}

SapFilePaidAmountsSearchCtrl.$inject = ['$scope', '$stateParams', 'paidAmounts'];

SapFilePaidAmountsSearchCtrl.$resolve = {
  paidAmounts: [
    'SapFile',
    '$stateParams',
    function(SapFile, $stateParams) {
      return SapFile.getPaidAmounts({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { SapFilePaidAmountsSearchCtrl };
