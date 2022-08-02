export const CompanyFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/companies/:id',
      {},
      {
        newCompany: {
          method: 'GET',
          url: 'api/companies/new'
        },
        canCreate: {
          method: 'POST',
          url: 'api/companies/canCreate'
        },
        canDelete: {
          method: 'POST',
          url: 'api/companies/:id/canDelete'
        },
        isUniqueUin: {
          method: 'GET',
          url: 'api/companies/isUniqueUin',
          interceptor: {
            response: function(response) {
              return response.data;
            }
          }
        },
        getCompanyByUin: {
          method: 'GET',
          url: 'api/companies/getCompanyByUin'
        }
      }
    );
  }
];
