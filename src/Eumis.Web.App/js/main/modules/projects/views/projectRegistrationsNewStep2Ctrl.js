function ProjectRegistrationsNewStep2Ctrl($scope, $state, $stateParams, Company, Project, results) {
  $scope.results = results;

  $scope.next = function() {
    return $scope.projectRegistrationsNewStep2Form.$validate().then(function() {
      if ($scope.projectRegistrationsNewStep2Form.$valid) {
        return $state.go('root.projects.newStep3', {
          pId: $scope.results.procedureId,
          cId: $scope.results.company.companyId,
          xmlId: $scope.results.regProjectXmlId
        });
      }
    });
  };

  $scope.createContinue = function() {
    return $scope.projectRegistrationsNewStep2Form.$validate().then(function() {
      if ($scope.projectRegistrationsNewStep2Form.$valid) {
        return Company.save($scope.results.company).$promise.then(function(company) {
          return $state.go('root.projects.newStep3', {
            pId: $scope.results.procedureId,
            cId: company.companyId,
            xmlId: $scope.results.regProjectXmlId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.projects.search');
  };
}

ProjectRegistrationsNewStep2Ctrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'Company',
  'Project',
  'results'
];

ProjectRegistrationsNewStep2Ctrl.$resolve = {
  results: [
    '$stateParams',
    'Project',
    '$state',
    function($stateParams, Project, $state) {
      if ($stateParams.pId && $stateParams.uinType && $stateParams.uin) {
        return Project.getCompanyByUin({
          procedureId: $stateParams.pId,
          uinType: $stateParams.uinType,
          uin: $stateParams.uin
        }).$promise;
      } else if ($stateParams.code) {
        return Project.getCompanyByCode({
          code: $stateParams.code
        }).$promise;
      } else {
        return $state.go('root.projects.search');
      }
    }
  ]
};

export { ProjectRegistrationsNewStep2Ctrl };
