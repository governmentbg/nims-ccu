import * as angular from 'angular';

function ProgrammeDeclarationItemsNewCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProgrammeDeclarationItem,
  declarationItem
) {
  $scope.declarationItem = declarationItem;

  $scope.save = function() {
    return $scope.newProgrammeDeclarationItemForm.$validate().then(function() {
      if ($scope.newProgrammeDeclarationItemForm.$valid) {
        return ProgrammeDeclarationItem.canAdd(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.declarationItem
        ).$promise.then(function(result) {
          if (!result.errors.length) {
            return ProgrammeDeclarationItem.save(
              { id: $stateParams.id, ind: $stateParams.ind },
              $scope.declarationItem
            ).$promise.then(function() {
              return $state.go('root.map.programmes.view.declarations.edit');
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
    return $state.go('root.map.programmes.view.declarations.edit');
  };
}

ProgrammeDeclarationItemsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProgrammeDeclarationItem',
  'declarationItem'
];

ProgrammeDeclarationItemsNewCtrl.$resolve = {
  declarationItem: [
    'ProgrammeDeclarationItem',
    '$stateParams',
    function(ProgrammeDeclarationItem, $stateParams) {
      return ProgrammeDeclarationItem.newDeclarationItem({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProgrammeDeclarationItemsNewCtrl };
