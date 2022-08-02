import _ from 'lodash';

function DeclarationsSearchCtrl($scope, $state, $stateParams, declarations) {
  $scope.declarations = declarations;

  $scope.filters = {
    activationDate: null,
    status: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.map.declarations.search', $scope.filters);
  };
}

DeclarationsSearchCtrl.$inject = ['$scope', '$state', '$stateParams', 'declarations'];

DeclarationsSearchCtrl.$resolve = {
  declarations: [
    '$stateParams',
    'Declaration',
    function($stateParams, Declaration) {
      return Declaration.query($stateParams).$promise;
    }
  ]
};

export { DeclarationsSearchCtrl };
