import _ from 'lodash';

function ProgrammePriorityPrognosesSearchCtrl(
  $scope,
  $state,
  $stateParams,
  programmePriorityPrognoses
) {
  $scope.filters = {
    year: null,
    month: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.programmePriorityPrognoses = programmePriorityPrognoses;

  $scope.search = function() {
    return $state.go('root.programmePriorityPrognoses.search', {
      year: $scope.filters.year,
      month: $scope.filters.month
    });
  };
}

ProgrammePriorityPrognosesSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'programmePriorityPrognoses'
];

ProgrammePriorityPrognosesSearchCtrl.$resolve = {
  programmePriorityPrognoses: [
    '$stateParams',
    'ProgrammePriorityPrognosis',
    function($stateParams, ProgrammePriorityPrognosis) {
      return ProgrammePriorityPrognosis.query($stateParams).$promise;
    }
  ]
};

export { ProgrammePriorityPrognosesSearchCtrl };
