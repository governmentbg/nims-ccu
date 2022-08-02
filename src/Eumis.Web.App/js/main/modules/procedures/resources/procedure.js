export const ProcedureFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id',
      {},
      {
        getTree: {
          method: 'GET',
          url: 'api/procedures/tree',
          isArray: true
        },
        newProcedure: {
          method: 'GET',
          url: 'api/procedures/new'
        },
        copyProcedure: {
          method: 'GET',
          url: 'api/procedures/:id/copy'
        },
        getInfo: {
          method: 'GET',
          url: 'api/procedures/:id/info'
        },
        getGid: {
          method: 'GET',
          url: 'api/procedures/:id/gid',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        changeStatusToDraft: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToDraft'
        },
        changeStatusToEntered: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToEntered'
        },
        changeStatusToChecked: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToChecked'
        },
        changeStatusToActive: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToActive'
        },
        changeStatusToEnded: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToEnded'
        },
        changeStatusToTerminated: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToTerminated'
        },
        changeStatusToCanceled: {
          method: 'PUT',
          url: 'api/procedures/:id/changeStatusToCanceled'
        },
        canChangeStatusToEntered: {
          method: 'POST',
          url: 'api/procedures/:id/canChangeStatusToEntered'
        },
        validate: {
          method: 'POST',
          url: 'api/procedures/:id/validate'
        },
        changeContractReportDocumentsSectionStatusToDraft: {
          method: 'PUT',
          url: 'api/procedures/:id/changeContractReportDocumentsSectionStatusToDraft'
        },
        changeContractReportDocumentsSectionStatusToActive: {
          method: 'PUT',
          url: 'api/procedures/:id/changeContractReportDocumentsSectionStatusToActive'
        }
      }
    );
  }
];
