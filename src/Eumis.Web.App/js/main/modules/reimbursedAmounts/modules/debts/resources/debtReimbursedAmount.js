export const DebtReimbursedAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/debtReimbursedAmounts/:id',
      {},
      {
        newReimbursedAmount: {
          method: 'GET',
          url: 'api/debtReimbursedAmounts/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/debtReimbursedAmounts/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/debtReimbursedAmounts/:id/data'
        },
        enterAmount: {
          method: 'POST',
          url: 'api/debtReimbursedAmounts/:id/enter'
        },
        canEnterAmount: {
          method: 'POST',
          url: 'api/debtReimbursedAmounts/:id/canEnter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/debtReimbursedAmounts/:id/setToDraft'
        },
        canSetToDraft: {
          method: 'POST',
          url: 'api/debtReimbursedAmounts/:id/canSetToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/debtReimbursedAmounts/:id/setToRemoved'
        }
      }
    );
  }
];
