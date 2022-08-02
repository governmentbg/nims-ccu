import _ from 'lodash';

function ContractCommunicationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  contractCommunications
) {
  $scope.contractCommunications = contractCommunications;

  $scope.filters = {
    programmeId: null,
    programmePriorityId: null,
    procedureId: null,
    fromDate: null,
    toDate: null,
    source: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.contractCommunicationsExportUrl = $interpolate(
    'api/contracts/contractCommunications/excelExport?' +
      'programmeId={{programmeId}}&programmePriorityId={{programmePriorityId}}&procedureId={{procedureId}}&fromDate={{fromDate}}&toDate={{toDate}}&source={{source}}'
  )({
    programmeId: $scope.filters.programmeId,
    programmePriorityId: $scope.filters.programmePriorityId,
    procedureId: $scope.filters.procedureId,
    fromDate: $scope.filters.fromDate,
    toDate: $scope.filters.toDate,
    source: $scope.filters.source
  });

  $scope.search = function() {
    return $scope.contractCommunicationForm.$validate().then(function() {
      if ($scope.contractCommunicationForm.$valid) {
        return $state.go('root.contractCommunications.search', {
          programmeId: $scope.filters.programmeId,
          programmePriorityId: $scope.filters.programmePriorityId,
          procedureId: $scope.filters.procedureId,
          fromDate: $scope.filters.fromDate,
          toDate: $scope.filters.toDate,
          source: $scope.filters.source
        });
      }
    });
  };
}

ContractCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'contractCommunications'
];

ContractCommunicationsSearchCtrl.$resolve = {
  contractCommunications: [
    '$stateParams',
    'AdminAuthorityCommunication',
    function($stateParams, AdminAuthorityCommunication) {
      return AdminAuthorityCommunication.getContracts($stateParams).$promise;
    }
  ]
};

export { ContractCommunicationsSearchCtrl };
