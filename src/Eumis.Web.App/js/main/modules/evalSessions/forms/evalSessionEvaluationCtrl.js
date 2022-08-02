import _ from 'lodash';

function EvalSessionEvaluationCtrl($scope, l10n, structuredDocument) {
  $scope.documentName = l10n.get('evalSessions_evalSessionEvaluationForm_document');

  _.forEach($scope.model.sheets, function(sheet) {
    sheet.url = structuredDocument.getUrl('evalSessionSheet', 'view', sheet.xmlGid);
  });

  $scope.endedSheetsCount = _.filter($scope.model.sheets, function(s) {
    return s.status === 'ended';
  }).length;

  $scope.canceledSheetsCount = _.filter($scope.model.sheets, function(s) {
    return s.status === 'canceled';
  }).length;

  $scope.pausedSheetsCount = _.filter($scope.model.sheets, function(s) {
    return s.status === 'paused';
  }).length;

  $scope.calculationTypeChanged = function() {
    if ($scope.model.calculationType === 'automatic') {
      $scope.model.evalIsPassed = $scope.model.originalEvalIsPassed;
      $scope.model.evalPoints = $scope.model.originalEvalPoints;
    }
  };
}

EvalSessionEvaluationCtrl.$inject = ['$scope', 'l10n', 'structuredDocument'];

export { EvalSessionEvaluationCtrl };
