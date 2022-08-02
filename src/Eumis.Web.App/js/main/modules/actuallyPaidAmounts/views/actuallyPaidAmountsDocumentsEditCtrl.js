function ActuallyPaidAmountsDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ActuallyPaidAmountDocument,
  document,
  scConfirm
) {
  $scope.document = document;
  $scope.editMode = null;
  $scope.status = $scope.paidAmountInfo.status;
  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editPaidAmountDocumentForm.$validate().then(function() {
      if ($scope.editPaidAmountDocumentForm.$valid) {
        return ActuallyPaidAmountDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.document
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.deleteDocument = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ActuallyPaidAmountDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.actuallyPaidAmounts.view.documents.search');
      }
    });
  };
}

ActuallyPaidAmountsDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ActuallyPaidAmountDocument',
  'document',
  'scConfirm'
];

ActuallyPaidAmountsDocumentsEditCtrl.$resolve = {
  document: [
    'ActuallyPaidAmountDocument',
    '$stateParams',
    function(ActuallyPaidAmountDocument, $stateParams) {
      return ActuallyPaidAmountDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise;
    }
  ]
};

export { ActuallyPaidAmountsDocumentsEditCtrl };
