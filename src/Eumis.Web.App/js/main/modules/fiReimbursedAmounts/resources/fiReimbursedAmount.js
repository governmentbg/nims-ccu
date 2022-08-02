export const FIReimbursedAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/fiReimbursedAmounts/:id',
      {},
      {
        newReimbursedAmount: {
          method: 'GET',
          url: 'api/fiReimbursedAmounts/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/fiReimbursedAmounts/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/fiReimbursedAmounts/:id/data'
        },
        enterAmount: {
          method: 'POST',
          url: 'api/fiReimbursedAmounts/:id/enter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/fiReimbursedAmounts/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/fiReimbursedAmounts/:id/setToRemoved'
        }
      }
    );
  }
];
