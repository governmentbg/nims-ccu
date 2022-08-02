function ProcedureReportDocumentsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  technicalReportDocs,
  financialReportDocs,
  advancePaymentDocs,
  intermediatePaymentDocs,
  finalPaymentDocs,
  procurementDocs,
  procedureInfo,
  scConfirm,
  l10n
) {
  $scope.isSectionReadonly =
    procedureInfo.procedureContractReportDocumentsSectionStatus !== 'draft';
  $scope.procedureId = $stateParams.id;
  $scope.procedureVersion = procedureInfo.version;
  $scope.status = procedureInfo.procedureContractReportDocumentsSectionStatus;

  $scope.technicalReportDocs = technicalReportDocs;
  $scope.financialReportDocs = financialReportDocs;
  $scope.advancePaymentDocs = advancePaymentDocs;
  $scope.intermediatePaymentDocs = intermediatePaymentDocs;
  $scope.finalPaymentDocs = finalPaymentDocs;
  $scope.procurementDocs = procurementDocs;

  $scope.changeStatus = function(sectionStatus) {
    var confirmMsg = $interpolate(
      l10n.get('procedures_contractReportDocumentsSearch_changeStatusConfirm')
    )({
      status: l10n.get('procedures_contractReportDocumentsSearch_' + sectionStatus)
    });

    return scConfirm({
      confirmMessage: confirmMsg,
      resource: 'Procedure',
      action:
        'changeContractReportDocumentsSectionStatusTo' +
        sectionStatus.charAt(0).toUpperCase() +
        sectionStatus.slice(1),
      params: { id: $scope.procedureId, version: $scope.procedureVersion }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProcedureReportDocumentsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'technicalReportDocs',
  'financialReportDocs',
  'advancePaymentDocs',
  'intermediatePaymentDocs',
  'finalPaymentDocs',
  'procurementDocs',
  'procedureInfo',
  'scConfirm',
  'l10n'
];

ProcedureReportDocumentsSearchCtrl.$resolve = {
  technicalReportDocs: [
    '$stateParams',
    'ProcedureTechnicalReportDocument',
    function($stateParams, ProcedureTechnicalReportDocument) {
      return ProcedureTechnicalReportDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  financialReportDocs: [
    '$stateParams',
    'ProcedureFinancialReportDocument',
    function($stateParams, ProcedureFinancialReportDocument) {
      return ProcedureFinancialReportDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  advancePaymentDocs: [
    '$stateParams',
    'ProcedureAdvancePaymentDocument',
    function($stateParams, ProcedureAdvancePaymentDocument) {
      return ProcedureAdvancePaymentDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  intermediatePaymentDocs: [
    '$stateParams',
    'ProcedureIntermediatePaymentDocument',
    function($stateParams, ProcedureIntermediatePaymentDocument) {
      return ProcedureIntermediatePaymentDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  finalPaymentDocs: [
    '$stateParams',
    'ProcedureFinalPaymentDocument',
    function($stateParams, ProcedureFinalPaymentDocument) {
      return ProcedureFinalPaymentDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  procurementDocs: [
    '$stateParams',
    'ProcedureProcurementDocument',
    function($stateParams, ProcedureProcurementDocument) {
      return ProcedureProcurementDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  procedureInfo: [
    '$stateParams',
    'Procedure',
    function($stateParams, Procedure) {
      return Procedure.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureReportDocumentsSearchCtrl };
