function InterestSchemesSearchCtrl($scope, interestSchemes) {
  $scope.interestSchemes = interestSchemes;
}

InterestSchemesSearchCtrl.$inject = ['$scope', 'interestSchemes'];

InterestSchemesSearchCtrl.$resolve = {
  interestSchemes: [
    'InterestScheme',
    function(InterestScheme) {
      return InterestScheme.query().$promise;
    }
  ]
};

export { InterestSchemesSearchCtrl };
