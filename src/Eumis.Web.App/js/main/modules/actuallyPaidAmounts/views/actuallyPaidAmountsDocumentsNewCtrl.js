function ActuallyPaidAmountsDocumentsNewCtrl(
  $scope,
  $state,
  $stateParams,
  ActuallyPaidAmountDocument,
  document
) {
  $scope.document = document;

  $scope.save = function() {
    return $scope.newPaidAmountDocumentForm.$validate().then(function() {
      if ($scope.newPaidAmountDocumentForm.$valid) {
        return ActuallyPaidAmountDocument.save(
          { id: $stateParams.id },
          $scope.document
        ).$promise.then(function() {
          return $state.go('root.actuallyPaidAmounts.view.documents.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.actuallyPaidAmounts.view.documents.search');
  };
}

ActuallyPaidAmountsDocumentsNewCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ActuallyPaidAmountDocument',
  'document'
];

ActuallyPaidAmountsDocumentsNewCtrl.$resolve = {
  document: [
    'ActuallyPaidAmountDocument',
    '$stateParams',
    function(ActuallyPaidAmountDocument, $stateParams) {
      return ActuallyPaidAmountDocument.newDocument({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ActuallyPaidAmountsDocumentsNewCtrl };
