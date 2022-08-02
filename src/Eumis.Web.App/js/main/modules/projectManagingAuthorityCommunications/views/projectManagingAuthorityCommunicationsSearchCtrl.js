import _ from 'lodash';

function ProjectManagingAuthorityCommunicationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  projectManagingAuthorityCommunications
) {
  $scope.projectManagingAuthorityCommunications = projectManagingAuthorityCommunications;

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

  $scope.search = function() {
    return $state.go('root.projectCommunications.search', {
      programmeId: $scope.filters.programmeId,
      programmePriorityId: $scope.filters.programmePriorityId,
      procedureId: $scope.filters.procedureId,
      fromDate: $scope.filters.fromDate,
      toDate: $scope.filters.toDate,
      source: $scope.filters.source
    });
  };

  $scope.projectCommunicationsExportUrl = $interpolate(
    'api/projectManagingAuthorityCommunications/excelExport?' +
      'programmeId={{programmeId}}&programmePriorityId={{programmePriorityId}}&procedureId={{procedureId}}&fromDate={{fromDate}}&toDate={{toDate}}&source={{source}}'
  )({
    programmeId: $scope.filters.programmeId,
    programmePriorityId: $scope.filters.programmePriorityId,
    procedureId: $scope.filters.procedureId,
    fromDate: $scope.filters.fromDate,
    toDate: $scope.filters.toDate,
    source: $scope.filters.source
  });
}

ProjectManagingAuthorityCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'projectManagingAuthorityCommunications'
];

ProjectManagingAuthorityCommunicationsSearchCtrl.$resolve = {
  projectManagingAuthorityCommunications: [
    '$stateParams',
    'ProjectManagingAuthorityCommunication',
    function($stateParams, ProjectManagingAuthorityCommunication) {
      return ProjectManagingAuthorityCommunication.getCommunications($stateParams).$promise;
    }
  ]
};

export { ProjectManagingAuthorityCommunicationsSearchCtrl };
