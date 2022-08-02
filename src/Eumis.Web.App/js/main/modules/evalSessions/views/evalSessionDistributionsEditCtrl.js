function EvalSessionDistributionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  l10n,
  scConfirm,
  EvalSessionDistribution,
  evalSessionDistribution
) {
  $scope.evalSessionDistribution = evalSessionDistribution;
  $scope.isSessionActive = $scope.evalSessionInfo.evalSessionStatusName === 'active';
  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');

  $scope.refuseDistribution = function() {
    return scConfirm({
      confirmMessage: 'evalSessions_editEvalSessionDistribution_refuseConfirm',
      noteLabel: 'evalSessions_editEvalSessionDistribution_refuseMessage',
      resource: 'EvalSessionDistribution',
      validationAction: 'canRefuse',
      action: 'refuse',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.evalSessionDistribution.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EvalSessionDistributionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'l10n',
  'scConfirm',
  'EvalSessionDistribution',
  'evalSessionDistribution'
];

EvalSessionDistributionsEditCtrl.$resolve = {
  evalSessionDistribution: [
    '$stateParams',
    'EvalSessionDistribution',
    function($stateParams, EvalSessionDistribution) {
      return EvalSessionDistribution.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { EvalSessionDistributionsEditCtrl };
