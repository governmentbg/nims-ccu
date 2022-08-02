function ContractReportCorrectionDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportCorrectionDocument,
  document
) {
  $scope.editMode = null;
  $scope.document = document;
  $scope.status = $scope.contractReportCorrectionInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editDocument.$validate().then(function() {
      if ($scope.editDocument.$valid) {
        return ContractReportCorrectionDocument.update(
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
      resource: 'ContractReportCorrectionDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportCorrections.view.docs.search');
      }
    });
  };
}

ContractReportCorrectionDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportCorrectionDocument',
  'document'
];

ContractReportCorrectionDocumentsEditCtrl.$resolve = {
  document: [
    'ContractReportCorrectionDocument',
    '$stateParams',
    function(ContractReportCorrectionDocument, $stateParams) {
      return ContractReportCorrectionDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportCorrectionDocumentsEditCtrl };
