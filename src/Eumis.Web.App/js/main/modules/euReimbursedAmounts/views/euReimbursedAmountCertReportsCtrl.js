import angular from 'angular';

function EuReimbursedAmountCertReportsCtrl(
  $scope,
  $state,
  $stateParams,
  scModal,
  scConfirm,
  certReports
) {
  $scope.certReports = certReports;
  $scope.status = $scope.amountInfo.status;
  $scope.euReimbursedAmountId = $stateParams.id;

  $scope.chooseItems = function() {
    var modalInstance = scModal.open('chooseEuReimbursedAmountCertReportsModal', {
      euReimbursedAmountId: $stateParams.id,
      version: $scope.amountInfo.version
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.delItem = function(itemId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'EuReimbursedAmountCertReport',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: itemId,
        version: $scope.amountInfo.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

EuReimbursedAmountCertReportsCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scModal',
  'scConfirm',
  'certReports'
];

EuReimbursedAmountCertReportsCtrl.$resolve = {
  certReports: [
    '$stateParams',
    'EuReimbursedAmountCertReport',
    function($stateParams, EuReimbursedAmountCertReport) {
      return EuReimbursedAmountCertReport.query($stateParams).$promise;
    }
  ]
};

export { EuReimbursedAmountCertReportsCtrl };
