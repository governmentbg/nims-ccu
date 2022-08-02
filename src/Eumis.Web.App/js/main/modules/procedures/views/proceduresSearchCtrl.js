import _ from 'lodash';

function ProceduresSearchCtrl($scope, $state, $stateParams, $interpolate, procedures) {
  $scope.filters = {
    programmeId: null,
    programmePriorityId: null
  };
  $scope.proceduresExportUrl = $interpolate(
    'api/procedures/excelExport?' +
      'programmeId={{programmeId}}&programmePriorityId={{programmePriorityId}}'
  )({
    programmeId: $stateParams.programmeId,
    programmePriorityId: $stateParams.programmePriorityId
  });

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.procedures = procedures;

  $scope.search = function() {
    return $state.go('root.procedures.search', {
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId
    });
  };
}

ProceduresSearchCtrl.$inject = ['$scope', '$state', '$stateParams', '$interpolate', 'procedures'];

ProceduresSearchCtrl.$resolve = {
  procedures: [
    '$stateParams',
    'Procedure',
    function($stateParams, Procedure) {
      return Procedure.query($stateParams).$promise;
    }
  ]
};

export { ProceduresSearchCtrl };
