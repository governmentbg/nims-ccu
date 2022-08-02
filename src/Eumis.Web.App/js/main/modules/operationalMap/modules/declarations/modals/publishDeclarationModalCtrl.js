function PublishDeclarationModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  Declaration,
  publication
) {
  $scope.model = publication;

  $scope.publish = function() {
    return $scope.publishDeclarationForm.$validate().then(function() {
      if ($scope.publishDeclarationForm.$valid) {
        return Declaration.publish(
          {
            id: scModalParams.declarationId
          },
          $scope.model
        ).$promise.then(function() {
          return $uibModalInstance.close();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

PublishDeclarationModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'Declaration',
  'publication'
];

PublishDeclarationModalCtrl.$resolve = {
  publication: [
    'scModalParams',
    'Declaration',
    function(scModalParams, Declaration) {
      return Declaration.newPublication({
        id: scModalParams.declarationId
      }).$promise;
    }
  ]
};

export { PublishDeclarationModalCtrl };
