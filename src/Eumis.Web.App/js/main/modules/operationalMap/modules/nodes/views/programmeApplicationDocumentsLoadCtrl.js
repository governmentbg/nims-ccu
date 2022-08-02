import angular from 'angular';

function ProgrammeApplicationDocumentsLoadCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProgrammeApplicationDocument
) {
  $scope.continue = function() {
    return $scope.loadProgrammeApplicationDocumentsForm.$validate().then(function() {
      if ($scope.loadProgrammeApplicationDocumentsForm.$valid) {
        ProgrammeApplicationDocument.canLoadDocuments(
          { id: $stateParams.id },
          $scope.model.file
        ).$promise.then(function(res) {
          if (res.errors.length !== 0) {
            var modalInstance = scModal.open('validationErrorsModal', {
              errors: res.errors
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          } else {
            return ProgrammeApplicationDocument.loadDocuments(
              { id: $stateParams.id },
              $scope.model.file
            ).$promise.then(function() {
              return $state.go('root.map.programmes.view.documents.search');
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.programmes.view.documents.search');
  };
}

ProgrammeApplicationDocumentsLoadCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProgrammeApplicationDocument'
];

export { ProgrammeApplicationDocumentsLoadCtrl };
