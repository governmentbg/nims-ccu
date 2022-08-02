function PermissionTemplateModalCtrl(
  $scope,
  $uibModalInstance,
  PermissionTemplate,
  permissionTemplate
) {
  $scope.permissionTemplate = permissionTemplate;

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

PermissionTemplateModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'PermissionTemplate',
  'permissionTemplate'
];

PermissionTemplateModalCtrl.$resolve = {
  permissionTemplate: [
    'scModalParams',
    'PermissionTemplate',
    function(scModalParams, PermissionTemplate) {
      return PermissionTemplate.get({ id: scModalParams.permissionTemplateId }).$promise;
    }
  ]
};

export { PermissionTemplateModalCtrl };
