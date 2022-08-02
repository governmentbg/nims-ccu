export const NomenclaturesFactory = [
  '$resource',
  function($resource) {
    return $resource('api/nomenclatures/:alias/:id/:valueAlias');
  }
];
