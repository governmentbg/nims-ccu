import _ from 'lodash';

function MyEvalSessionsViewCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  $interpolate,
  evalSessionInfo,
  canViewSessionSheets,
  canViewSessionStandpoints
) {
  $scope.evalSessionInfo = evalSessionInfo;
  $scope.infoText = $interpolate(l10n.get('myEvalSessions_viewEvalSession_info'))({
    status: evalSessionInfo.evalSessionStatus,
    number: evalSessionInfo.sessionNum,
    procedure: evalSessionInfo.procedureName
  });

  $scope.tabList = {
    myEvalSessions_tabs_evalSessionData: 'root.evalSessions.my.view.edit'
  };

  if (canViewSessionSheets) {
    _.assign($scope.tabList, {
      myEvalSessions_tabs_evalSessionSheets: 'root.evalSessions.my.view.sheets'
    });
  }

  if (canViewSessionStandpoints) {
    _.assign($scope.tabList, {
      myEvalSessions_tabs_evalSessionStandpoints: 'root.evalSessions.my.view.standpoints'
    });
  }
}

MyEvalSessionsViewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  '$interpolate',
  'evalSessionInfo',
  'canViewSessionSheets',
  'canViewSessionStandpoints'
];

MyEvalSessionsViewCtrl.$resolve = {
  evalSessionInfo: [
    'MyEvalSession',
    '$stateParams',
    function(MyEvalSession, $stateParams) {
      return MyEvalSession.getInfo({ id: $stateParams.id }).$promise;
    }
  ],
  canViewSessionSheets: [
    '$http',
    '$stateParams',
    function($http, $stateParams) {
      return $http({
        method: 'GET',
        url: 'api/authorizer/cando',
        params: {
          action: 'MyEvalSession.ViewSessionSheets',
          id: $stateParams.id
        }
      }).then(function(response) {
        return response.data;
      });
    }
  ],
  canViewSessionStandpoints: [
    '$http',
    '$stateParams',
    function($http, $stateParams) {
      return $http({
        method: 'GET',
        url: 'api/authorizer/cando',
        params: {
          action: 'MyEvalSession.ViewSessionStandpoints',
          id: $stateParams.id
        }
      }).then(function(response) {
        return response.data;
      });
    }
  ]
};

export { MyEvalSessionsViewCtrl };
