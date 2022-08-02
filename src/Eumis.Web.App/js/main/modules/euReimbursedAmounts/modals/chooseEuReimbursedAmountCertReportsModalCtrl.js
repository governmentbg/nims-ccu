import _ from 'lodash';

function ChooseEuReimbursedAmountCertReportsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  l10n,
  EuReimbursedAmountCertReport,
  items
) {
  $scope.chosenIds = [];
  $scope.items = items;

  $scope.choose = function(item) {
    item.isChosen = true;
    $scope.chosenIds.push(item.certReportId);
  };

  $scope.remove = function(item) {
    item.isChosen = false;
    $scope.chosenIds = _.without($scope.chosenIds, item.certReportId);
  };

  $scope.ok = function() {
    return EuReimbursedAmountCertReport.save(
      {
        id: scModalParams.euReimbursedAmountId,
        version: scModalParams.version
      },
      $scope.chosenIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseEuReimbursedAmountCertReportsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'l10n',
  'EuReimbursedAmountCertReport',
  'items'
];

ChooseEuReimbursedAmountCertReportsModalCtrl.$resolve = {
  items: [
    'EuReimbursedAmountCertReport',
    'scModalParams',
    function(EuReimbursedAmountCertReport, scModalParams) {
      return EuReimbursedAmountCertReport.getCertReports({
        id: scModalParams.euReimbursedAmountId
      }).$promise;
    }
  ]
};

export { ChooseEuReimbursedAmountCertReportsModalCtrl };
