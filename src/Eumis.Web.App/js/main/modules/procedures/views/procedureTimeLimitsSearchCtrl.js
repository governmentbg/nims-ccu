import _ from 'lodash';

function ProcedureTimeLimitsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureTimeLimit,
  procedureTimeLimits
) {
  $scope.procedureId = $stateParams.id;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureTimeLimits = procedureTimeLimits;
  if (!$scope.isProcedureReadonly && procedureTimeLimits.length > 0) {
    _.last($scope.procedureTimeLimits).isLast = true;
  }
}

ProcedureTimeLimitsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureTimeLimit',
  'procedureTimeLimits'
];

ProcedureTimeLimitsSearchCtrl.$resolve = {
  procedureTimeLimits: [
    '$stateParams',
    'ProcedureTimeLimit',
    function($stateParams, ProcedureTimeLimit) {
      return ProcedureTimeLimit.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureTimeLimitsSearchCtrl };
