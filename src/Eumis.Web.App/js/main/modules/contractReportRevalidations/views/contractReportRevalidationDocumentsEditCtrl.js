function ContractReportRevalidationDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportRevalidationDocument,
  document
) {
  $scope.editMode = null;
  $scope.document = document;
  $scope.status = $scope.contractReportRevalidationInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editDocument.$validate().then(function() {
      if ($scope.editDocument.$valid) {
        return ContractReportRevalidationDocument.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.document
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ContractReportRevalidationDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportRevalidations.view.docs.search');
      }
    });
  };
}

ContractReportRevalidationDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportRevalidationDocument',
  'document'
];

ContractReportRevalidationDocumentsEditCtrl.$resolve = {
  document: [
    'ContractReportRevalidationDocument',
    '$stateParams',
    function(ContractReportRevalidationDocument, $stateParams) {
      return ContractReportRevalidationDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportRevalidationDocumentsEditCtrl };
