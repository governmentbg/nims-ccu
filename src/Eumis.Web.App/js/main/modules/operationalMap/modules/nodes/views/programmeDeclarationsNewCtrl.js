import * as angular from 'angular';

function ProgrammeDeclarationsNewCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProgrammeDeclaration,
  declaration
) {
  $scope.declaration = declaration;

  $scope.save = function() {
    return $scope.newProgrammeDeclarationForm.$validate().then(function() {
      if ($scope.newProgrammeDeclarationForm.$valid) {
        return ProgrammeDeclaration.canAdd(
          {
            id: $stateParams.id
          },
          $scope.declaration
        ).$promise.then(function(result) {
          if (!result.errors.length) {
            return ProgrammeDeclaration.save(
              { id: $stateParams.id },
              $scope.declaration
            ).$promise.then(function(result) {
              return $state.go('root.map.programmes.view.declarations.edit', {
                ind: result.programmeDeclarationId
              });
            });
          } else {
            const modalInstance = scModal.open('validationErrorsModal', {
              errors: result.errors
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.programmes.view.documents.search');
  };
}

ProgrammeDeclarationsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProgrammeDeclaration',
  'declaration'
];

ProgrammeDeclarationsNewCtrl.$resolve = {
  declaration: [
    'ProgrammeDeclaration',
    '$stateParams',
    function(ProgrammeDeclaration, $stateParams) {
      return ProgrammeDeclaration.newDeclaration({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammeDeclarationsNewCtrl };
