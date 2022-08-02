import _ from 'lodash';

function ChooseNSContractsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  NotificationSettingAttachedContract,
  contracts
) {
  $scope.chosenContractIds = [];
  $scope.contracts = contracts;
  $scope.hasChoosenAll = true;
  $scope.tableControl = {};

  $scope.chooseAll = function() {
    var filteredContracts = $scope.tableControl.getFilteredItems();
    _.forEach(filteredContracts, function(cr) {
      if (_.includes($scope.chosenContractIds, cr.contractId)) {
        cr.isChosen = true;
      } else {
        $scope.choose(cr);
      }
    });
    $scope.hasChoosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.contracts, function(cr) {
      $scope.remove(cr);
    });
    $scope.hasChoosenAll = true;
  };

  $scope.choose = function(contract) {
    contract.isChosen = true;
    $scope.chosenContractIds.push(contract.contractId);
  };

  $scope.remove = function(contract) {
    contract.isChosen = false;
    $scope.chosenContractIds = _.without($scope.chosenContractIds, contract.contractId);
  };

  $scope.ok = function() {
    return NotificationSettingAttachedContract.save(
      {
        id: scModalParams.notificationSettingId,
        version: scModalParams.version
      },
      $scope.chosenContractIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseNSContractsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'NotificationSettingAttachedContract',
  'contracts'
];

ChooseNSContractsModalCtrl.$resolve = {
  contracts: [
    'NotificationSettingAttachedContract',
    'scModalParams',
    function(NotificationSettingAttachedContract, scModalParams) {
      return NotificationSettingAttachedContract.getContracts({
        id: scModalParams.notificationSettingId
      }).$promise;
    }
  ]
};

export { ChooseNSContractsModalCtrl };
