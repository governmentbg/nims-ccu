import _ from 'lodash';

function ContractReportFinancialRevalidationsCSDsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  scConfirm,
  contractReportFinancialCSDBudgetItems
) {
  $scope.contractReportFinancialRevalidationId = $stateParams.id;
  $scope.contractReportFinancialRevalidationStatus =
    $scope.contractReportFinancialRevalidationInfo.status;

  $scope.contractReportFinancialCSDBudgetItems = _.forEach(
    contractReportFinancialCSDBudgetItems,
    function(item) {
      csdNameCreator(item);
    }
  );

  $scope.correctBudgetItem = function(contractReportFinancialCSDBudgetItemId) {
    return scConfirm({
      confirmMessage:
        'contractReportFinancialRevalidations_' +
        'contractReportFinancialRevalidationsCSDsSearch_confirmCorrect',
      resource: 'ContractReportFinancialRevalidationCSD',
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
    return $state.go('root.contractReportFinancialRevalidations.view.csds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportFinancialRevalidationsCSDsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'scConfirm',
  'contractReportFinancialCSDBudgetItems'
];

ContractReportFinancialRevalidationsCSDsSearchCtrl.$resolve = {
  contractReportFinancialCSDBudgetItems: [
    '$stateParams',
    'contractReportFinancialRevalidationInfo',
    'ContractReportFinancialRevalidation',
    function(
      $stateParams,
      contractReportFinancialRevalidationInfo,
      ContractReportFinancialRevalidation
    ) {
      return ContractReportFinancialRevalidation.getFinancialCSDBudgetItems({
        id: $stateParams.id,
        contractReportId: contractReportFinancialRevalidationInfo.contractReportId,
        csd: $stateParams.csd,
        company: $stateParams.company
      }).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsCSDsSearchCtrl };
