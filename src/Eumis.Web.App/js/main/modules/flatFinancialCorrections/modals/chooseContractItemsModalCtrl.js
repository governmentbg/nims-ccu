import _ from 'lodash';

function ChooseContractItemsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  FlatFinancialCorrectionContractItem,
  contracts
) {
  $scope.form = {};
  $scope.chosenContractIds = [];
  $scope.contracts = contracts;

  $scope.choose = function(contract) {
    contract.isChosen = true;
    $scope.chosenContractIds.push(contract.itemId);
  };

  $scope.remove = function(contract) {
    contract.isChosen = false;
    $scope.chosenContractIds = _.without($scope.chosenContractIds, contract.itemId);
  };

  $scope.ok = function() {
    return FlatFinancialCorrectionContractItem.save(
      {
        id: scModalParams.flatFinancialCorrectionId,
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

ChooseContractItemsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'FlatFinancialCorrectionContractItem',
  'contracts'
];

ChooseContractItemsModalCtrl.$resolve = {
  contracts: [
    'FlatFinancialCorrectionContractItem',
    'scModalParams',
    function(FlatFinancialCorrectionContractItem, scModalParams) {
      return FlatFinancialCorrectionContractItem.getContracts({
        id: scModalParams.flatFinancialCorrectionId
      }).$promise;
    }
  ]
};

export { ChooseContractItemsModalCtrl };
