export const ContractReportFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReports/:id',
      {},
      {
        getContracts: {
          method: 'GET',
          url: 'api/reportContracts',
          isArray: true
        },
        getReportsForCheck: {
          method: 'GET',
          url: 'api/contractReports/checks',
          isArray: true
        },
        newContractReport: {
          method: 'GET',
          url: 'api/contractReports/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReports/:id/info'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contractReports/canCreate'
        },
        canDelete: {
          method: 'POST',
          url: 'api/contractReports/:id/canDelete'
        },
        canUpdate: {
          method: 'POST',
          url: 'api/contractReports/:id/canUpdate'
        },
        getCanChangeStatusToUnchecked: {
          method: 'GET',
          url: 'api/contractReports/:id/canChangeStatusToUnchecked'
        },
        checkUpdate: {
          method: 'PUT',
          url: 'api/contractReports/:id/checkUpdate'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contractReports/:id/changeStatusToDraft'
        },
        changeStatusToEntered: {
          method: 'POST',
          url: 'api/contractReports/:id/changeStatusToEntered'
        },
        changeStatusToSentChecked: {
          method: 'POST',
          url: 'api/contractReports/:id/changeStatusToSentChecked'
        },
        changeStatusToUnchecked: {
          method: 'POST',
          url: 'api/contractReports/:id/changeStatusToUnchecked'
        },
        changeStatusToAccepted: {
          method: 'POST',
          url: 'api/contractReports/:id/changeStatusToAccepted'
        },
        changeStatusToRefused: {
          method: 'POST',
          url: 'api/contractReports/:id/changeStatusToRefused'
        },
        canChangeStatusToEntered: {
          method: 'POST',
          url: 'api/contractReports/:id/canChangeStatusToEntered'
        },
        canChangeStatusToAccepted: {
          method: 'POST',
          url: 'api/contractReports/:id/canChangeStatusToAccepted'
        },
        canChangeStatusToRefused: {
          method: 'POST',
          url: 'api/contractReports/:id/canChangeStatusToRefused'
        },
        isRegNumExisting: {
          method: 'GET',
          url: 'api/contractReports/isRegNumExisting',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        returnStatusToUnchecked: {
          method: 'POST',
          url: 'api/contractReports/:id/returnStatusToUnchecked'
        },
        canReturnStatusToUnchecked: {
          method: 'POST',
          url: 'api/contractReports/:id/canReturnStatusToUnchecked'
        },
        getSAPData: {
          method: 'GET',
          url: 'api/contractReports/:id/sapData',
          isArray: true
        }
      }
    );
  }
];
