function ProcedureIntermediatePaymentDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureIntermediatePaymentDocument,
  intermediatePaymentDocument,
  procedureInfo,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.intermediatePaymentDocument = intermediatePaymentDocument;
  $scope.editMode = null;
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureIntermediatePaymentDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.intermediatePaymentDocument.version
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
      resource: 'ProcedureIntermediatePaymentDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.intermediatePaymentDocument.version
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
    return $scope.editProcedureIntermediatePaymentDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureIntermediatePaymentDocumentsForm.$valid) {
        return ProcedureIntermediatePaymentDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.intermediatePaymentDocument
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
      resource: 'ProcedureIntermediatePaymentDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.intermediatePaymentDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.reportDocs.search');
      }
    });
  };
}

ProcedureIntermediatePaymentDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureIntermediatePaymentDocument',
  'intermediatePaymentDocument',
  'procedureInfo',
  'scConfirm'
];

ProcedureIntermediatePaymentDocumentsEditCtrl.$resolve = {
  intermediatePaymentDocument: [
    'ProcedureIntermediatePaymentDocument',
    '$stateParams',
    function(ProcedureIntermediatePaymentDocument, $stateParams) {
      return ProcedureIntermediatePaymentDocument.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
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

export { ProcedureIntermediatePaymentDocumentsEditCtrl };
