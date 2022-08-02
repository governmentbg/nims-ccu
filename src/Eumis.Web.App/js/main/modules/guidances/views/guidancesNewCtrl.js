function GuidancesNewCtrl($scope, $state, Guidance, guidance) {
  $scope.guidance = guidance;

  $scope.save = function() {
    return $scope.newGuidanceForm.$validate().then(function() {
      if ($scope.newGuidanceForm.$valid) {
        return Guidance.save($scope.guidance).$promise.then(function(result) {
          return $state.go('root.guidances.edit', {
            id: result.guidanceId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.guidances.search');
  };
}

GuidancesNewCtrl.$inject = ['$scope', '$state', 'Guidance', 'guidance'];

GuidancesNewCtrl.$resolve = {
  guidance: [
    'Guidance',
    function(Guidance) {
      return Guidance.newGuidance().$promise;
    }
  ]
};

export { GuidancesNewCtrl };
