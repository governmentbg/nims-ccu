import _ from 'lodash';

function EvalSessionSheetsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  evalSessionSheets
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.evalSessionSheets = evalSessionSheets;
  $scope.sheetsExportUrl = $interpolate(
    'api/evalSessions/{{id}}/sheetsExcelExport?' +
      'project={{project}}&evalTableType={{evalTableType}}&distribution={{distribution}}' +
      '&assessor={{assessor}}'
  )({
    id: $stateParams.id,
    project: $stateParams.project,
    evalTableType: $stateParams.evalTableType,
    distribution: $stateParams.distribution,
    assessor: $stateParams.assessor
  });

  if ($stateParams.statuses) {
    $scope.sheetsExportUrl =
      $scope.sheetsExportUrl +
      _.reduce(
        $stateParams.statuses,
        function(result, val) {
          return result + '&statuses=' + val;
        },
        ''
      );
  }

  $scope.filters = {
    project: null,
    evalTableType: null,
    distribution: null,
    assessor: null,
    statuses: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.evalSessions.view.sheets.search', {
      id: $stateParams.id,
      project: $scope.filters.project,
      evalTableType: $scope.filters.evalTableType,
      distribution: $scope.filters.distribution,
      assessor: $scope.filters.assessor,
      statuses: $scope.filters.statuses
    });
  };
}

EvalSessionSheetsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'evalSessionSheets'
];

EvalSessionSheetsSearchCtrl.$resolve = {
  evalSessionSheets: [
    '$stateParams',
    'EvalSessionSheet',
    function($stateParams, EvalSessionSheet) {
      if ($stateParams.statuses) {
        $stateParams.statuses = $stateParams.statuses.split(',');
      } else {
        $stateParams.statuses = null;
      }
      return EvalSessionSheet.query($stateParams).$promise;
    }
  ]
};

export { EvalSessionSheetsSearchCtrl };
