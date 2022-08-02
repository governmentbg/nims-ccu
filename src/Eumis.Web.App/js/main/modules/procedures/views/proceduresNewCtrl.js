function ProceduresNewCtrl($q, $scope, $state, $stateParams, Procedure, newProcedure, scMessage) {
  $scope.newProcedure = newProcedure;

  $scope.save = function() {
    return $scope.newProcedureForm.$validate().then(function() {
      if ($scope.newProcedureForm.$valid) {
        return Procedure.save($scope.newProcedure).$promise.then(
          function(result) {
            return $state.go('root.procedures.view.edit', {
              id: result.procedureId
            });
          },
          function(err) {
            if (err.status === 409) {
              //CONFLICT: the number has been used
              return Procedure.newProcedure({
                programmeId: $stateParams.programmeId,
                programmePriorityId: $stateParams.programmePriorityId
              })
                .$promise.then(function(newProcedure) {
                  $scope.newProcedure.number = newProcedure.number;
                  $scope.newProcedure.procedure.code = newProcedure.procedure.code;
                })
                .then(function() {
                  return scMessage('procedures_procedureShare_new_msg_duplicate_number', [
                    {
                      name: 'OK',
                      l10nLabel: 'scaffolding.scMessage.okButton',
                      type: 'btn-primary',
                      icon: 'glyphicon-ok'
                    }
                  ]);
                });
            }

            return $q.reject(err);
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.procedures.tree');
  };
}

ProceduresNewCtrl.$inject = [
  '$q',
  '$scope',
  '$state',
  '$stateParams',
  'Procedure',
  'newProcedure',
  'scMessage'
];

ProceduresNewCtrl.$resolve = {
  newProcedure: [
    '$stateParams',
    'Procedure',
    function($stateParams, Procedure) {
      return Procedure.newProcedure({
        programmeId: $stateParams.programmeId,
        programmePriorityId: $stateParams.programmePriorityId
      }).$promise;
    }
  ]
};

export { ProceduresNewCtrl };
