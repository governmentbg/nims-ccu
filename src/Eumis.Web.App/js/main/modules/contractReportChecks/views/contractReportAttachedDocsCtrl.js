import angular from 'angular';

function ContractReportAttachedDocsCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  financialCorrections
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportSource = $scope.contractReportInfo.source;
  $scope.contractReportVersion = $scope.contractReportInfo.version;
  $scope.financialCorrections = financialCorrections;

  $scope.chooseCorrection = function() {
    var modalInstance = scModal.open('chooseFinancialCorrection', {
      contractReportId: $stateParams.id,
      version: $scope.contractReportVersion
    });

    modalInstance.result.then(function(contractReportFinancialCorrectionId) {
      return $state.go('root.contractReportChecks.view.attachedDocs.viewFinCor', {
        id: $stateParams.id,
        ind: contractReportFinancialCorrectionId
      });
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delCorrection = function(contractReportFinancialCorrectionId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReportAttachedFinancialCorrection',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: contractReportFinancialCorrectionId,
        version: $scope.contractReportVersion
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ContractReportAttachedDocsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'financialCorrections'
];

ContractReportAttachedDocsCtrl.$resolve = {
  financialCorrections: [
    '$stateParams',
    'ContractReportAttachedFinancialCorrection',
    function($stateParams, ContractReportAttachedFinancialCorrection) {
      return ContractReportAttachedFinancialCorrection.query($stateParams).$promise;
    }
  ]
};

export { ContractReportAttachedDocsCtrl };
