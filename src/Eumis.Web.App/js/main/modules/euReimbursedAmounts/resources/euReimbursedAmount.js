export const EuReimbursedAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/euReimbursedAmounts/:id',
      {},
      {
        newAmount: {
          method: 'GET',
          url: 'api/euReimbursedAmounts/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/euReimbursedAmounts/:id/info'
        },
        enter: {
          method: 'POST',
          url: 'api/euReimbursedAmounts/:id/enter'
        },
        canEnter: {
          method: 'POST',
          url: 'api/euReimbursedAmounts/:id/canEnter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/euReimbursedAmounts/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/euReimbursedAmounts/:id/setToRemoved'
        }
      }
    );
  }
];
