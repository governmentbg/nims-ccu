function ContractReportRevalidationsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportRevalidationData
) {
  $scope.contractReportRevalidationData = contractReportRevalidationData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contractReportRevalidations_basicData_draftConfirm',
      resource: 'ContractReportRevalidation',
      action: 'setToDraft',
      validationAction: 'canSetToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportRevalidationData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'contractReportRevalidations_basicData_enterConfirm',
      resource: 'ContractReportRevalidation',
      action: 'enter',
      validationAction: 'canEnter',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportRevalidationData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'contractReportRevalidations_basicData_removeConfirm',
      noteLabel: 'contractReportRevalidations_basicData_removeNote',
      resource: 'ContractReportRevalidation',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportRevalidationData.version
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
      resource: 'ContractReportRevalidation',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportRevalidationData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportRevalidations.search');
      }
    });
  };
}

ContractReportRevalidationsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportRevalidationData'
];

ContractReportRevalidationsBasicViewCtrl.$resolve = {
  contractReportRevalidationData: [
    'ContractReportRevalidation',
    '$stateParams',
    function(ContractReportRevalidation, $stateParams) {
      return ContractReportRevalidation.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportRevalidationsBasicViewCtrl };
