import angular from 'angular';

function ExpenseTypesEditCtrl(
  $scope,
  $state,
  $stateParams,
  scMessage,
  scConfirm,
  scModal,
  ExpenseType,
  ExpenseSubType,
  expenseType
) {
  $scope.editMode = null;
  $scope.expenseType = expenseType;
  $scope.isActive = expenseType.expenseTypeData.isActive;

  $scope.save = function() {
    return $scope.editExpenseTypeForm.$validate().then(function() {
      if ($scope.editExpenseTypeForm.$valid) {
        return ExpenseType.update(
          {
            id: $stateParams.id
          },
          $scope.expenseType.expenseTypeData
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
  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ExpenseType',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        version: $scope.expenseType.expenseTypeData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ExpenseType',
      action: 'activate',
      params: {
        id: $stateParams.id,
        version: $scope.expenseType.expenseTypeData.version
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
      resource: 'ExpenseType',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.expenseType.expenseTypeData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.expenseTypes.search');
      }
    });
  };

  $scope.newSubType = function() {
    var modalInstance = scModal.open('newExpenseSubTypeModal', {
      expenseTypeId: $stateParams.id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editSubType = function(expenseSubTypeId) {
    var modalInstance = scModal.open('editExpenseSubTypeModal', {
      expenseTypeId: $stateParams.id,
      expenseSubTypeId: expenseSubTypeId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteSubType = function(expenseSubTypeId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ExpenseSubType',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: expenseSubTypeId,
        version: $scope.expenseType.expenseTypeData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ExpenseTypesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'scConfirm',
  'scModal',
  'ExpenseType',
  'ExpenseSubType',
  'expenseType'
];

ExpenseTypesEditCtrl.$resolve = {
  expenseType: [
    'ExpenseType',
    '$stateParams',
    function(ExpenseType, $stateParams) {
      return ExpenseType.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ExpenseTypesEditCtrl };
