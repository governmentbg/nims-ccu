/*global angular, _*/
(function (angular) {
  'use strict';

  function SampleDataFilter($http) {
    var cache = {};

    return function (sampleDataKey) {
      if (cache[sampleDataKey]) {
        return cache[sampleDataKey];
      }

      cache[sampleDataKey] = {};

      $http.get('sampleData/' + sampleDataKey + '.js').then(function(response){
        cache[sampleDataKey] = response.data;
      });

      return {};
    };
  }

  SampleDataFilter.$inject = ['$http'];

  angular.module('app').filter('sampleData', SampleDataFilter);

}(angular));
