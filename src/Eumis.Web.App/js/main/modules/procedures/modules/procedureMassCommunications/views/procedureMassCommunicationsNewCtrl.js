function ProcedureMassCommunicationsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureMassCommunication,
  procedureMassCommunication
) {
  $scope.communicationId = $stateParams.id;
  $scope.procedureMassCommunication = procedureMassCommunication;

  $scope.save = function() {
    return $scope.newProcedureMassCommunicationForm.$validate().then(function() {
      if ($scope.newProcedureMassCommunicationForm.$valid) {
        return ProcedureMassCommunication.save(
          { id: $stateParams.id },
          $scope.procedureMassCommunication
        ).$promise.then(function(data) {
          return $state.go('root.procedureMassCommunications.view.edit', {
            id: data.procedureMassCommunicationId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedureMassCommunications.search');
  };
}

ProcedureMassCommunicationsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureMassCommunication',
  'procedureMassCommunication'
];

ProcedureMassCommunicationsNewCtrl.$resolve = {
  procedureMassCommunication: [
    'ProcedureMassCommunication',
    function(ProcedureMassCommunication) {
      return ProcedureMassCommunication.newProcedureMassCommunications().$promise;
    }
  ]
};

export { ProcedureMassCommunicationsNewCtrl };
