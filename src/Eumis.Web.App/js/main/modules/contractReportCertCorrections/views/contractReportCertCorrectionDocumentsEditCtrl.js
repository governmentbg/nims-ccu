function ContractReportCertCorrectionDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  ContractReportCertCorrectionDocument,
  document
) {
  $scope.editMode = null;
  $scope.document = document;
  $scope.status = $scope.contractReportCertCorrectionInfo.status;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editDocument.$validate().then(function() {
      if ($scope.editDocument.$valid) {
        return ContractReportCertCorrectionDocument.update(
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
      resource: 'ContractReportCertCorrectionDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.document.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.contractReportCertCorrections.view.docs.search');
      }
    });
  };
}

ContractReportCertCorrectionDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'ContractReportCertCorrectionDocument',
  'document'
];

ContractReportCertCorrectionDocumentsEditCtrl.$resolve = {
  document: [
    'ContractReportCertCorrectionDocument',
    '$stateParams',
    function(ContractReportCertCorrectionDocument, $stateParams) {
      return ContractReportCertCorrectionDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportCertCorrectionDocumentsEditCtrl };
