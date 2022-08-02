export const ContractReportRevalidationFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportRevalidations/:id',
      {},
      {
        newContractReportRevalidation: {
          method: 'GET',
          url: 'api/contractReportRevalidations/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportRevalidations/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/contractReportRevalidations/:id/data'
        },
        enter: {
          method: 'POST',
          url: 'api/contractReportRevalidations/:id/enter'
        },
        canEnter: {
          method: 'POST',
          url: 'api/contractReportRevalidations/:id/canEnter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/contractReportRevalidations/:id/setToDraft'
        },
        canSetToDraft: {
          method: 'POST',
          url: 'api/contractReportRevalidations/:id/canSetToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/contractReportRevalidations/:id/setToRemoved'
        },
        getCertReportRevalidationsContractReportRevalidation: {
          method: 'GET',
          url: 'api/certReports/:id/revalidations/:ind/revalidation'
        },
        certUpdate: {
          method: 'PUT',
          url: 'api/certReports/:id/revalidations/:ind/certUpdate'
        },
        changeCertStatusToEnded: {
          method: 'POST',
          url: 'api/certReports/:id/revalidations/:ind/changeCertStatusToEnded'
        },
        changeCertStatusToDraft: {
          method: 'POST',
          url: 'api/certReports/:id/revalidations/:ind/changeCertStatusToDraft'
        },
        canChangeCertStatusToEnded: {
          method: 'POST',
          url: 'api/certReports/:id/revalidations/:ind/canChangeCertStatusToEnded'
        }
      }
    );
  }
];
