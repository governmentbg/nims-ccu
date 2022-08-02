export const EuReimbursedAmountCertReportFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/euReimbursedAmounts/:id/certReports/:ind',
      {},
      {
        getCertReports: {
          method: 'GET',
          url: 'api/certReportEuReimbursedAmounts/:id',
          isArray: true
        }
      }
    );
  }
];
