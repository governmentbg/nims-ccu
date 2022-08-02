import angular from 'angular';
import _ from 'lodash';

function PPriorityBudgetsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  l10n,
  ProgrammePriorityBudget,
  budget
) {
  $scope.budget = budget;
  $scope.editMode = null;
  $scope.programmePriorityStatus = $scope.info.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editPPriorityBudgetForm.$validate().then(function() {
      if ($scope.editPPriorityBudgetForm.$valid) {
        var hasBudget = false,
          modalInstance;

        _.forOwn($scope.budget.budgets, function(b) {
          if (b.isActive) {
            hasBudget = true;
          }
        });

        if (hasBudget) {
          return ProgrammePriorityBudget.update(
            {
              id: $stateParams.id,
              ind: $stateParams.ind
            },
            $scope.budget
          ).$promise.then(function() {
            $state.partialReload();
          });
        } else {
          modalInstance = scModal.open('validationErrorsModal', {
            errors: [l10n.get('programmePriorities_editPPriorityBudget_missingBudgetError')]
          });
          modalInstance.result.catch(angular.noop);
          return modalInstance.opened;
        }
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteBudget = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProgrammePriorityBudget',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.budget.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.ppriorities.view.budgets.search');
      }
    });
  };
}

PPriorityBudgetsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'l10n',
  'ProgrammePriorityBudget',
  'budget'
];

PPriorityBudgetsEditCtrl.$resolve = {
  budget: [
    'ProgrammePriorityBudget',
    '$stateParams',
    function(ProgrammePriorityBudget, $stateParams) {
      return ProgrammePriorityBudget.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { PPriorityBudgetsEditCtrl };
