function AllowancesSearchCtrl($scope, allowances) {
  $scope.allowances = allowances;
}

AllowancesSearchCtrl.$inject = ['$scope', 'allowances'];

AllowancesSearchCtrl.$resolve = {
  allowances: [
    'Allowance',
    function(Allowance) {
      return Allowance.query().$promise;
    }
  ]
};

export { AllowancesSearchCtrl };
