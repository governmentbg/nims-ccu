function RegularCtrl($scope, scMessage, $timeout) {
  $scope.someData = {};
  $scope.load = function() {
    return $timeout(function() {
      return undefined;
    }, 5000);
  };
  $scope.validate = function() {
    return $scope.testForm.$validate().then(function() {
      if ($scope.testForm.$valid) {
        return $timeout(function() {
          return undefined;
        }, 500);
      }
    });
  };
}

RegularCtrl.$inject = ['$scope', 'scMessage', '$timeout'];

export { RegularCtrl };
