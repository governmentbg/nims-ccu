function ProcedureSpecFieldsNewCtrl(
  $scope,
  $state,
  $stateParams,
  Procedure,
  ProcedureSpecField,
  newProcedureSpecField
) {
  $scope.newProcedureSpecField = newProcedureSpecField;

  $scope.save = function() {
    return $scope.newProcedureSpecFieldForm.$validate().then(function() {
      if ($scope.newProcedureSpecFieldForm.$valid) {
        return ProcedureSpecField.save(
          { id: $stateParams.id },
          $scope.newProcedureSpecField
        ).$promise.then(function() {
          return $state.go('root.procedures.view.procedureSpecFields.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.view.procedureSpecFields.search');
  };
}

ProcedureSpecFieldsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'Procedure',
  'ProcedureSpecField',
  'newProcedureSpecField'
];

ProcedureSpecFieldsNewCtrl.$resolve = {
  newProcedureSpecField: [
    '$stateParams',
    'ProcedureSpecField',
    function($stateParams, ProcedureSpecField) {
      return ProcedureSpecField.newProcedureSpecField({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProcedureSpecFieldsNewCtrl };
