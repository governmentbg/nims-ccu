function ContractReportCertCorrectionsBasicViewCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  contractReportCertCorrectionData
) {
  $scope.contractReportCertCorrectionData = contractReportCertCorrectionData;

  $scope.setToDraft = function() {
    return scConfirm({
      confirmMessage: 'contractReportCertCorrections_basicData_draftConfirm',
      resource: 'ContractReportCertCorrection',
      action: 'setToDraft',
      validationAction: 'canSetToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCertCorrectionData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.enter = function() {
    return scConfirm({
      confirmMessage: 'contractReportCertCorrections_basicData_enterConfirm',
      resource: 'ContractReportCertCorrection',
      action: 'enter',
      validationAction: 'canEnter',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCertCorrectionData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.remove = function() {
    return scConfirm({
      confirmMessage: 'contractReportCertCorrections_basicData_removeConfirm',
      noteLabel: 'contractReportCertCorrections_basicData_removeNote',
      resource: 'ContractReportCertCorrection',
      action: 'setToRemoved',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCertCorrectionData.version
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
      resource: 'ContractReportCertCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.contractReportCertCorrectionData.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportCertCorrections.search');
      }
    });
  };
}

ContractReportCertCorrectionsBasicViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'contractReportCertCorrectionData'
];

ContractReportCertCorrectionsBasicViewCtrl.$resolve = {
  contractReportCertCorrectionData: [
    'ContractReportCertCorrection',
    '$stateParams',
    function(ContractReportCertCorrection, $stateParams) {
      return ContractReportCertCorrection.getBasicData({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ContractReportCertCorrectionsBasicViewCtrl };
