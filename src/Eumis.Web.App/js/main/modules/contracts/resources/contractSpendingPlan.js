export const ContractSpendingPlanFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contracts/:id/spendingPlans/:spid',
      {},
      {
        newContractSpendingPlan: {
          method: 'GET',
          url: 'api/contracts/:id/spendingPlans/new'
        },
        markAsChecked: {
          method: 'POST',
          url: 'api/contracts/:id/spendingPlans/:spid/markAsChecked'
        },
        changeStatusToDraft: {
          method: 'POST',
          url: 'api/contracts/:id/spendingPlans/:spid/changeStatusToDraft'
        },
        canCreate: {
          method: 'POST',
          url: 'api/contracts/:id/spendingPlans/canCreate'
        }
      }
    );
  }
];
