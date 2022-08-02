function ProgrammeDocumentsNewCtrl($scope, $state, $stateParams, ProgrammeDocument, document) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newProgrammeDocumentForm.$validate().then(function() {
      if ($scope.newProgrammeDocumentForm.$valid) {
        return ProgrammeDocument.save({ id: $stateParams.id }, $scope.document).$promise.then(
          function() {
            return $state.go('root.map.programmes.view.documents.search');
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.programmes.view.documents.search');
  };
}

ProgrammeDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProgrammeDocument',
  'document'
];

ProgrammeDocumentsNewCtrl.$resolve = {
  document: [
    'ProgrammeDocument',
    '$stateParams',
    function(ProgrammeDocument, $stateParams) {
      return ProgrammeDocument.newDocument({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammeDocumentsNewCtrl };
