function CompaniesEditCtrl($scope, $state, $stateParams, scMessage, scConfirm, Company, company) {
  $scope.editMode = null;
  $scope.company = company;

  $scope.showLAGMunicipalities = company.isLocalActionGroup;

  $scope.save = function() {
    return $scope.editCompanyForm.$validate().then(function() {
      if ($scope.editCompanyForm.$valid) {
        return Company.update({ id: $stateParams.id }, $scope.company).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'Company',
      validationAction: 'canDelete',
      action: 'remove',
      params: {
        id: $stateParams.id,
        version: $scope.company.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.companies.search');
      }
    });
  };
}

CompaniesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scMessage',
  'scConfirm',
  'Company',
  'company'
];

CompaniesEditCtrl.$resolve = {
  company: [
    'Company',
    '$stateParams',
    function(Company, $stateParams) {
      return Company.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { CompaniesEditCtrl };
