function ContractReportCorrectionsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportCorrectionData
) {
  $scope.contractReportCorrectionData = contractReportCorrectionData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contractReportCorrections_basicData_draftConfirm',
      resource: 'ContractReportCorrection',
      action: 'setToDraft',
      validationAction: 'canSetToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCorrectionData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'contractReportCorrections_basicData_enterConfirm',
      resource: 'ContractReportCorrection',
      action: 'enter',
      validationAction: 'canEnter',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCorrectionData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'contractReportCorrections_basicData_removeConfirm',
      noteLabel: 'contractReportCorrections_basicData_removeNote',
      resource: 'ContractReportCorrection',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCorrectionData.version
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
      resource: 'ContractReportCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCorrectionData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportCorrections.search');
      }
    });
  };
}

ContractReportCorrectionsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportCorrectionData'
];

ContractReportCorrectionsBasicViewCtrl.$resolve = {
  contractReportCorrectionData: [
    'ContractReportCorrection',
    '$stateParams',
    function(ContractReportCorrection, $stateParams) {
      return ContractReportCorrection.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCorrectionsBasicViewCtrl };
