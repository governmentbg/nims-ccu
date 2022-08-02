function ProcedureSharesEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureShare,
  procedureShare,
  scMessage,
  scConfirm
) {
  $scope.editMode = null;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureShare = procedureShare;

  $scope.save = function() {
    return $scope.editProcedureShareForm.$validate().then(function() {
      if ($scope.editProcedureShareForm.$valid) {
        return ProcedureShare.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.procedureShare
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.del = function() {
    if ($scope.procedureShare.isPrimary) {
      return scMessage('procedures_procedureShare_edit_msg_unableToDeletePrimaryShare', [
        {
          name: 'OK',
          l10nLabel: 'scaffolding.scMessage.okButton',
          type: 'btn-primary',
          icon: 'glyphicon-ok'
        }
      ]);
    } else if ($scope.procedureShare.hasLinkedExpenseBudgets) {
      return scMessage('procedures_procedureShare_edit_msg_unableToDeleteLinedBudgedShare', [
        {
          name: 'OK',
          l10nLabel: 'scaffolding.scMessage.okButton',
          type: 'btn-primary',
          icon: 'glyphicon-ok'
        }
      ]);
    } else {
      return scConfirm({
        confirmMessage: 'common_messages_confirmDelete',
        resource: 'ProcedureShare',
        action: 'remove',
        params: {
          id: $stateParams.id,
          ind: $stateParams.ind,
          version: $scope.procedureShare.version
        }
      }).then(function(result) {
        if (result.executed) {
          return $state.go('root.procedures.view.procedureShares.search');
        }
      });
    }
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };
}

ProcedureSharesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureShare',
  'procedureShare',
  'scMessage',
  'scConfirm'
];

ProcedureSharesEditCtrl.$resolve = {
  procedureShare: [
    '$stateParams',
    'ProcedureShare',
    function($stateParams, ProcedureShare) {
      return ProcedureShare.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProcedureSharesEditCtrl };
