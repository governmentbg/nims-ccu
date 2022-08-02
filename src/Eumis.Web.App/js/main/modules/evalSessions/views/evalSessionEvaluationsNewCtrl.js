import _ from 'lodash';

function EvalSessionEvaluationsNewCtrl(
  $scope,
  $state,
  $stateParams,
  scMessage,
  EvalSessionEvaluation,
  evalSessionEvaluation
) {
  $scope.evalSessionEvaluation = evalSessionEvaluation;

  $scope.cancel = function() {
    return $state.go('root.evalSessions.view.projects.search');
  };

  $scope.save = function() {
    return $scope.evalSessionEvaluationNewForm.$validate().then(function() {
      if ($scope.evalSessionEvaluationNewForm.$valid) {
        var draftSheets = _.filter($scope.evalSessionEvaluation.sheets, function(s) {
          return s.status === 'draft';
        });

        if (draftSheets.length !== 0) {
          return scMessage('evalSessions_evalSessionEvaluationForm_cannotSave', [
            {
              name: 'OK',
              l10nLabel: 'scaffolding.scMessage.okButton',
              type: 'btn-primary',
              icon: 'glyphicon-ok'
            }
          ]);
        } else {
          return EvalSessionEvaluation.save(
            { id: $stateParams.id },
            $scope.evalSessionEvaluation
          ).$promise.then(function() {
            return $state.go('root.evalSessions.view.projects.view', {
              id: $stateParams.id,
              ind: $scope.evalSessionEvaluation.projectId
            });
          });
        }
      }
    });
  };
}

EvalSessionEvaluationsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'EvalSessionEvaluation',
  'evalSessionEvaluation'
];

EvalSessionEvaluationsNewCtrl.$resolve = {
  evalSessionEvaluation: [
    '$state',
    '$stateParams',
    'EvalSessionEvaluation',
    function($state, $stateParams, EvalSessionEvaluation) {
      if ($stateParams.pId && $stateParams.t) {
        return EvalSessionEvaluation.newEvalSessionEvaluation({
          id: $stateParams.id,
          projectId: $stateParams.pId,
          evalTableType: $stateParams.t
        }).$promise;
      } else {
        return $state.go('root.evalSessions.view.projects.search');
      }
    }
  ]
};

export { EvalSessionEvaluationsNewCtrl };
