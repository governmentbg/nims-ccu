import angular from 'angular';

function EvalSessionStandpointsNewCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  EvalSessionStandpoint,
  newStandpoint
) {
  $scope.newStandpoint = newStandpoint;

  $scope.save = function() {
    return $scope.newEvalSessionStandpointForm.$validate().then(function() {
      if ($scope.newEvalSessionStandpointForm.$valid) {
        return EvalSessionStandpoint.canCreate(
          {
            id: $stateParams.id
          },
          $scope.newStandpoint
        ).$promise.then(function(error) {
          if (!error) {
            return EvalSessionStandpoint.save(
              {
                id: $stateParams.id
              },
              $scope.newStandpoint
            ).$promise.then(function(result) {
              return $state.go('root.evalSessions.view.standpoints.edit', {
                ind: result.evalSessionStandpointId
              });
            });
          } else {
            var modalInstance = scModal.open('validationErrorsModal', {
              errors: [error]
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.standpoints.search');
  };
}

EvalSessionStandpointsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'EvalSessionStandpoint',
  'newStandpoint'
];

EvalSessionStandpointsNewCtrl.$resolve = {
  newStandpoint: [
    '$stateParams',
    'EvalSessionStandpoint',
    function($stateParams, EvalSessionStandpoint) {
      return EvalSessionStandpoint.newStandpoint($stateParams).$promise;
    }
  ]
};

export { EvalSessionStandpointsNewCtrl };
