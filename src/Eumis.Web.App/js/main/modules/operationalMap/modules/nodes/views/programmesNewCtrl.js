function ProgrammesNewCtrl($scope, $state, scConfirm, programme) {
  $scope.programme = programme;

  $scope.save = function() {
    return $scope.newProgrammeForm.$validate().then(function() {
      if ($scope.newProgrammeForm.$valid) {
        return scConfirm({
          resource: 'Programme',
          validationAction: 'canCreate',
          action: 'save',
          data: $scope.programme
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.map.programmes.view.edit', {
              id: result.result.programmeId
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.tree');
  };
}

ProgrammesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'programme'];

ProgrammesNewCtrl.$resolve = {
  programme: [
    'Programme',
    function(Programme) {
      return Programme.newProgamme().$promise;
    }
  ]
};

export { ProgrammesNewCtrl };
