function PTemplatesEditCtrl(
  $q,
  $scope,
  $state,
  $stateParams,
  scConfirm,
  PermissionTemplate,
  permissionTemplate
) {
  $scope.editMode = null;
  $scope.permissionTemplate = permissionTemplate;
  $scope.oldPermissionTemplateName = permissionTemplate.name;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return scConfirm({
      confirmMessage: 'pTemplates_edit_confirmUpdate',
      resource: 'PermissionTemplate',
      validationAction: 'canUpdate',
      action: 'update',
      params: {
        id: $stateParams.id
      },
      data: $scope.permissionTemplate
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.isNameExist = function(name) {
    if (!name || name === $scope.oldPermissionTemplateName) {
      return $q.resolve();
    } else {
      return PermissionTemplate.isNameExist({
        name: name
      }).$promise.then(function(exist) {
        return !exist ? $q.resolve() : $q.reject();
      });
    }
  };
}

PTemplatesEditCtrl.$inject = [
  '$q',
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'PermissionTemplate',
  'permissionTemplate'
];

PTemplatesEditCtrl.$resolve = {
  permissionTemplate: [
    '$stateParams',
    'PermissionTemplate',
    function($stateParams, PermissionTemplate) {
      return PermissionTemplate.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { PTemplatesEditCtrl };
