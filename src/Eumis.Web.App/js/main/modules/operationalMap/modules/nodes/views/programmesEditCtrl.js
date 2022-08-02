function ProgrammesEditCtrl($scope, $state, $stateParams, scConfirm, programme) {
  $scope.programme = programme;
  $scope.editMode = null;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProgrammeDataForm.$validate().then(function() {
      if ($scope.editProgrammeDataForm.$valid) {
        return scConfirm({
          resource: 'Programme',
          validationAction: 'canUpdate',
          action: 'update',
          params: {
            id: $stateParams.id
          },
          data: $scope.programme
        }).then(function(result) {
          if (result.executed) {
            return $state.partialReload();
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.changeStatusToDraft = function() {
    return scConfirm({
      confirmMessage: 'programmes_editProgrammeData_draftConfirm',
      resource: 'Programme',
      action: 'changeStatusToDraft',
      params: {
        id: $stateParams.id,
        version: $scope.programme.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.changeStatusToEntered = function() {
    return scConfirm({
      confirmMessage: 'programmes_editProgrammeData_enterConfirm',
      resource: 'Programme',
      validationAction: 'canEnter',
      action: 'changeStatusToEntered',
      params: {
        id: $stateParams.id,
        version: $scope.programme.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Programme',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.programme.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.map.tree');
      }
    });
  };
}

ProgrammesEditCtrl.$inject = ['$scope', '$state', '$stateParams', 'scConfirm', 'programme'];

ProgrammesEditCtrl.$resolve = {
  programme: [
    'Programme',
    '$stateParams',
    function(Programme, $stateParams) {
      return Programme.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammesEditCtrl };
