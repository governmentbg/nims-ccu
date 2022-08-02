import _ from 'lodash';

function CertAuthorityCommunicationContractsSearchCtrl($scope, $state, $stateParams, contracts) {
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
    return $state.go('root.certAuthorityCommunications.search', {
      procedureId: $scope.filters.procedureId,
      programmePriorityId: $scope.filters.programmePriorityId
    });
  };
}

CertAuthorityCommunicationContractsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'contracts'
];

CertAuthorityCommunicationContractsSearchCtrl.$resolve = {
  contracts: [
    '$stateParams',
    'CertAuthorityCommunication',
    function($stateParams, CertAuthorityCommunication) {
      return CertAuthorityCommunication.getContracts($stateParams).$promise;
    }
  ]
};

export { CertAuthorityCommunicationContractsSearchCtrl };
