export const ProgrammePriorityBudgetFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/programmePriorities/:id/budgets/:ind',
      {},
      {
        newBudget: {
          method: 'GET',
          url: 'api/programmePriorities/:id/budgets/new'
        }
      }
    );
  }
];
