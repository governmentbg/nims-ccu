import angular from 'angular';
import _ from 'lodash';

function ContractReportAttachedDocFinancialCorrectionCtrl(
  $scope,
  $state,
  $stateParams,
  csdNameCreator,
  scModal,
  contractReportAttachedFinancialCorrection
) {
  $scope.contractReportId = $stateParams.id;
  $scope.contractReportAttachedFinancialCorrection = contractReportAttachedFinancialCorrection;

  $scope.contractReportAttachedFinancialCorrection.contractReportFinancialCorrectionCSDs = _.forEach(
    contractReportAttachedFinancialCorrection.contractReportFinancialCorrectionCSDs,
    function(item) {
      csdNameCreator(item);
    }
  );

  $scope.viewCorrectedBudgetItem = function(contractReportFinancialCorrectionCSDId) {
    var modalInstance = scModal.open('correctionCSDBudgetItemModal', {
      contractReportFinancialCorrectionId: $stateParams.ind,
      contractReportFinancialCorrectionCSDId: contractReportFinancialCorrectionCSDId
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.back = function() {
    return $state.go('root.contractReportChecks.view.attachedDocs.search');
  };
}

ContractReportAttachedDocFinancialCorrectionCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'csdNameCreator',
  'scModal',
  'contractReportAttachedFinancialCorrection'
];

ContractReportAttachedDocFinancialCorrectionCtrl.$resolve = {
  contractReportAttachedFinancialCorrection: [
    'ContractReportAttachedFinancialCorrection',
    '$stateParams',
    function(ContractReportAttachedFinancialCorrection, $stateParams) {
      return ContractReportAttachedFinancialCorrection.get({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ContractReportAttachedDocFinancialCorrectionCtrl };
