function ProcedureQuestionsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureQuestion,
  procedureQuestion
) {
  $scope.procedureQuestion = procedureQuestion;

  $scope.save = function() {
    return $scope.newProcedureQuestionForm.$validate().then(function() {
      if ($scope.newProcedureQuestionForm.$valid) {
        return ProcedureQuestion.save(
          { id: $stateParams.id },
          $scope.procedureQuestion
        ).$promise.then(function() {
          return $state.go('root.procedures.view.allDocs.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.allDocs.search');
  };
}

ProcedureQuestionsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureQuestion',
  'procedureQuestion'
];

ProcedureQuestionsNewCtrl.$resolve = {
  procedureQuestion: [
    'ProcedureQuestion',
    '$stateParams',
    function(ProcedureQuestion, $stateParams) {
      return ProcedureQuestion.newQuestion({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureQuestionsNewCtrl };
