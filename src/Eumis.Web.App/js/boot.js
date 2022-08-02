import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import _ from 'lodash';
import ScaffoldingModule from 'js/scaffolding/scaffolding';

var controllers = {},
  states = {},
  modals = {};

const BootModule = angular
  .module('boot', [ScaffoldingModule, UiRouterModule])
  .config([
    '$controllerProvider',
    '$stateProvider',
    'scModalProvider',
    '$provide',
    function($controllerProvider, $stateProvider, scModalProvider, $provide) {
      $controllerProvider.register = _.wrap($controllerProvider.register, function(
        original,
        name,
        constructor
      ) {
        if (angular.isObject(name)) {
          angular.extend(controllers, name);
        } else {
          controllers[name] = constructor;
        }

        return original.apply($controllerProvider, [name, constructor]);
      });

      scModalProvider.modal = _.wrap(scModalProvider.modal, function(
        original,
        name,
        template,
        controller,
        size
      ) {
        var modalObj = {
          template: template,
          controller: controller,
          size: size || 'xlg'
        };

        modals[name] = modalObj;

        return original.apply(scModalProvider, [name, modalObj]);
      });

      $stateProvider.state = _.wrap($stateProvider.state, function(original, name, definition) {
        var stateArray = name,
          views = {},
          isAbstract,
          i,
          l;

        if (angular.isArray(name)) {
          if (stateArray.length < 2) {
            throw new Error('State arrays must contain at least 2 items.');
          }

          if (stateArray[2] === true) {
            isAbstract = true;
            i = 3;
          } else {
            isAbstract = stateArray.length === 2;
            i = 2;
          }

          for (l = stateArray.length; i < l; i++) {
            views[stateArray[i][0]] = {
              templateUrl: stateArray[i][1],
              controller: stateArray[i][2]
            };
          }

          return original.apply($stateProvider, [
            {
              name: stateArray[0],
              url: stateArray[1],
              views: views,
              abstract: isAbstract
            }
          ]);
        } else {
          return original.apply($stateProvider, [name, definition]);
        }
      });

      $stateProvider.decorator('resolve', function(state) {
        states[state.self.name] = state;
        return state.resolve;
      });

      $provide.decorator('$state', [
        '$delegate',
        function($state) {
          var originalGo;

          $state.getWrapper = function(stateName) {
            return states[stateName];
          };

          originalGo = $state.go;
          $state.go = function(to, params, options) {
            params = _.assign(params || {}, { rf: Date.parse(Date()) });

            return originalGo.apply($state, [to, params, options]);
          };

          //Add partialReload method
          $state.partialReload = function() {
            return $state.go($state.$current, { rf: Date.parse(Date()) }, { location: 'replace' });
          };
          return $state;
        }
      ]);
    }
  ])
  .run([
    function() {
      _.forOwn(states, function(state) {
        if (
          (!state.self.url || state.self.url.indexOf('?') === 0) &&
          state.parent &&
          state.parent['abstract']
        ) {
          state.parent.defaultChild = state;
        }

        if (_.isEmpty(state.resolve)) {
          _.forOwn(state.views, function(view) {
            var resolve;
            if (view.controller) {
              resolve =
                (controllers[view.controller] && controllers[view.controller].$resolve) ||
                view.controller.$resolve;
              if (resolve) {
                _.assign(state.resolve, resolve);
              }
            }
          });
        }
      });
    }
  ])
  .run([
    '$rootScope',
    '$state',
    '$location',
    '$window',
    function($rootScope, $state, $location, $window) {
      $rootScope.$on('$stateChangeSuccess', function(event, toState, toParams, fromState) {
        $state.previous = $state.getWrapper($state.get(fromState).name);

        // Send data to Google Analytics
        $window.ga('set', 'page', $location.url());
        $window.ga('send', 'pageview');
      });
    }
  ])
  .run([
    function() {
      _.forOwn(modals, function(modal) {
        var resolve;
        if (modal.controller) {
          resolve =
            (controllers[modal.controller] && controllers[modal.controller].$resolve) ||
            modal.controller.$resolve;
          if (resolve) {
            modal.resolve = resolve;
          }
        }
      });
    }
  ]);

export default BootModule.name;
