function FinancialCorrectionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  FinancialCorrection,
  financialCorrection
) {
  $scope.editMode = null;
  $scope.financialCorrection = financialCorrection;

  $scope.save = function() {
    return $scope.editFinancialCorrectionForm.$validate().then(function() {
      if ($scope.editFinancialCorrectionForm.$valid) {
        return FinancialCorrection.update(
          { id: $stateParams.id },
          $scope.financialCorrection
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

  $scope.cancelCorrection = function() {
    return scConfirm({
      confirmMessage: 'financialCorrections_editFinancialCorrection_cancelConfirm',
      noteLabel: 'financialCorrections_editFinancialCorrection_cancelMessage',
      resource: 'FinancialCorrection',
      action: 'cancel',
      params: {
        id: $stateParams.id,
        version: $scope.financialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activateCorrection = function() {
    return scConfirm({
      confirmMessage: 'financialCorrections_editFinancialCorrection_actualReason',
      resource: 'FinancialCorrection',
      action: 'changeStatusToEntered',
      params: {
        id: $stateParams.id,
        version: $scope.financialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.delCorrection = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'FinancialCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.financialCorrection.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.financialCorrections.search');
      }
    });
  };
}

FinancialCorrectionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'FinancialCorrection',
  'financialCorrection'
];

FinancialCorrectionsEditCtrl.$resolve = {
  financialCorrection: [
    'FinancialCorrection',
    '$stateParams',
    function(FinancialCorrection, $stateParams) {
      return FinancialCorrection.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { FinancialCorrectionsEditCtrl };
