// Usage:
// <sc-file path= resources= ng-model="">
// </sc-file>

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('scFile', ['$q', function ($q) {
        return {
            restrict: 'E',
            priority: 10,
            replace: true,
            scope: {
                model: '=ngModel',
                path: '@',
                resources: '=',
                url: '@'
            },
            templateUrl: '/Scripts/angular/directives/file/fileDirective.html',
            link: {
                pre: function (scope, element, attrs) {

                    scope.routeSession = window._eumis_options.session;

                    scope.showLinks = attrs.showLinks ? (attrs.showLinks == 'true') : true;

                    scope.progress = 0;
                    scope.showUploader = (scope.model.BlobContentId || '').length <= 0;
                    
                    var updateUI;
                    scope.add = function (e, data) {
                        var filename = data.files[0].name;

                        updateUI = $q.defer();
                        updateUI.promise.then(function (data) {
                            scope.model.FileName = filename;
                            scope.model.BlobContentId = data.result.fileKey;
                            scope.model.Hash = data.result.hash;
                            scope.model.Size = data.result.size;
                            scope.model.UploaderAccessToken = data.result.accessToken;
                            scope.showUploader = (data.result.fileKey || '').length <= 0;
                            scope.fileError = false;
                            scope.fileExceedsMaximumSize = false;
                        }, function (error) {
                            if (error == 'file_exceeded_maximum_size') {
                                scope.fileError = false;
                                scope.fileExceedsMaximumSize = true;
                            }
                            else {
                                scope.fileError = true;
                                scope.fileExceedsMaximumSize = false;
                            }
                            scope.showUploader = true;
                            scope.progress = 0;
                            scope.model.BlobContentId = null;
                            scope.model.FileName = null;
                            scope.model.Hash = null;
                            scope.model.Size = null;
                        });

                        if (data.files[0].size > window._eumis_options.attachedDocumentMaxSize) {
                            updateUI.reject('file_exceeded_maximum_size');
                        }
                        else {
                            scope.fileError = false;
                            scope.fileExceedsMaximumSize = false;
                            data.submit();
                        }
                    };

                    scope.done = function (e, data) {
                        updateUI.resolve(data);
                    };

                    scope.fail = function (e, error) {
                        updateUI.reject(error);
                    };

                    scope.progressall = function (e, data) {
                        scope.$apply(function () {
                            scope.progress = parseInt(data.loaded / data.total * 100, 10);
                        });
                    };

                    scope.delete = function () {
                        scope.showUploader = true;
                        scope.progress = 0;
                        scope.model.BlobContentId = null;
                        scope.model.FileName = null;
                        scope.model.Hash = null;
                        scope.model.Size = null;
                    };

                    scope.options = {
                        dataType: "json",
                        dropZone: $(this),
                        url: scope.url,
                        add: scope.add,
                        done: scope.done,
                        fail: scope.fail,
                        progressall: scope.progressall
                    };
                }
            }
        }
    }]);
}(angular));
