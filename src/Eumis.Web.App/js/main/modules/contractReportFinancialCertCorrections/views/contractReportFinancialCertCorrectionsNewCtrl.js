import angular from 'angular';

function ContractReportFinancialCertCorrectionsNewCtrl(
  $q,
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  Contract,
  ContractReport,
  ContractReportFinancialCertCorrection
) {
  $scope.model = {};

  $scope.save = function() {
    return $scope.newContractReportFinancialCertCorrectionsForm.$validate().then(function() {
      if ($scope.newContractReportFinancialCertCorrectionsForm.$valid) {
        return scConfirm({
          resource: 'ContractReportFinancialCertCorrection',
          validationAction: 'canCreate',
          params: {
            contractNum: $scope.model.contractNumber,
            contractReportNum: $scope.model.contractReportNum
          }
        }).then(function(result) {
          if (result.executed) {
            return ContractReportFinancialCertCorrection.save(
              {
                contractNum: $scope.model.contractNumber,
                contractReportNum: $scope.model.contractReportNum
              },
              {}
            ).$promise.then(function(result) {
              return $state.go('root.contractReportFinancialCertCorrections.view.contract', {
                id: result.contractReportFinancialCertCorrectionId
              });
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportFinancialCertCorrections.search');
  };

  $scope.chooseContractReport = function() {
    var modalInstance = scModal.open('chooseCRFCCContractReportModal', {
      procedureId: $scope.model.procedureId,
      contractRegNum: $scope.model.contractNumber,
      contractReportNum: $scope.model.contractReportNum
    });

    modalInstance.result.then(function(contractReport) {
      $scope.model.procedureId = contractReport.procedureId;
      $scope.model.contractNumber = contractReport.contractRegNum;
      $scope.model.contractReportNum = contractReport.orderNum;
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.isValidContractReportNum = function(contractReportNum) {
    if (!contractReportNum) {
      return $q.resolve();
    }

    return ContractReport.isRegNumExisting({
      contractReportNum: contractReportNum
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };

  $scope.isValidContractNum = function(contractNumber) {
    if (!contractNumber) {
      return $q.resolve();
    }

    return Contract.isRegNumExisting({
      procedureId: $scope.model.procedureId,
      contractNum: contractNumber
    }).$promise.then(function(result) {
      return result ? $q.resolve() : $q.reject();
    });
  };
}

ContractReportFinancialCertCorrectionsNewCtrl.$inject = [
  '$q',
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'Contract',
  'ContractReport',
  'ContractReportFinancialCertCorrection'
];

export { ContractReportFinancialCertCorrectionsNewCtrl };
