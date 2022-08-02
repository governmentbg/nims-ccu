import angular from 'angular';

function ProgrammeApplicationDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  ProgrammeApplicationDocument,
  document
) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newProgrammeApplicationDocumentForm.$validate().then(function() {
      if ($scope.newProgrammeApplicationDocumentForm.$valid) {
        return ProgrammeApplicationDocument.canAdd(
          {
            id: $stateParams.id
          },
          {
            name: $scope.document.name
          }
        ).$promise.then(function(result) {
          if (!result.errors.length) {
            return ProgrammeApplicationDocument.save(
              { id: $stateParams.id },
              $scope.document
            ).$promise.then(function() {
              return $state.go('root.map.programmes.view.documents.search');
            });
          } else {
            var modalInstance = scModal.open('validationErrorsModal', {
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

ProgrammeApplicationDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'ProgrammeApplicationDocument',
  'document'
];

ProgrammeApplicationDocumentsNewCtrl.$resolve = {
  document: [
    'ProgrammeApplicationDocument',
    '$stateParams',
    function(ProgrammeApplicationDocument, $stateParams) {
      return ProgrammeApplicationDocument.newDocument({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammeApplicationDocumentsNewCtrl };
