function ProcedureMassCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ProcedureMassCommunication,
  procedureMassCommunication
) {
  $scope.communicationId = $stateParams.id;
  $scope.procedureMassCommunication = procedureMassCommunication;
  $scope.editMode = undefined;

  $scope.edit = function() {
    $scope.editMode = !$scope.editMode;
  };

  $scope.save = function() {
    return $scope.editProcedureMassCommunicationsForm.$validate().then(function() {
      if ($scope.editProcedureMassCommunicationsForm.$valid) {
        return ProcedureMassCommunication.update(
          { id: $stateParams.id },
          $scope.procedureMassCommunication
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.send = function() {
    return scConfirm({
      confirmMessage: 'procedureMassCommunications_procedureMassCommunicationsEdit_sendMessage',
      resource: 'ProcedureMassCommunication',
      validationAction: 'canSend',
      action: 'send',
      params: {
        id: $stateParams.id,
        version: $scope.procedureMassCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedureMassCommunications.search');
      }
    });
  };

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureMassCommunication',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.procedureMassCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedureMassCommunications.search');
      }
    });
  };
}

ProcedureMassCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ProcedureMassCommunication',
  'procedureMassCommunication'
];

ProcedureMassCommunicationsEditCtrl.$resolve = {
  procedureMassCommunication: [
    'ProcedureMassCommunication',
    '$stateParams',
    function(ProcedureMassCommunication, $stateParams) {
      return ProcedureMassCommunication.get({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureMassCommunicationsEditCtrl };
