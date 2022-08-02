import _ from 'lodash';

function ContractReportsFinancialCSDBudgetItemsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  $interpolate,
  contractReportFinancialCSDBudgetItems
) {
  $scope.contractReportId = $stateParams.id;
  $scope.budgetItemsExportUrl = $interpolate(
    'api/contractReports/{{id}}/' +
      'financialCSDBudgetItems/excelExport?csd={{csd}}&company={{company}}'
  )({
    id: $stateParams.id,
    csd: $stateParams.csd,
    company: $stateParams.company
  });
  $scope.contractReportStatus = $scope.contractReportInfo.status;

  $scope.contractReportFinancialCSDBudgetItems = _.forEach(
    contractReportFinancialCSDBudgetItems,
    function(item) {
      csdNameCreator(item);
    }
  );

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
    return $state.go('root.contractReportChecks.view.csds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportsFinancialCSDBudgetItemsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  '$interpolate',
  'contractReportFinancialCSDBudgetItems'
];

ContractReportsFinancialCSDBudgetItemsSearchCtrl.$resolve = {
  contractReportFinancialCSDBudgetItems: [
    '$stateParams',
    'ContractReportFinancialCSDBudgetItem',
    function($stateParams, ContractReportFinancialCSDBudgetItem) {
      return ContractReportFinancialCSDBudgetItem.query($stateParams).$promise;
    }
  ]
};

export { ContractReportsFinancialCSDBudgetItemsSearchCtrl };
