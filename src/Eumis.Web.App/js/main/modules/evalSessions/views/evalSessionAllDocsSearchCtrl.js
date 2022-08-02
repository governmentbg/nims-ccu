import _ from 'lodash';

function EvalSessionAllDocsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  EvalSessionFile,
  evalSessionReports,
  evalSessionDocuments
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.isSessionEndedByLAG = $scope.evalSessionInfo.evalSessionStatusName === 'endedByLAG';
  $scope.evalSessionReports = evalSessionReports;
  $scope.evalSessionDocuments = evalSessionDocuments;

  $scope.evalSessionDocuments = _.map(evalSessionDocuments, function(item) {
    if (item.file) {
      item.file.url = EvalSessionFile.getUrl({ id: item.evalSessionId, fileKey: item.file.key });
    }
    return item;
  });

  $scope.canCreateReport = function() {
    return scConfirm({
      resource: 'EvalSessionReport',
      validationAction: 'canCreate',
      params: { id: $scope.evalSessionId }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.evalSessions.view.allDocs.reports.new');
      }
    });
  };
}

EvalSessionAllDocsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'EvalSessionFile',
  'evalSessionReports',
  'evalSessionDocuments'
];

EvalSessionAllDocsSearchCtrl.$resolve = {
  evalSessionReports: [
    '$stateParams',
    'EvalSessionReport',
    function($stateParams, EvalSessionReport) {
      return EvalSessionReport.query({ id: $stateParams.id }).$promise;
    }
  ],
  evalSessionDocuments: [
    '$stateParams',
    'EvalSessionDocument',
    function($stateParams, EvalSessionDocument) {
      return EvalSessionDocument.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { EvalSessionAllDocsSearchCtrl };
