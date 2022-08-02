function MyEvalSessionSheetsEditCtrl($scope, $state, evalSessionSheet) {
  $scope.evalSessionSheet = evalSessionSheet;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';

  $scope.sheetUpdated = function() {
    return $state.partialReload();
  };
}

MyEvalSessionSheetsEditCtrl.$inject = ['$scope', '$state', 'evalSessionSheet'];

MyEvalSessionSheetsEditCtrl.$resolve = {
  evalSessionSheet: [
    '$stateParams',
    'MyEvalSessionSheet',
    function($stateParams, MyEvalSessionSheet) {
      return MyEvalSessionSheet.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { MyEvalSessionSheetsEditCtrl };
