function ContractDebtVersionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractDebtVersion,
  contractDebtVersion
) {
  $scope.editMode = null;
  $scope.contractDebtVersion = contractDebtVersion;

  $scope.save = function() {
    return $scope.editContractDebtVersionForm.$validate().then(function() {
      if ($scope.editContractDebtVersionForm.$valid) {
        return ContractDebtVersion.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.contractDebtVersion
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.savePartial = function() {
    return $scope.editContractDebtVersionForm.$validate().then(function() {
      if ($scope.editContractDebtVersionForm.$valid) {
        return scConfirm({
          resource: 'ContractDebtVersion',
          validationAction: 'canUpdatePartial',
          params: {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          data: $scope.contractDebtVersion
        }).then(function(result) {
          if (result.executed) {
            return ContractDebtVersion.updatePartial(
              {
                id: $stateParams.id,
                ind: $stateParams.ind
              },
              $scope.contractDebtVersion
            ).$promise.then(function() {
              return $state.partialReload();
            });
          }
        });
      }
    });
  };

  $scope.changeStatus = function(status) {
    var confirmMsg = null,
      validationAction = null;

    if (status === 'actual') {
      confirmMsg = 'contractDebts_editContractDebtVersion_actualReason';
      validationAction = 'canChangeStatusToActual';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'ContractDebtVersion',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractDebtVersion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractDebtVersion',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.contractDebtVersion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractDebts.view.versions');
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    if (contractDebtVersion.status === 'draft') {
      $scope.editMode = 'edit';
    } else {
      $scope.editMode = 'partial';
    }
  };
}

ContractDebtVersionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractDebtVersion',
  'contractDebtVersion'
];

ContractDebtVersionsEditCtrl.$resolve = {
  contractDebtVersion: [
    'ContractDebtVersion',
    '$stateParams',
    function(ContractDebtVersion, $stateParams) {
      return ContractDebtVersion.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractDebtVersionsEditCtrl };
