import _ from 'lodash';

function ScModalProvider() {
  var modals = {};

  this.modal = function modal(name, modalObj) {
    modals[name] = modalObj;

    return this;
  };

  this.$get = [
    '$uibModal',
    '$injector',
    '$exceptionHandler',
    function modalProviderFactory($uibModal, $injector, $exceptionHandler) {
      var self = this;

      self.modal = {};

      self.modal.open = function(modalName, params) {
        var modalObj = modals[modalName],
          promisesObj;

        params = params || {};

        if (!modalObj) {
          return new Error('Invalid modal ' + modalName);
        }

        promisesObj = {
          scModalParams: function() {
            return params;
          }
        };

        _.forOwn(modalObj.resolve, function(value, key) {
          promisesObj[key] = function() {
            return $injector
              .invoke(value, null, { scModalParams: params })
              ['catch'](function(error) {
                $exceptionHandler(error);
              });
          };
        });

        return $uibModal.open({
          templateUrl: modalObj.template,
          controller: modalObj.controller,
          windowClass: modalObj.size + '-modal',
          resolve: promisesObj,
          backdrop: 'static'
        });
      };

      return self.modal;
    }
  ];
}

export { ScModalProvider as scModalProvider };
