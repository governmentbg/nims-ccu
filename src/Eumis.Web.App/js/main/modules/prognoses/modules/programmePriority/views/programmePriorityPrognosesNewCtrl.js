function ProgrammePriorityPrognosesNewCtrl($scope, $state, scConfirm, prognosis) {
  $scope.prognosis = prognosis;

  $scope.save = function() {
    return $scope.programmePriorityPrognosisNewForm.$validate().then(function() {
      if ($scope.programmePriorityPrognosisNewForm.$valid) {
        return scConfirm({
          resource: 'ProgrammePriorityPrognosis',
          validationAction: 'canCreate',
          action: 'save',
          data: prognosis
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.programmePriorityPrognoses.view', {
              id: result.result.prognosisId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.programmePriorityPrognoses.search');
  };
}

ProgrammePriorityPrognosesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'prognosis'];

ProgrammePriorityPrognosesNewCtrl.$resolve = {
  prognosis: [
    'ProgrammePriorityPrognosis',
    function(ProgrammePriorityPrognosis) {
      return ProgrammePriorityPrognosis.newProgrammePriorityPrognosis().$promise;
    }
  ]
};

export { ProgrammePriorityPrognosesNewCtrl };
