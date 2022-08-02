import _ from 'lodash';

function ChooseContractContractItemsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  FlatFinancialCorrectionContractContractItem,
  contractContracts
) {
  $scope.form = {};
  $scope.chosenContractContractIds = [];
  $scope.contractContracts = contractContracts;

  $scope.choose = function(contractContract) {
    contractContract.isChosen = true;
    $scope.chosenContractContractIds.push(contractContract.itemId);
  };

  $scope.remove = function(contractContract) {
    contractContract.isChosen = false;
    $scope.chosenContractContractIds = _.without(
      $scope.chosenContractContractIds,
      contractContract.itemId
    );
  };

  $scope.ok = function() {
    return FlatFinancialCorrectionContractContractItem.save(
      {
        id: scModalParams.flatFinancialCorrectionId,
        version: scModalParams.version
      },
      $scope.chosenContractContractIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseContractContractItemsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'FlatFinancialCorrectionContractContractItem',
  'contractContracts'
];

ChooseContractContractItemsModalCtrl.$resolve = {
  contractContracts: [
    'FlatFinancialCorrectionContractContractItem',
    'scModalParams',
    function(FlatFinancialCorrectionContractContractItem, scModalParams) {
      return FlatFinancialCorrectionContractContractItem.getContractContracts({
        id: scModalParams.flatFinancialCorrectionId
      }).$promise;
    }
  ]
};

export { ChooseContractContractItemsModalCtrl };
