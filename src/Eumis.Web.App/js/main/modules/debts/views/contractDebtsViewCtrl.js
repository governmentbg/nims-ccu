import _ from 'lodash';

function ContractDebtsViewCtrl($scope, contractDebtInfo) {
  $scope.contractDebtInfo = contractDebtInfo;

  $scope.tabList = {
    contractDebts_tabs_edit: 'root.contractDebts.view.edit',
    contractDebts_tabs_versions: 'root.contractDebts.view.versions'
  };

  if (contractDebtInfo.status !== 'new') {
    _.assign($scope.tabList, {
      contractDebts_tabs_interests: 'root.contractDebts.view.interests'
    });
  }
}

ContractDebtsViewCtrl.$inject = ['$scope', 'contractDebtInfo'];

ContractDebtsViewCtrl.$resolve = {
  contractDebtInfo: [
    'ContractDebt',
    '$stateParams',
    function(ContractDebt, $stateParams) {
      return ContractDebt.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ContractDebtsViewCtrl };
