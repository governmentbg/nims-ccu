export const SapCertReportFactory = [
  '$resource',
  function($resource) {
    return $resource('api/sapCertReports/:id');
  }
];
