/*global angular*/
(function (angular) {
    'use strict';

    angular
        .module('scaffolding', ['ngResource', 'ui.select2', 'ngAnimate', 'ui.jq', 'utils'])
        .factory('appcontext', ['$http', '$window', function ($http, $window) {
            return {
                save: function (deferred, alias, data, params) {
                    var route = $window['_eumis_options']['session'];

                    return $http({
                        data: data,
                        method: 'POST',
                        params: params,
                        url: '/api/' + route + '/appcontext/' + alias
                    }).then(function (result) {
                        if (result && result.data === false) {
                            deferred.reject();
                        }
                        else {
                            deferred.resolve();
                        }
                    }, function () {
                        deferred.reject();
                    });
                }
            };
        }])
        .filter('money', function () {
            return function formatSpaces(input) {
                var num = input;
                if (!num)
                    return;

                var str = num.toString().split('.');
                if (str[0].length >= 4) {
                    str[0] = str[0].replace(/(\d)(?=(\d{3})+$)/g, '$1 ');
                }

                if (str.length == 1) {
                    str[1] = '00';
                }
                else if (str[1].length == 1) {
                    str[1] += '0';
                }

                return str.join('.');
            }
        });
}(angular));