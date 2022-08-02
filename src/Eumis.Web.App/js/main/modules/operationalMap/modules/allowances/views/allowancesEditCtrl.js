import angular from 'angular';

function AllowancesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scMessage,
  scConfirm,
  scModal,
  Allowance,
  AllowanceRate,
  allowance
) {
  $scope.editMode = null;
  $scope.allowance = allowance;

  $scope.save = function() {
    return $scope.editAllowanceForm.$validate().then(function() {
      if ($scope.editAllowanceForm.$valid) {
        return Allowance.update(
          {
            id: $stateParams.id
          },
          $scope.allowance.allowanceData
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Allowance',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.allowance.allowanceData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.allowances.search');
      }
    });
  };

  $scope.newRate = function() {
    var modalInstance = scModal.open('newAllowanceRateModal', {
      allowanceId: $stateParams.id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editRate = function(allowanceRateId) {
    var modalInstance = scModal.open('editAllowanceRateModal', {
      allowanceId: $stateParams.id,
      allowanceRateId: allowanceRateId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteRate = function(allowanceRateId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'AllowanceRate',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: allowanceRateId,
        version: $scope.allowance.allowanceData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

AllowancesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'scConfirm',
  'scModal',
  'Allowance',
  'AllowanceRate',
  'allowance'
];

AllowancesEditCtrl.$resolve = {
  allowance: [
    'Allowance',
    '$stateParams',
    function(Allowance, $stateParams) {
      return Allowance.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { AllowancesEditCtrl };
