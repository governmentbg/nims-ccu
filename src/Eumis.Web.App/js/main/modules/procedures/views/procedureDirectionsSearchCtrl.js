import angular from 'angular';

function ProcedureDirectionsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProcedureDirection,
  procedureDirections
) {
  $scope.procedureId = $stateParams.id;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureDirections = procedureDirections;

  $scope.chooseDirections = function() {
    var modalInstance = scModal.open('chooseDirectionModal', {
      procedureId: $stateParams.id,
      version: $scope.info.version
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };
}

ProcedureDirectionsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProcedureDirection',
  'procedureDirections'
];

ProcedureDirectionsSearchCtrl.$resolve = {
  procedureDirections: [
    '$stateParams',
    'ProcedureDirection',
    function($stateParams, ProcedureDirection) {
      return ProcedureDirection.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureDirectionsSearchCtrl };
