function ChooseProcedureModalCtrl($scope, $state, $uibModalInstance, scModalParams, Procedure) {
  $scope.programmeId = scModalParams.programmeId;
  $scope.programmePriorityId = scModalParams.programmePriorityId;

  $scope.ok = function() {
    return $scope.chooseProcedureForm.$validate().then(function() {
      if ($scope.chooseProcedureForm.$valid) {
        return Procedure.copyProcedure({
          id: $scope.chooseProcedureForm.procedureId.$modelValue
        }).$promise.then(function(result) {
          $state.go('root.procedures.view.edit', {
            id: result.procedureId
          });
          return $uibModalInstance.dismiss();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseProcedureModalCtrl.$inject = [
  '$scope',
  '$state',
  '$uibModalInstance',
  'scModalParams',
  'Procedure'
];

export { ChooseProcedureModalCtrl };
