function DeclarationsNewCtrl($scope, $state, Declaration, declaration) {
  $scope.declaration = declaration;

  $scope.save = function() {
    return $scope.newDeclarationForm.$validate().then(function() {
      if ($scope.newDeclarationForm.$valid) {
        return Declaration.save($scope.declaration).$promise.then(function(result) {
          return $state.go('root.map.declarations.edit', {
            id: result.declarationId
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.map.declarations.search');
  };
}

DeclarationsNewCtrl.$inject = ['$scope', '$state', 'Declaration', 'declaration'];

DeclarationsNewCtrl.$resolve = {
  declaration: [
    'Declaration',
    function(Declaration) {
      return Declaration.newDeclaration().$promise;
    }
  ]
};

export { DeclarationsNewCtrl };
