import angular from 'angular';

const AuthorizerModule = angular.module('authorizer', []).factory('authorizerService', [
  '$q',
  '$injector',
  function($q, $injector) {
    var $http,
      $exceptionHandler,
      cache = {};

    function getHttp() {
      //service initialized later because of circular dependency problem.
      $http = $http || $injector.get('$http');
    }

    function getExceptionHandler() {
      //service initialized later because of circular dependency problem.
      $exceptionHandler = $exceptionHandler || $injector.get('$exceptionHandler');
    }

    function AuthorizerService() {}

    AuthorizerService.prototype.canDo = function(action, id) {
      var key;

      if (id) {
        key = action + '#' + id;
      } else {
        key = action;
      }

      if ({}.hasOwnProperty.call(cache, key)) {
        return cache[key];
      }

      cache[key] = false;

      AuthorizerService.prototype.canDoRequest(action, id).then(
        function(canDo) {
          cache[key] = canDo;
        },
        function() {
          delete cache[key];
        }
      );

      return cache[key];
    };

    AuthorizerService.prototype.canDoRequest = function(action, id) {
      getHttp();
      return $http({
        method: 'GET',
        url: 'api/authorizer/cando',
        params: {
          action: action,
          id: id
        }
      }).then(
        function(response) {
          return response.data;
        },
        function(error) {
          getExceptionHandler();
          $exceptionHandler(error);
        }
      );
    };

    return new AuthorizerService();
  }
]);

export default AuthorizerModule.name;
