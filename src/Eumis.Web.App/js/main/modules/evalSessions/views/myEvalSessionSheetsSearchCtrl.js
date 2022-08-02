import _ from 'lodash';

function MyEvalSessionSheetsSearchCtrl($scope, $state, $stateParams, evalSessionSheets) {
  $scope.evalSessionId = $stateParams.id;
  $scope.evalSessionSheets = evalSessionSheets;

  $scope.filters = {
    project: null,
    evalTableType: null,
    distribution: null,
    statuses: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.evalSessions.my.view.sheets.search', {
      id: $stateParams.id,
      project: $scope.filters.project,
      evalTableType: $scope.filters.evalTableType,
      distribution: $scope.filters.distribution,
      statuses: $scope.filters.statuses
    });
  };
}

MyEvalSessionSheetsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'evalSessionSheets'];

MyEvalSessionSheetsSearchCtrl.$resolve = {
  evalSessionSheets: [
    '$stateParams',
    'MyEvalSessionSheet',
    function($stateParams, MyEvalSessionSheet) {
      if ($stateParams.statuses) {
        $stateParams.statuses = $stateParams.statuses.split(',');
      } else {
        $stateParams.statuses = null;
      }
      return MyEvalSessionSheet.query($stateParams).$promise;
    }
  ]
};

export { MyEvalSessionSheetsSearchCtrl };
