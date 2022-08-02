import _ from 'lodash';

function ProcedurePrognosesSearchCtrl($scope, $state, $stateParams, procedurePrognoses) {
  $scope.filters = {
    year: null,
    month: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.procedurePrognoses = procedurePrognoses;

  $scope.search = function() {
    return $state.go('root.procedurePrognoses.search', {
      year: $scope.filters.year,
      month: $scope.filters.month
    });
  };
}

ProcedurePrognosesSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'procedurePrognoses'];

ProcedurePrognosesSearchCtrl.$resolve = {
  procedurePrognoses: [
    '$stateParams',
    'ProcedurePrognosis',
    function($stateParams, ProcedurePrognosis) {
      return ProcedurePrognosis.query($stateParams).$promise;
    }
  ]
};

export { ProcedurePrognosesSearchCtrl };
