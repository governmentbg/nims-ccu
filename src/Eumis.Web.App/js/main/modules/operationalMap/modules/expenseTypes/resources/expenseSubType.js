export const ExpenseSubTypeFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/expenseTypes/:id/expenseSubTypes/:ind',
      {},
      {
        newExpenseSubType: {
          method: 'GET',
          url: 'api/expenseTypes/:id/expenseSubTypes/new'
        }
      }
    );
  }
];
