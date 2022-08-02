export const MyEvalSessionSheetFactory = [
  '$resource',
  function($resource) {
    return $resource('api/myEvalSessions/:id/sheets/:ind');
  }
];
