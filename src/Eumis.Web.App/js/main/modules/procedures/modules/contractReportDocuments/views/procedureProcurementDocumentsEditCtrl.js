function ProcedureProcurementDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureProcurementDocument,
  procurementDocument,
  procedureInfo,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.procurementDocument = procurementDocument;
  $scope.editMode = null;
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureProcurementDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procurementDocument.version
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
      resource: 'ProcedureProcurementDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procurementDocument.version
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
    return $scope.editProcedureProcurementDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureProcurementDocumentsForm.$valid) {
        return ProcedureProcurementDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.procurementDocument
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
      resource: 'ProcedureProcurementDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.procurementDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.reportDocs.search');
      }
    });
  };
}

ProcedureProcurementDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureProcurementDocument',
  'procurementDocument',
  'procedureInfo',
  'scConfirm'
];

ProcedureProcurementDocumentsEditCtrl.$resolve = {
  procurementDocument: [
    'ProcedureProcurementDocument',
    '$stateParams',
    function(ProcedureProcurementDocument, $stateParams) {
      return ProcedureProcurementDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
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

export { ProcedureProcurementDocumentsEditCtrl };
