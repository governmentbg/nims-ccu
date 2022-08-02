function ProcedureSpecFieldsSearchCtrl($scope, $stateParams, procedureSpecFields) {
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureSpecFields = procedureSpecFields;
  $scope.procedureId = $stateParams.id;
}

ProcedureSpecFieldsSearchCtrl.$inject = ['$scope', '$stateParams', 'procedureSpecFields'];

ProcedureSpecFieldsSearchCtrl.$resolve = {
  procedureSpecFields: [
    '$stateParams',
    'ProcedureSpecField',
    function($stateParams, ProcedureSpecField) {
      return ProcedureSpecField.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureSpecFieldsSearchCtrl };
