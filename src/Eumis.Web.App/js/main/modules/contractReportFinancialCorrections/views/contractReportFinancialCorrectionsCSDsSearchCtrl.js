import _ from 'lodash';

function ContractReportFinancialCorrectionsCSDsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  scConfirm,
  contractReportFinancialCSDBudgetItems
) {
  $scope.contractReportFinancialCorrectionId = $stateParams.id;
  $scope.contractReportFinancialCorrectionStatus =
    $scope.contractReportFinancialCorrectionInfo.status;

  $scope.contractReportFinancialCSDBudgetItems = _.forEach(
    contractReportFinancialCSDBudgetItems,
    function(item) {
      csdNameCreator(item);
    }
  );

  $scope.correctBudgetItem = function(contractReportFinancialCSDBudgetItemId) {
    return scConfirm({
      confirmMessage:
        'contractReportFinancialCorrections_' +
        'contractReportFinancialCorrectionsCSDsSearch_confirmCorrect',
      resource: 'ContractReportFinancialCorrectionCSD',
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
    return $state.go('root.contractReportFinancialCorrections.view.csds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportFinancialCorrectionsCSDsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'scConfirm',
  'contractReportFinancialCSDBudgetItems'
];

ContractReportFinancialCorrectionsCSDsSearchCtrl.$resolve = {
  contractReportFinancialCSDBudgetItems: [
    '$stateParams',
    'contractReportFinancialCorrectionInfo',
    'ContractReportFinancialCorrection',
    function(
      $stateParams,
      contractReportFinancialCorrectionInfo,
      ContractReportFinancialCorrection
    ) {
      return ContractReportFinancialCorrection.getFinancialCSDBudgetItems({
        id: $stateParams.id,
        contractReportId: contractReportFinancialCorrectionInfo.contractReportId,
        csd: $stateParams.csd,
        company: $stateParams.company
      }).$promise;
    }
  ]
};

export { ContractReportFinancialCorrectionsCSDsSearchCtrl };
