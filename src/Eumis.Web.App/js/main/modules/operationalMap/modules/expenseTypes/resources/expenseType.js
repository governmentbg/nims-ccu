export const ExpenseTypeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/expenseTypes/:id',
      {},
      {
        newExpenseType: {
          method: 'GET',
          url: 'api/expenseTypes/new'
        },
        deactivate: {
          method: 'PUT',
          url: 'api/expenseTypes/:id/deactivate'
        },
        activate: {
          method: 'PUT',
          url: 'api/expenseTypes/:id/activate'
        },
        canDelete: {
          method: 'POST',
          url: 'api/expenseTypes/:id/canDelete'
        }
      }
    );
  }
];
