function ProcedureAdvancePaymentDocumentsEditCtrl(
  $scope,
  $state,
  $stateParams,
  ProcedureAdvancePaymentDocument,
  advancePaymentDocument,
  procedureInfo,
  scConfirm
) {
  $scope.procedureId = $stateParams.id;
  $scope.advancePaymentDocument = advancePaymentDocument;
  $scope.editMode = null;
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';

  $scope.deactivate = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDeactivate',
      resource: 'ProcedureAdvancePaymentDocument',
      action: 'deactivate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.advancePaymentDocument.version
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
      resource: 'ProcedureAdvancePaymentDocument',
      action: 'activate',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.advancePaymentDocument.version
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
    return $scope.editProcedureAdvancePaymentDocumentsForm.$validate().then(function() {
      if ($scope.editProcedureAdvancePaymentDocumentsForm.$valid) {
        return ProcedureAdvancePaymentDocument.update(
          { id: $stateParams.id, ind: $stateParams.ind },
          $scope.advancePaymentDocument
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
      resource: 'ProcedureAdvancePaymentDocument',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.advancePaymentDocument.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.procedures.view.reportDocs.search');
      }
    });
  };
}

ProcedureAdvancePaymentDocumentsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProcedureAdvancePaymentDocument',
  'advancePaymentDocument',
  'procedureInfo',
  'scConfirm'
];

ProcedureAdvancePaymentDocumentsEditCtrl.$resolve = {
  advancePaymentDocument: [
    'ProcedureAdvancePaymentDocument',
    '$stateParams',
    function(ProcedureAdvancePaymentDocument, $stateParams) {
      return ProcedureAdvancePaymentDocument.get({ id: $stateParams.id, ind: $stateParams.ind })
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

export { ProcedureAdvancePaymentDocumentsEditCtrl };
