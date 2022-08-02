export const ProjectFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/projects/:id',
      {},
      {
        newProjectRegistration: {
          method: 'GET',
          url: 'api/projects/new'
        },
        isCodeExisting: {
          method: 'GET',
          url: 'api/projects/isCodeExisting',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        isRegNumExisting: {
          method: 'GET',
          url: 'api/projects/isRegNumExisting',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        getContractProjectByNumber: {
          method: 'GET',
          url: 'api/projects/contractProjectByNumber'
        },
        getProjectDossierProjectByNumber: {
          method: 'GET',
          url: 'api/projects/projectDossierProjectByNumber'
        },
        isProjectValidForProjectDossier: {
          method: 'POST',
          url: 'api/projects/isProjectValidForProjectDossier'
        },
        getForSheet: {
          method: 'GET',
          url: 'api/evalSessionSheets/:id/project'
        },
        getForStandpoint: {
          method: 'GET',
          url: 'api/evalSessionStandpoints/:id/project'
        },
        getCompanyByUin: {
          method: 'GET',
          url: 'api/projects/companyByUin'
        },
        getCompanyByCode: {
          method: 'GET',
          url: 'api/projects/companyByCode'
        },
        canWithdraw: {
          method: 'POST',
          url: 'api/projects/:id/canWithdraw'
        },
        withdraw: {
          method: 'POST',
          url: 'api/projects/:id/withdraw'
        }
      }
    );
  }
];
