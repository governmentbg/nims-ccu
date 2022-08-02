export const PermissionTemplateFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/permissionTemplates/:id',
      {},
      {
        newTemplate: {
          method: 'GET',
          url: 'api/permissionTemplates/new'
        },
        isNameExist: {
          method: 'GET',
          url: 'api/permissionTemplates/isNameExist',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        canUpdate: {
          method: 'POST',
          url: 'api/permissionTemplates/:id/canUpdate'
        }
      }
    );
  }
];
