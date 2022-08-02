function ServiceContractsNewStep2Ctrl($scope, $state, $stateParams, Company, Contract, results) {
  $scope.results = results;

  $scope.next = function() {
    return $scope.serviceContractsNewStep2Form.$validate().then(function() {
      if ($scope.serviceContractsNewStep2Form.$valid) {
        return $state.go('root.contracts.serviceContracts.newStep3', {
          pId: $scope.results.procedureId,
          cId: $scope.results.company.companyId,
          xmlId: $scope.results.regProjectXmlId
        });
      }
    });
  };

  $scope.createContinue = function() {
    return $scope.serviceContractsNewStep2Form.$validate().then(function() {
      if ($scope.serviceContractsNewStep2Form.$valid) {
        return Company.save($scope.results.company).$promise.then(function(company) {
          return $state.go('root.contracts.serviceContracts.newStep3', {
            pId: $scope.results.procedureId,
            cId: company.companyId,
            xmlId: $scope.results.regProjectXmlId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.contracts.search');
  };
}

ServiceContractsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'Company',
  'Project',
  'results'
];

ServiceContractsNewStep2Ctrl.$resolve = {
  results: [
    '$stateParams',
    'Project',
    function($stateParams, Project) {
      if ($stateParams.pId && $stateParams.uinType && $stateParams.uin) {
        return Project.getCompanyByUin({
          procedureId: $stateParams.pId,
          uinType: $stateParams.uinType,
          uin: $stateParams.uin
        }).$promise;
      }
    }
  ]
};

export { ServiceContractsNewStep2Ctrl };
