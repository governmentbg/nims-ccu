function ContractSpendingPlansEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractSpendingPlan,
  spendingPlan
) {
  $scope.spendingPlanId = $stateParams.spid;
  $scope.spendingPlan = spendingPlan;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editSpendingPlanForm.$validate().then(function() {
      if ($scope.editSpendingPlanForm.$valid) {
        return ContractSpendingPlan.update(
          {
            id: $stateParams.id,
            spid: $stateParams.spid
          },
          $scope.spendingPlan
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
      resource: 'ContractSpendingPlan',
      action: 'remove',
      params: {
        id: $stateParams.id,
        spid: $stateParams.spid,
        version: $scope.spendingPlan.version
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
      confirmMessage: 'contracts_spendingPlansEdit_confirmDraft',
      resource: 'ContractSpendingPlan',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        spid: $stateParams.spid,
        version: $scope.spendingPlan.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.check = function() {
    return scConfirm({
      confirmMessage: 'contracts_spendingPlansEdit_confirmCheck',
      resource: 'ContractSpendingPlan',
      action: 'markAsChecked',
      params: {
        id: $stateParams.id,
        spid: $stateParams.spid,
        version: $scope.spendingPlan.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.spendingPlanUpdated = function() {
    return $state.partialReload();
  };
}

ContractSpendingPlansEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractSpendingPlan',
  'spendingPlan'
];

ContractSpendingPlansEditCtrl.$resolve = {
  spendingPlan: [
    'ContractSpendingPlan',
    '$stateParams',
    function(ContractSpendingPlan, $stateParams) {
      return ContractSpendingPlan.get({
        id: $stateParams.id,
        spid: $stateParams.spid
      }).$promise;
    }
  ]
};

export { ContractSpendingPlansEditCtrl };
