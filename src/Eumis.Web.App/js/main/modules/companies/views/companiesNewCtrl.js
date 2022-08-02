function CompaniesNewCtrl($scope, $state, scConfirm, Company, company) {
  $scope.company = company;

  $scope.save = function() {
    return $scope.newCompanyForm.$validate().then(function() {
      if ($scope.newCompanyForm.$valid) {
        return scConfirm({
          resource: 'Company',
          validationAction: 'canCreate',
          action: 'save',
          data: $scope.company
        }).then(function(result) {
          if (result.executed) {
            return $state.go('root.companies.search');
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.companies.search');
  };
}

CompaniesNewCtrl.$inject = ['$scope', '$state', 'scConfirm', 'Company', 'company'];

CompaniesNewCtrl.$resolve = {
  company: [
    'Company',
    function(Company) {
      return Company.newCompany().$promise;
    }
  ]
};

export { CompaniesNewCtrl };
