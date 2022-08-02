import _ from 'lodash';

function RequestPackagesSearchCtrl($scope, $state, $stateParams, RequestPackage, requestPackages) {
  $scope.requestPackages = requestPackages;

  $scope.filters = {
    dateFrom: null,
    dateTo: null,
    typeId: null,
    userOrganizationId: null,
    statusId: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.requestPackages.search', $scope.filters);
  };
}

RequestPackagesSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'RequestPackage',
  'requestPackages'
];

RequestPackagesSearchCtrl.$resolve = {
  requestPackages: [
    '$stateParams',
    'RequestPackage',
    function($stateParams, RequestPackage) {
      return RequestPackage.query($stateParams).$promise;
    }
  ]
};

export { RequestPackagesSearchCtrl };
