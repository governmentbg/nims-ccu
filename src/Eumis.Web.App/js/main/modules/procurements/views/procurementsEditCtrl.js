function ProcurementsEditCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  l10n,
  scConfirm,
  Procurement,
  procurement
) {
  $scope.editMode = null;
  $scope.procurement = procurement;

  $scope.changeStatus = function(status) {
    var validationAction = null,
      confirmMsg = $interpolate(l10n.get('procurements_editProcurement_confirmChangeStatus'))({
        status: l10n.get('procurements_editProcurement_' + status)
      });

    if (status === 'active') {
      validationAction = 'canChangeStatusToActive';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      validationAction: validationAction,
      resource: 'Procurement',
      action: 'changeStatusTo' + status.charAt(0).toUpperCase() + status.slice(1),
      params: {
        id: $stateParams.id,
        version: $scope.procurement.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.save = function() {
    return $scope.editProcurementForm.$validate().then(function() {
      if ($scope.editProcurementForm.$valid) {
        return Procurement.update({ id: $stateParams.id }, $scope.procurement).$promise.then(
          function() {
            return $state.partialReload();
          }
        );
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
      resource: 'Procurement',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.procurement.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procurements.search');
      }
    });
  };
}

ProcurementsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'l10n',
  'scConfirm',
  'Procurement',
  'procurement'
];

ProcurementsEditCtrl.$resolve = {
  procurement: [
    'Procurement',
    '$stateParams',
    function(Procurement, $stateParams) {
      return Procurement.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcurementsEditCtrl };
