function ProcedureQuestionsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureQuestion,
  procedureQuestion,
  scConfirm
) {
  $scope.procedureQuestion = procedureQuestion;
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.deletable = $stateParams.del === 'true';

  $scope.ok = function() {
    return $state.go('root.procedures.view.allDocs.search');
  };

  $scope.deleteQuestion = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureQuestion',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procedureQuestion.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.allDocs.search');
      }
    });
  };
}

ProcedureQuestionsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureQuestion',
  'procedureQuestion',
  'scConfirm'
];

ProcedureQuestionsEditCtrl.$resolve = {
  procedureQuestion: [
    'ProcedureQuestion',
    '$stateParams',
    function(ProcedureQuestion, $stateParams) {
      return ProcedureQuestion.get({ id: $stateParams.id, ind: $stateParams.ind }).$promise;
    }
  ]
};

export { ProcedureQuestionsEditCtrl };
