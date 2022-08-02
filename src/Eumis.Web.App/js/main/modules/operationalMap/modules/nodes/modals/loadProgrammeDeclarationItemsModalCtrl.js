function LoadProgrammeDeclarationItemsModalCtrl(
  $scope,
  $state,
  $uibModalInstance,
  scModalParams,
  ProgrammeDeclarationItem
) {
  $scope.programmeId = scModalParams.programmeId;

  $scope.ok = function() {
    return $scope.loadProgrammeDeclarationItemsModalForm.$validate().then(function() {
      if ($scope.loadProgrammeDeclarationItemsModalForm.$valid) {
        return ProgrammeDeclarationItem.loadItems(
          {
            id: scModalParams.programmeId,
            ind: scModalParams.programmeDeclarationId
          },
          $scope.file
        ).$promise.then(function(result) {
          $scope.errors = result.errors;

          if (result.errors.length === 0) {
            $state.partialReload();
            return $uibModalInstance.dismiss('cancel');
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

LoadProgrammeDeclarationItemsModalCtrl.$inject = [
  '$scope',
  '$state',
  '$uibModalInstance',
  'scModalParams',
  'ProgrammeDeclarationItem'
];

export { LoadProgrammeDeclarationItemsModalCtrl };
