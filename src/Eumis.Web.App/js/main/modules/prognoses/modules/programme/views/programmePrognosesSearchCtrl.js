import _ from 'lodash';

function ProgrammePrognosesSearchCtrl($scope, $state, $stateParams, programmePrognoses) {
  $scope.filters = {
    year: null,
    month: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.programmePrognoses = programmePrognoses;

  $scope.search = function() {
    return $state.go('root.programmePrognoses.search', {
      year: $scope.filters.year,
      month: $scope.filters.month
    });
  };
}

ProgrammePrognosesSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'programmePrognoses'];

ProgrammePrognosesSearchCtrl.$resolve = {
  programmePrognoses: [
    '$stateParams',
    'ProgrammePrognosis',
    function($stateParams, ProgrammePrognosis) {
      return ProgrammePrognosis.query($stateParams).$promise;
    }
  ]
};

export { ProgrammePrognosesSearchCtrl };
