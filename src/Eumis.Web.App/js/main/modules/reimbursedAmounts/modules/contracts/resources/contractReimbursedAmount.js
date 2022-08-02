export const ContractReimbursedAmountFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contarctReimbursedAmounts/:id',
      {},
      {
        newReimbursedAmount: {
          method: 'GET',
          url: 'api/contarctReimbursedAmounts/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contarctReimbursedAmounts/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/contarctReimbursedAmounts/:id/data'
        },
        enterAmount: {
          method: 'POST',
          url: 'api/contarctReimbursedAmounts/:id/enter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/contarctReimbursedAmounts/:id/setToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/contarctReimbursedAmounts/:id/setToRemoved'
        },
        getContracts: {
          method: 'GET',
          url: 'api/reimbursedAmountContracts',
          isArray: true
        },
        attachToContractDebt: {
          method: 'POST',
          url: 'api/contarctReimbursedAmounts/:id/attachToContractDebt'
        }
      }
    );
  }
];
