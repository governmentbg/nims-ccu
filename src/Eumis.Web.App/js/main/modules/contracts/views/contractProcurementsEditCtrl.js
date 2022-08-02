function ContractProcurementsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractProcurement,
  procurement
) {
  $scope.procurementId = $stateParams.pid;
  $scope.procurement = procurement;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcurementForm.$validate().then(function() {
      if ($scope.editProcurementForm.$valid) {
        return ContractProcurement.update(
          {
            id: $stateParams.id,
            pid: $stateParams.pid
          },
          $scope.procurement
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractProcurement',
      action: 'remove',
      params: {
        id: $stateParams.id,
        pid: $stateParams.pid,
        version: $scope.procurement.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contracts.view.amendments.search', $stateParams, {
          reload: true
        });
      }
    });
  };

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contracts_procurementsEdit_confirmDraft',
      resource: 'ContractProcurement',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        pid: $stateParams.pid,
        version: $scope.procurement.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.check = function() {
    return scConfirm({
      confirmMessage: 'contracts_procurementsEdit_confirmCheck',
      resource: 'ContractProcurement',
      action: 'markAsChecked',
      params: {
        id: $stateParams.id,
        pid: $stateParams.pid,
        version: $scope.procurement.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.procurementUpdated = function() {
    return $state.partialReload();
  };
}

ContractProcurementsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractProcurement',
  'procurement'
];

ContractProcurementsEditCtrl.$resolve = {
  procurement: [
    'ContractProcurement',
    '$stateParams',
    function(ContractProcurement, $stateParams) {
      return ContractProcurement.get({
        id: $stateParams.id,
        pid: $stateParams.pid
      }).$promise;
    }
  ]
};

export { ContractProcurementsEditCtrl };
