function ProcedureFinancialReportDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureFinancialReportDocument,
  financialReportDocument,
  procedureInfo,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.financialReportDocument = financialReportDocument;
  $scope.editMode = null;
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureFinancialReportDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.financialReportDocument.version
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
      resource: 'ProcedureFinancialReportDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.financialReportDocument.version
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
    return $scope.editProcedureFinancialReportDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureFinancialReportDocumentsForm.$valid) {
        return ProcedureFinancialReportDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.financialReportDocument
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
      resource: 'ProcedureFinancialReportDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.financialReportDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.reportDocs.search');
      }
    });
  };
}

ProcedureFinancialReportDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureFinancialReportDocument',
  'financialReportDocument',
  'procedureInfo',
  'scConfirm'
];

ProcedureFinancialReportDocumentsEditCtrl.$resolve = {
  financialReportDocument: [
    'ProcedureFinancialReportDocument',
    '$stateParams',
    function(ProcedureFinancialReportDocument, $stateParams) {
      return ProcedureFinancialReportDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
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

export { ProcedureFinancialReportDocumentsEditCtrl };
