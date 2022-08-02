import _ from 'lodash';

function EvalSessionReportsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  EvalSessionReport,
  evalSessionReport
) {
  $scope.evalSessionId = $stateParams.id;
  $scope.reportId = $stateParams.ind;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.isSessionEndedByLAG = $scope.evalSessionInfo.evalSessionStatusName === 'endedByLAG';
  $scope.evalSessionReport = evalSessionReport;

  $scope.showPartners = _.some(evalSessionReport.projects, function(project) {
    return project.partners.length !== 0;
  });
  $scope.showAdminAdmissResult = _.some(evalSessionReport.projects, function(project) {
    return project.hasAdminAdmiss;
  });
  $scope.showTechFinanceResult = _.some(evalSessionReport.projects, function(project) {
    return project.hasTechFinance;
  });
  $scope.showComplexResult = _.some(evalSessionReport.projects, function(project) {
    return project.hasComplex;
  });

  $scope.deleteReport = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionReport_deleteConfirm',
      noteLabel: 'evalSessions_editEvalSessionReport_deleteMessage',
      resource: 'EvalSessionReport',
      validationAction: 'canCancel',
      action: 'cancelReport',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionReport.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionReportsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  'EvalSessionReport',
  'evalSessionReport'
];

EvalSessionReportsEditCtrl.$resolve = {
  evalSessionReport: [
    '$stateParams',
    'EvalSessionReport',
    function($stateParams, EvalSessionReport) {
      return EvalSessionReport.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { EvalSessionReportsEditCtrl };
