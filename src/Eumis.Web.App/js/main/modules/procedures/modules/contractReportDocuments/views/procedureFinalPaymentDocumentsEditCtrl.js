function ProcedureFinalPaymentDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureFinalPaymentDocument,
  finalPaymentDocument,
  procedureInfo,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.finalPaymentDocument = finalPaymentDocument;
  $scope.editMode = null;
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureFinalPaymentDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.finalPaymentDocument.version
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
      resource: 'ProcedureFinalPaymentDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.finalPaymentDocument.version
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
    return $scope.editProcedureFinalPaymentDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureFinalPaymentDocumentsForm.$valid) {
        return ProcedureFinalPaymentDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.finalPaymentDocument
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
      resource: 'ProcedureFinalPaymentDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.finalPaymentDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.reportDocs.search');
      }
    });
  };
}

ProcedureFinalPaymentDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureFinalPaymentDocument',
  'finalPaymentDocument',
  'procedureInfo',
  'scConfirm'
];

ProcedureFinalPaymentDocumentsEditCtrl.$resolve = {
  finalPaymentDocument: [
    'ProcedureFinalPaymentDocument',
    '$stateParams',
    function(ProcedureFinalPaymentDocument, $stateParams) {
      return ProcedureFinalPaymentDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
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

export { ProcedureFinalPaymentDocumentsEditCtrl };
