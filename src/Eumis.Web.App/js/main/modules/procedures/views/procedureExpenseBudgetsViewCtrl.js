import angular from 'angular';
import _ from 'lodash';

function ProcedureExpenseBudgetsViewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scModal,
  scConfirm,
  ProcedureShareExpenseBudget,
  expenseBudgetTree,
  romanizer
) {
  $scope.expenseBudgetTree = expenseBudgetTree;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureId = $stateParams.id;

  $scope.deminimisShortName = l10n.get('procedures_expenseBudgets_view_deminimis');
  $scope.stateAidShortName = l10n.get('procedures_expenseBudgets_view_stateAid');
  $scope.notApplicableShortName = l10n.get('procedures_expenseBudgets_view_notApplicable');

  $scope.validationMessageHeader = l10n.get('procedures_expenseBudgets_view_message');

  $scope.expenseBudgetTree.programmes = _.map($scope.expenseBudgetTree.programmes, function(lev0) {
    var lev2code = 1;

    lev0.programmeName =
      l10n.get('procedures_expenseBudgets_view_prrogrammePrefix') + lev0.programmeName;

    lev0.level1Items = _.map(lev0.level1Items, function(lev1) {
      lev1.level2Items = _.map(lev1.level2Items, function(lev2) {
        lev2.code = lev2code++;
        return lev2;
      });
      return lev1;
    });

    return lev0;
  });

  $scope.getAidModeShortName = function(aidMode) {
    if (aidMode === 'deminimis') {
      return $scope.deminimisShortName;
    } else if (aidMode === 'stateAid') {
      return $scope.stateAidShortName;
    } else {
      return $scope.notApplicableShortName;
    }
  };

  $scope.romanize = function(val) {
    return romanizer(val);
  };

  $scope.addLevel1 = function(programmeId) {
    var modalInstance = scModal.open('budgetLevel1Modal', {
      procedureId: $stateParams.id,
      programmeId: programmeId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteLevel1 = function(procedureBudgetLevel1Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureShareExpenseBudget',
      validationAction: 'canDeleteLevel1',
      action: 'deleteLevel1',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel1Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.deactivateLevel1 = function(procedureBudgetLevel1Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureShareExpenseBudget',
      action: 'deactivateLevel1',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel1Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activateLevel1 = function(procedureBudgetLevel1Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ProcedureShareExpenseBudget',
      action: 'activateLevel1',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel1Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.addLevel2 = function(procedureBudgetLevel1Id) {
    var modalInstance = scModal.open('budgetLevel2Modal', {
      procedureId: $stateParams.id,
      procedureBudgetLevel1Id: procedureBudgetLevel1Id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editLevel2 = function(procedureBudgetLevel2Id) {
    var modalInstance = scModal.open('budgetLevel2Modal', {
      procedureId: $stateParams.id,
      procedureBudgetLevel2Id: procedureBudgetLevel2Id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.previewLevel2 = function(procedureBudgetLevel2Id) {
    var modalInstance = scModal.open('budgetLevel2Modal', {
      procedureId: $stateParams.id,
      procedureBudgetLevel2Id: procedureBudgetLevel2Id,
      previewMode: true
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteLevel2 = function(procedureBudgetLevel2Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureShareExpenseBudget',
      validationAction: 'canDeleteLevel2',
      action: 'deleteLevel2',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel2Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.deactivateLevel2 = function(procedureBudgetLevel2Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureShareExpenseBudget',
      action: 'deactivateLevel2',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel2Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activateLevel2 = function(procedureBudgetLevel2Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ProcedureShareExpenseBudget',
      action: 'activateLevel2',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel2Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.addLevel3 = function(procedureBudgetLevel2Id) {
    var modalInstance = scModal.open('budgetLevel3Modal', {
      procedureId: $stateParams.id,
      procedureBudgetLevel2Id: procedureBudgetLevel2Id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editLevel3 = function(procedureBudgetLevel3Id) {
    var modalInstance = scModal.open('budgetLevel3Modal', {
      procedureId: $stateParams.id,
      procedureBudgetLevel3Id: procedureBudgetLevel3Id
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteLevel3 = function(procedureBudgetLevel3Id) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureShareExpenseBudget',
      action: 'deleteLevel3',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetLevel3Id,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.addValidationRule = function(programmeId) {
    var modalInstance = scModal.open('budgetValidationRuleModal', {
      procedureId: $stateParams.id,
      programmeId: programmeId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.editValidationRule = function(procedureBudgetValidationRuleId) {
    var modalInstance = scModal.open('budgetValidationRuleModal', {
      procedureId: $stateParams.id,
      procedureBudgetValidationRuleId: procedureBudgetValidationRuleId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteValidationRule = function(procedureBudgetValidationRuleId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureShareExpenseBudget',
      action: 'deleteValidationRule',
      params: {
        id: $stateParams.id,
        ind: procedureBudgetValidationRuleId,
        version: $scope.expenseBudgetTree.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProcedureExpenseBudgetsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scModal',
  'scConfirm',
  'ProcedureShareExpenseBudget',
  'expenseBudgetTree',
  'romanizer'
];

ProcedureExpenseBudgetsViewCtrl.$resolve = {
  expenseBudgetTree: [
    '$stateParams',
    'ProcedureShareExpenseBudget',
    function($stateParams, ProcedureShareExpenseBudget) {
      return ProcedureShareExpenseBudget.getTree({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureExpenseBudgetsViewCtrl };
