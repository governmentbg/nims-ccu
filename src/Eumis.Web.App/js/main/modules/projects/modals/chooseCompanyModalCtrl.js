function ChooseCompanyModalCtrl($scope, $uibModalInstance, scModalParams, Company, companies) {
  $scope.companies = companies;
  $scope.filters = {
    name: null,
    uinTypeId: scModalParams.uinTypeId,
    uin: scModalParams.uin
  };

  $scope.search = function() {
    return Company.query($scope.filters).$promise.then(function(result) {
      $scope.companies = result;
    });
  };

  $scope.choose = function(company) {
    return $uibModalInstance.close(company);
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

ChooseCompanyModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'Company',
  'companies'
];

ChooseCompanyModalCtrl.$resolve = {
  companies: [
    'Company',
    'scModalParams',
    function(Company, scModalParams) {
      return Company.query(scModalParams).$promise;
    }
  ]
};

export { ChooseCompanyModalCtrl };
