import _ from 'lodash';

function ContractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  scConfirm,
  contractReportFinancialRevalidationCSDs
) {
  $scope.contractReportFinancialRevalidationId = $stateParams.id;

  $scope.contractReportFinancialRevalidationCSDs = _.forEach(
    contractReportFinancialRevalidationCSDs,
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
    return $state.go('root.contractReportFinancialRevalidations.view.revalidatedCsds.search', {
      csd: $scope.filters.csd,
      company: $scope.filters.company
    });
  };
}

ContractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'scConfirm',
  'contractReportFinancialRevalidationCSDs'
];

ContractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl.$resolve = {
  contractReportFinancialRevalidationCSDs: [
    '$stateParams',
    'ContractReportFinancialRevalidationCSD',
    function($stateParams, ContractReportFinancialRevalidationCSD) {
      return ContractReportFinancialRevalidationCSD.query($stateParams).$promise;
    }
  ]
};

export { ContractReportFinancialRevalidationsRevalidatedCSDsSearchCtrl };
