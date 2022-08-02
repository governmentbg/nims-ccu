export const ProcedureShareExpenseBudgetFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/procedures/:id/shareExpenseBudgets',
      {},
      {
        getTree: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/tree'
        },
        //Level1 methods
        newLevel1: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/newLevel1'
        },
        addLevel1: {
          method: 'POST',
          url: 'api/procedures/:id/shareExpenseBudgets/level1'
        },
        deleteLevel1: {
          method: 'DELETE',
          url: 'api/procedures/:id/shareExpenseBudgets/level1/:ind'
        },
        deactivateLevel1: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/level1/:ind/deactivate'
        },
        activateLevel1: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/level1/:ind/activate'
        },
        canDeleteLevel1: {
          method: 'POST',
          url: 'api/procedures/:id/shareExpenseBudgets/level1/:ind/canDeleteLevel1'
        },
        //Level2 methods
        newLevel2: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/newLevel2'
        },
        addLevel2: {
          method: 'POST',
          url: 'api/procedures/:id/shareExpenseBudgets/level2'
        },
        getLevel2: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/level2/:ind'
        },
        editLevel2: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/level2/:ind'
        },
        deleteLevel2: {
          method: 'DELETE',
          url: 'api/procedures/:id/shareExpenseBudgets/level2/:ind'
        },
        deactivateLevel2: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/level2/:ind/deactivate'
        },
        activateLevel2: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/level2/:ind/activate'
        },
        canDeleteLevel2: {
          method: 'POST',
          url: 'api/procedures/:id/shareExpenseBudgets/level2/:ind/canDeleteLevel2'
        },
        //Level3 methods
        newLevel3: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/newLevel3'
        },
        addLevel3: {
          method: 'POST',
          url: 'api/procedures/:id/shareExpenseBudgets/level3'
        },
        getLevel3: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/level3/:ind'
        },
        editLevel3: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/level3/:ind'
        },
        deleteLevel3: {
          method: 'DELETE',
          url: 'api/procedures/:id/shareExpenseBudgets/level3/:ind'
        },
        //ValidationRule methods
        newValidationRule: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/newValidationRule'
        },
        addValidationRule: {
          method: 'POST',
          url: 'api/procedures/:id/shareExpenseBudgets/validationRule'
        },
        getValidationRule: {
          method: 'GET',
          url: 'api/procedures/:id/shareExpenseBudgets/validationRule/:ind'
        },
        editValidationRule: {
          method: 'PUT',
          url: 'api/procedures/:id/shareExpenseBudgets/validationRule/:ind'
        },
        deleteValidationRule: {
          method: 'DELETE',
          url: 'api/procedures/:id/shareExpenseBudgets/validationRule/:ind'
        }
      }
    );
  }
];
