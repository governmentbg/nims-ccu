import angular from 'angular';

function ContractReportTechnicalCorrectionsNewCtrl(
  $q,
  $scope,
  $state,
  scConfirm,
  scModal,
  Contract,
  ContractReport,
  ContractReportTechnicalCorrection
) {
  $scope.model = {};

  $scope.save = function() {
    const save = () =>
      ContractReportTechnicalCorrection.save(
        {
          contractNum: $scope.model.contractNumber,
          contractReportNum: $scope.model.contractReportNum
        },
        {}
      ).$promise.then(result =>
        $state.go('root.contractReportTechnicalCorrections.view.contract', {
          id: result.contractReportTechnicalCorrectionId
        })
      );

    return $scope.newContractReportTechnicalCorrectionsForm.$validate().then(function() {
      if ($scope.newContractReportTechnicalCorrectionsForm.$valid) {
        return scConfirm({
          resource: 'ContractReportTechnicalCorrection',
          validationAction: 'canCreate',
          params: {
            contractNum: $scope.model.contractNumber,
            contractReportNum: $scope.model.contractReportNum
          }
        }).then(function(result) {
          if (result.executed) {
            return ContractReportTechnicalCorrection.existsEnded(
              {
                contractNum: $scope.model.contractNumber,
                contractReportNum: $scope.model.contractReportNum
              },
              {}
            ).$promise.then(function(result) {
              if (result) {
                return scConfirm({
                  confirmMessage:
                    'contractReportTechnicalCorrections_newContractReportTechnicalCorrection_confirmCreate'
                }).then(function(result) {
                  if (result.executed) {
                    return save();
                  }
                });
              } else {
                return save();
              }
            });
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contractReportTechnicalCorrections.search');
  };

  $scope.chooseContractReport = function() {
    var modalInstance = scModal.open('chooseCRTCContractReportModal', {
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

ContractReportTechnicalCorrectionsNewCtrl.$inject = [
  '$q',
  '$scope',
  '$state',
  'scConfirm',
  'scModal',
  'Contract',
  'ContractReport',
  'ContractReportTechnicalCorrection'
];

export { ContractReportTechnicalCorrectionsNewCtrl };
