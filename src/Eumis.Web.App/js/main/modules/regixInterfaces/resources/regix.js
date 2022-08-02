export const RegixFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/regix/',
      {},
      {
        personValid: {
          method: 'POST',
          url: 'api/regix/personValid'
        },
        personalIdentity: {
          method: 'POST',
          url: 'api/regix/personalIdentity'
        },
        actualState: {
          method: 'POST',
          url: 'api/regix/actualState'
        },
        stateOfPlay: {
          method: 'POST',
          url: 'api/regix/stateOfPlay'
        },
        npoRegistration: {
          method: 'POST',
          url: 'api/regix/npoRegistrationInfo'
        }
      }
    );
  }
];
