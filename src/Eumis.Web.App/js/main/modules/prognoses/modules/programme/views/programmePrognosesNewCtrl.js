function ProgrammePrognosesNewCtrl($scope, $state, scConfirm, prognosis) {
  $scope.prognosis = prognosis;

  $scope.save = function() {
    return $scope.programmePrognosisNewForm.$validate().then(function() {
      if ($scope.programmePrognosisNewForm.$valid) {
        return scConfirm({
          resource: 'ProgrammePrognosis',
          validationAction: 'canCreate',
          action: 'save',
          data: prognosis
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.programmePrognoses.view', {
              id: result.result.prognosisId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.programmePrognoses.search');
  };
}

ProgrammePrognosesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'prognosis'];

ProgrammePrognosesNewCtrl.$resolve = {
  prognosis: [
    'ProgrammePrognosis',
    function(ProgrammePrognosis) {
      return ProgrammePrognosis.newProgrammePrognosis().$promise;
    }
  ]
};

export { ProgrammePrognosesNewCtrl };
