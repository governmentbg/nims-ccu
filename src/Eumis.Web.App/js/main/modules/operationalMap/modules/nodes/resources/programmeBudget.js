export const ProgrammeBudgetFactory = [
  '$resource',
  function($resource) {
    return $resource('api/programmes/:id/budgets/:ind');
  }
];
