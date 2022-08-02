function InterestSchemesNewCtrl($scope, $state, InterestScheme, newInterestScheme) {
  $scope.newInterestScheme = newInterestScheme;

  $scope.save = function() {
    return $scope.newInterestSchemeForm.$validate().then(function() {
      if ($scope.newInterestSchemeForm.$valid) {
        return InterestScheme.save($scope.newInterestScheme).$promise.then(function() {
          return $state.go('root.map.interestSchemes.search');
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.interestSchemes.search');
  };
}

InterestSchemesNewCtrl.$inject = ['$scope', '$state', 'InterestScheme', 'newInterestScheme'];

InterestSchemesNewCtrl.$resolve = {
  newInterestScheme: [
    'InterestScheme',
    function(InterestScheme) {
      return InterestScheme.newInterestScheme().$promise;
    }
  ]
};

export { InterestSchemesNewCtrl };
