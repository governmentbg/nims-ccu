/*global angular, require, _*/
(function (angular, require, _) {
  'use strict';

  angular.module('app').config(function ($httpBackendConfiguratorProvider) {
    $httpBackendConfiguratorProvider
      .when('GET', 'api/nomenclatures/:alias?term',
        function ($http, $params, $filter) {
          return [
            200,
            $http.get('nomenclatures/' + $params.alias + '.js').then(function (response) {
              return $filter('filter')(response.data, {
                name: $params.term
              });
            })
          ];
        });
  });
}(angular, _));
