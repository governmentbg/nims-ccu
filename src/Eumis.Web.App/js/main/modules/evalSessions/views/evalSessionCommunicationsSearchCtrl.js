import _ from 'lodash';

function EvalSessionCommunicationsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  evalSessionCommunications
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.exportUrl = $interpolate(
    'api/evalSessions/{{id}}/communicationsExcelExport?' +
      'projectId={{projectId}}&statusId={{statusId}}&questionDateFrom={{questionDateFrom}}' +
      '&questionDateTo={{questionDateTo}}'
  )($stateParams);
  $scope.evalSessionCommunications = evalSessionCommunications;

  $scope.filters = {
    projectId: null,
    statusId: null,
    questionDateFrom: null,
    questionDateTo: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.evalSessions.view.communication.search', {
      id: $stateParams.id,
      projectId: $scope.filters.projectId,
      statusId: $scope.filters.statusId,
      questionDateFrom: $scope.filters.questionDateFrom,
      questionDateTo: $scope.filters.questionDateTo
    });
  };
}

EvalSessionCommunicationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'evalSessionCommunications'
];

EvalSessionCommunicationsSearchCtrl.$resolve = {
  evalSessionCommunications: [
    '$stateParams',
    'ProjectCommunication',
    function($stateParams, ProjectCommunication) {
      return ProjectCommunication.getCommunicationsForSession($stateParams).$promise;
    }
  ]
};

export { EvalSessionCommunicationsSearchCtrl };
