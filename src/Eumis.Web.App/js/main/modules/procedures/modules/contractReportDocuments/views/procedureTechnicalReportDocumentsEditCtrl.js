function ProcedureTechnicalReportDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureTechnicalReportDocument,
  technicalReportDocument,
  procedureInfo,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.technicalReportDocument = technicalReportDocument;
  $scope.editMode = null;
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureTechnicalReportDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.technicalReportDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.activate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmActivate',
      resource: 'ProcedureTechnicalReportDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.technicalReportDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProcedureTechnicalReportDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureTechnicalReportDocumentsForm.$valid) {
        return ProcedureTechnicalReportDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.technicalReportDocument
        ).$promise.then(function() {
          return $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.delete = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProcedureTechnicalReportDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.technicalReportDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.reportDocs.search');
      }
    });
  };
}

ProcedureTechnicalReportDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureTechnicalReportDocument',
  'technicalReportDocument',
  'procedureInfo',
  'scConfirm'
];

ProcedureTechnicalReportDocumentsEditCtrl.$resolve = {
  technicalReportDocument: [
    'ProcedureTechnicalReportDocument',
    '$stateParams',
    function(ProcedureTechnicalReportDocument, $stateParams) {
      return ProcedureTechnicalReportDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise;
    }
  ],
  procedureInfo: [
    'Procedure',
    '$stateParams',
    function(Procedure, $stateParams) {
      return Procedure.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureTechnicalReportDocumentsEditCtrl };
