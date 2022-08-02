import _ from 'lodash';

function CompaniesSearchCtrl($scope, $state, $stateParams, $interpolate, companies) {
  $scope.companies = companies;

  $scope.companiesExportUrl = $interpolate(
    'api/companies/excelExport?' +
      'name={{name}}&uinTypeId={{uinTypeId}}&uin={{uin}}' +
      '&assessor={{assessor}}'
  )({
    name: $stateParams.name,
    uinTypeId: $stateParams.uinTypeId,
    uin: $stateParams.uin
  });

  $scope.filters = {
    name: null,
    uinTypeId: null,
    uin: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.companies.search', $scope.filters);
  };
}

CompaniesSearchCtrl.$inject = ['$scope', '$state', '$stateParams', '$interpolate', 'companies'];

CompaniesSearchCtrl.$resolve = {
  companies: [
    '$stateParams',
    'Company',
    function($stateParams, Company) {
      return Company.query($stateParams).$promise;
    }
  ]
};

export { CompaniesSearchCtrl };
