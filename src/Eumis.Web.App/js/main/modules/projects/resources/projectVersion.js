export const ProjectVersionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:id/versions/:vid',
      {},
      {
        newProjectVersion: {
          method: 'GET',
          url: 'api/projects/:id/versions/new'
        },
        getVersionsForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/projectVersions',
          isArray: true
        },
        getVersionsForStandpoint: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/projectVersions',
          isArray: true
        },
        createFromRegData: {
          method: 'POST',
          url: 'api/projects/:id/versions/createFromRegData'
        },
        canCreate: {
          method: 'POST',
          url: 'api/projects/:id/versions/canCreate'
        }
      }
    );
  }
];
