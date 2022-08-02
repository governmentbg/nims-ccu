import angular from 'angular';

function ContractReportDocumentsCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  ContractReportPayment,
  contractReportFinancials,
  contractReportTechnicals,
  contractReportPayments
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportStatus = $scope.contractReportInfo.status;
  $scope.contractReportSource = $scope.contractReportInfo.source;
  $scope.contractReportType = $scope.contractReportInfo.reportType;
  $scope.contractReportHasReturnedDocuments = $scope.contractReportInfo.hasReturnedDocuments;
  $scope.contractReportFinancials = contractReportFinancials;
  $scope.contractReportTechnicals = contractReportTechnicals;
  $scope.contractReportPayments = contractReportPayments;

  $scope.newReport = function(type) {
    var reportResource;

    if (type === 'financial') {
      reportResource = 'ContractReportFinancial';
    } else if (type === 'technical') {
      reportResource = 'ContractReportTechnical';
    }

    return scConfirm({
      validationAction: 'canCreate',
      resource: reportResource,
      action: 'save',
      params: {
        id: $stateParams.id
      }
    }).then(function(result) {
      if (result.executed) {
        if (type === 'financial') {
          return $state.go('root.contractReports.view.documents.editFinancial', {
            id: $stateParams.id,
            ind: result.result.contractReportFinancialId
          });
        } else if (type === 'technical') {
          return $state.go('root.contractReports.view.documents.editTechnical', {
            id: $stateParams.id,
            ind: result.result.contractReportTechnicalId
          });
        }
      }
    });
  };

  $scope.newReportPayment = function() {
    var modalInstance = scModal.open('choosePaymentTypeModal', {
      contractReportId: $stateParams.id
    });

    modalInstance.result.then(function(contractReportPaymentId) {
      return $state.go('root.contractReports.view.documents.editPayment', {
        id: $stateParams.id,
        ind: contractReportPaymentId
      });
    }, angular.noop);

    return modalInstance.opened;
  };
}

ContractReportDocumentsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'ContractReportPayment',
  'contractReportFinancials',
  'contractReportTechnicals',
  'contractReportPayments'
];

ContractReportDocumentsCtrl.$resolve = {
  contractReportFinancials: [
    '$stateParams',
    'ContractReportFinancial',
    function($stateParams, ContractReportFinancial) {
      return ContractReportFinancial.query($stateParams).$promise;
    }
  ],
  contractReportTechnicals: [
    '$stateParams',
    'ContractReportTechnical',
    function($stateParams, ContractReportTechnical) {
      return ContractReportTechnical.query($stateParams).$promise;
    }
  ],
  contractReportPayments: [
    '$stateParams',
    'ContractReportPayment',
    function($stateParams, ContractReportPayment) {
      return ContractReportPayment.query($stateParams).$promise;
    }
  ]
};

export { ContractReportDocumentsCtrl };
