import _ from 'lodash';

function EvalSessionsViewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  evalSessionInfo,
  canViewSessionData
) {
  $scope.evalSessionInfo = evalSessionInfo;
  $scope.infoText = $interpolate(l10n.get('evalSessions_viewEvalSession_info'))({
    status: evalSessionInfo.evalSessionStatus,
    number: evalSessionInfo.sessionNum,
    procedure: evalSessionInfo.procedureName
  });

  $scope.tabList = {
    evalSessions_tabs_evalSessionData: 'root.evalSessions.view.edit',
    evalSessions_tabs_evalSessionUsers: 'root.evalSessions.view.users',
    evalSessions_tabs_evalSessionProjects: 'root.evalSessions.view.projects'
  };

  if (canViewSessionData) {
    _.assign($scope.tabList, {
      evalSessions_tabs_evalSessionSheets: 'root.evalSessions.view.sheets',
      evalSessions_tabs_evalSessionStandpoints: 'root.evalSessions.view.standpoints',
      evalSessions_tabs_evalSessionDistributions: 'root.evalSessions.view.distributions',
      evalSessions_tabs_evalSessionStandings: 'root.evalSessions.view.standings',
      evalSessions_tabs_evalSessionCommunication: 'root.evalSessions.view.communication',
      evalSessions_tabs_evalSessionResults: 'root.evalSessions.view.result',
      evalSessions_tabs_evalSessionDocuments: 'root.evalSessions.view.allDocs'
    });
  }
}

EvalSessionsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'evalSessionInfo',
  'canViewSessionData'
];

EvalSessionsViewCtrl.$resolve = {
  evalSessionInfo: [
    'EvalSession',
    '$stateParams',
    function(EvalSession, $stateParams) {
      return EvalSession.getInfo({ id: $stateParams.id }).$promise;
    }
  ],
  canViewSessionData: [
    '$http',
    '$stateParams',
    function($http, $stateParams) {
      return $http({
        method: 'GET',
        url: 'api/authorizer/cando',
        params: {
          action: 'EvalSessionActions.ViewSessionData',
          id: $stateParams.id
        }
      }).then(function(response) {
        return response.data;
      });
    }
  ]
};

export { EvalSessionsViewCtrl };
