function ProcedurePrognosesNewCtrl($scope, $state, scConfirm, prognosis) {
  $scope.prognosis = prognosis;

  $scope.save = function() {
    return $scope.procedurePrognosisNewForm.$validate().then(function() {
      if ($scope.procedurePrognosisNewForm.$valid) {
        return scConfirm({
          resource: 'ProcedurePrognosis',
          validationAction: 'canCreate',
          action: 'save',
          data: prognosis
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.procedurePrognoses.view', {
              id: result.result.prognosisId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedurePrognoses.search');
  };
}

ProcedurePrognosesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'prognosis'];

ProcedurePrognosesNewCtrl.$resolve = {
  prognosis: [
    'ProcedurePrognosis',
    function(ProcedurePrognosis) {
      return ProcedurePrognosis.newProcedurePrognosis().$promise;
    }
  ]
};

export { ProcedurePrognosesNewCtrl };
