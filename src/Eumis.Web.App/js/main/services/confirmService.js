import angular from 'angular';

export const scConfirmService = [
  '$injector',
  'scModal',
  'scMessage',
  '$q',
  function($injector, scModal, scMessage, $q) {
    return function(scConfirmParams) {
      function doAction(note) {
        if (scConfirmParams.action !== null && scConfirmParams.action !== undefined) {
          var postData = {};
          if (scConfirmParams.data) {
            postData = angular.extend({}, postData, scConfirmParams.data);
          }
          if (note) {
            postData = angular.extend({}, postData, { note: note });
          }
          return $injector
            .get(scConfirmParams.resource)
            [scConfirmParams.action](scConfirmParams.params, postData)
            .$promise.then(function(result) {
              return {
                executed: true,
                result: result
              };
            });
        } else {
          return $q.resolve({
            executed: true,
            result: null
          });
        }
      }

      function validate(note) {
        if (scConfirmParams.validationAction) {
          var postData = {};
          if (scConfirmParams.data) {
            postData = angular.extend({}, postData, scConfirmParams.data);
          }

          return $injector
            .get(scConfirmParams.resource)
            [scConfirmParams.validationAction](scConfirmParams.params, postData)
            .$promise.then(function(result) {
              if (result.errors && result.errors.length) {
                return scModal
                  .open('validationErrorsModal', {
                    errors: result.errors
                  })
                  .result.catch(function(resp) {
                    // skip modal dismissals which are not errors but still trigger a rejection
                    if (['cancel', 'backdrop click', 'escape key press'].indexOf(resp) === -1) {
                      throw resp;
                    }
                  })
                  .then(function() {
                    return {
                      executed: false,
                      result: null
                    };
                  });
              } else {
                return doAction(note);
              }
            });
        } else {
          return doAction(note);
        }
      }

      if (scConfirmParams.confirmMessage) {
        if (scConfirmParams.noteLabel) {
          return scModal
            .open('messageNoteModal', {
              confirmMessage: scConfirmParams.confirmMessage,
              noteLabel: scConfirmParams.noteLabel
            })
            .result.catch(function(resp) {
              if (['cancel', 'backdrop click', 'escape key press'].indexOf(resp) === -1) {
                return resp;
              }
            })
            .then(function(result) {
              if (result && result.confirm) {
                return validate(result.note);
              } else {
                return {
                  executed: false,
                  result: null
                };
              }
            }, angular.noop);
        } else {
          return scMessage(scConfirmParams.confirmMessage).then(function(result) {
            if (result === 'OK') {
              return validate(null);
            } else {
              return {
                executed: false,
                result: null
              };
            }
          });
        }
      } else {
        return validate(null);
      }
    };
  }
];
