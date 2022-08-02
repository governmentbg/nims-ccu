import _ from 'lodash';

function ContractReportFinancialCertCorrectionsCSDsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  scConfirm,
  contractReportFinancialCSDBudgetItems
) {
  $scope.contractReportFinancialCertCorrectionId = $stateParams.id;
  $scope.contractReportFinancialCertCorrectionStatus =
    $scope.contractReportFinancialCertCorrectionInfo.status;

  $scope.contractReportFinancialCSDBudgetItems = _.forEach(
    contractReportFinancialCSDBudgetItems,
    function(item) {
      csdNameCreator(item);
    }
  );

  $scope.correctBudgetItem = function(contractReportFinancialCSDBudgetItemId) {
    return scConfirm({
      confirmMessage:
        'contractReportFinancialCertCorrections_' +
        'contractReportFinancialCertCorrectionsCSDsSearch_confirmCorrect',
      resource: 'ContractReportFinancialCertCorrectionCSD',
      action: 'save',
      params: {
        id: $stateParams.id,
        contractReportFinancialCSDBudgetItemId: contractReportFinancialCSDBudgetItemId
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.filters = {
    csd: null,
    company: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.contractReportFinancialCertCorrections.view.csds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportFinancialCertCorrectionsCSDsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'scConfirm',
  'contractReportFinancialCSDBudgetItems'
];

ContractReportFinancialCertCorrectionsCSDsSearchCtrl.$resolve = {
  contractReportFinancialCSDBudgetItems: [
    '$stateParams',
    'contractReportFinancialCertCorrectionInfo',
    'ContractReportFinancialCertCorrection',
    function(
      $stateParams,
      contractReportFinancialCertCorrectionInfo,
      ContractReportFinancialCertCorrection
    ) {
      return ContractReportFinancialCertCorrection.getFinancialCSDBudgetItems({
        id: $stateParams.id,
        contractReportId: contractReportFinancialCertCorrectionInfo.contractReportId,
        csd: $stateParams.csd,
        company: $stateParams.company
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCertCorrectionsCSDsSearchCtrl };
