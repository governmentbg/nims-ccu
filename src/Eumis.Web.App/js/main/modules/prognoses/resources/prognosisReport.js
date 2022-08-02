export const PrognosisReportFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/prognosisReports/:id',
      {},
      {
        getYearlyPrognoses: {
          url: 'api/prognosisReports/yearlyPrognoses',
          method: 'GET',
          isArray: true
        },
        getMonthlyPrognoses: {
          url: 'api/prognosisReports/monthlyPrognoses',
          method: 'GET',
          isArray: true
        },
        getProgrammePriorityPrognoses: {
          url: 'api/prognosisReports/programmePriorityPrognoses',
          method: 'GET',
          isArray: true
        },
        getProgrammePrognoses: {
          url: 'api/prognosisReports/programmePrognoses',
          method: 'GET',
          isArray: true
        },
        getSummary: {
          url: 'api/prognosisReports/summary',
          method: 'GET',
          isArray: true
        }
      }
    );
  }
];
