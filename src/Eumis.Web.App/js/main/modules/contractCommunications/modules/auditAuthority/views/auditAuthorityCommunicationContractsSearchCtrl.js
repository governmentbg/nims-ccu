import _ from 'lodash';

function AuditAuthorityCommunicationContractsSearchCtrl($scope, $state, $stateParams, contracts) {
  $scope.filters = {
    procedureId: null,
    programmePriorityId: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.contracts = contracts;

  $scope.search = function() {
    return $state.go('root.auditAuthorityCommunications.search', {
      procedureId: $scope.filters.procedureId,
      programmePriorityId: $scope.filters.programmePriorityId
    });
  };
}

AuditAuthorityCommunicationContractsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contracts'
];

AuditAuthorityCommunicationContractsSearchCtrl.$resolve = {
  contracts: [
    '$stateParams',
    'AuditAuthorityCommunication',
    function($stateParams, AuditAuthorityCommunication) {
      return AuditAuthorityCommunication.getContracts($stateParams).$promise;
    }
  ]
};

export { AuditAuthorityCommunicationContractsSearchCtrl };
