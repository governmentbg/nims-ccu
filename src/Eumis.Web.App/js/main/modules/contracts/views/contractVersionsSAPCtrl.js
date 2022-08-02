function ContractVersionsSAPCtrl($scope, sapData) {
  $scope.sapData = sapData;
  $scope.programmePriorities = sapData.programmePriorityCodes.join(', ');
}

ContractVersionsSAPCtrl.$inject = ['$scope', 'sapData'];

ContractVersionsSAPCtrl.$resolve = {
  sapData: [
    '$stateParams',
    'ContractVersion',
    function($stateParams, ContractVersion) {
      return ContractVersion.getSAPData($stateParams).$promise;
    }
  ]
};

export { ContractVersionsSAPCtrl };
