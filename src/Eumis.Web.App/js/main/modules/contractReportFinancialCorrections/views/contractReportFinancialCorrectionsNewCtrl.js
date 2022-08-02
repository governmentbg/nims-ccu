import angular from 'angular';

function ContractReportFinancialCorrectionsNewCtrl(
  $q,
  $scope,
  $state,
  $stateParams,
  scConfirm,
  scModal,
  Contract,
  ContractReport,
  ContractReportFinancialCorrection
) {
  $scope.model = {};

  $scope.save = function() {
    return $scope.newContractReportFinancialCorrectionsForm.$validate().then(function() {
      if ($scope.newContractReportFinancialCorrectionsForm.$valid) {
        return scConfirm({
          resource: 'ContractReportFinancialCorrection',
          validationAction: 'canCreate',
          params: {
            contractNum: $scope.model.contractNumber,
            contractReportNum: $scope.model.contractReportNum
          }
        }).then(function(result) {
          if (result.executed) {
            return ContractReportFinancialCorrection.save(
              {
                contractNum: $scope.model.contractNumber,
                contractReportNum: $scope.model.contractReportNum
              },
              {}
            ).$promise.then(function(result) {
              return $state.go('root.contractReportFinancialCorrections.view.contract', {
                id: result.contractReportFinancialCorrectionId
              });
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportFinancialCorrections.search');
  };

  $scope.chooseContractReport = function() {
    var modalInstance = scModal.open('chooseCRFCContractReportModal', {
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

ContractReportFinancialCorrectionsNewCtrl.$inject = [
  '$q',
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'scModal',
  'Contract',
  'ContractReport',
  'ContractReportFinancialCorrection'
];

export { ContractReportFinancialCorrectionsNewCtrl };
