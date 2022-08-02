import _ from 'lodash';
import 'jquery-ui/ui/jquery.ui.widget';
import 'jquery-ui/ui/jquery.ui.core';
import 'jquery-ui/ui/jquery.ui.mouse';
import 'jquery-ui/ui/jquery.ui.sortable';
import 'blueimp-file-upload/js/jquery.iframe-transport';
import 'blueimp-file-upload/js/jquery.fileupload';

function FileCtrl($q, $exceptionHandler, $scope, l10n, $timeout, scFileConfig, accessToken) {
  $scope.upload = undefined;
  $scope.uploadUrl = scFileConfig.uploadUrl;

  $scope.setFile = function(file, uploaderToken) {
    var url;
    if (file) {
      if (!_.has(file, 'url')) {
        if (uploaderToken) {
          //we have just uploaded a file and the blob server has given us a token for that file
          url = scFileConfig.uploadUrl + file.key + '?access_token=' + uploaderToken;
        } else {
          url = $scope.urlTemplate.getUrl(
            _.extend(
              {
                fileKey: file.key,
                access_token: accessToken.get()
              },
              $scope.urlParams()
            )
          );
        }
      } else {
        url = file.url;
      }

      $scope.file = {
        name: file.name,
        url: url
      };
    } else {
      $scope.file = undefined;
    }
  };

  $scope.add = function(e, fileData) {
    if ($scope.isReadonly) {
      return;
    }

    //if the file size is too big, make a fake file and set it(change the ngModel) to trigger
    //the blob size validation function(validateBlobSize, added in the ngModel's $parsers and $formatters)
    //and then return, because we don't want to upload files with size, larger than the allowed
    if (fileData.files[0].size > window.eumisConfiguration.maxBlobSize) {
      var fakeFile = {
        name: fileData.files[0].name,
        size: fileData.files[0].size,
        url: '#'
      };

      $scope.setViewValue(fakeFile);
      $scope.setFile(fakeFile);

      return;
    }

    var jqXHR = fileData.submit(),
      deferred = $q.defer();

    $scope.$apply(function() {
      $scope.upload = jqXHR;
      $scope.percent = 0;
    });

    jqXHR.then(
      function(data) {
        deferred.resolve(data);
      },
      function(jqXHR, textStatus, errorThrown) {
        deferred.reject(errorThrown);
      }
    );

    deferred.promise
      .then(function(data) {
        if (data.fileKey && data.accessToken) {
          var file = {
            name: fileData.files[0].name,
            key: data.fileKey
          };

          $scope.setViewValue(file);
          $scope.setFile(file, data.accessToken);
        }
      })
      ['catch'](function(error) {
        if (error !== 'abort') {
          $exceptionHandler(error);
        }
      })
      ['finally'](function() {
        $scope.upload = undefined;
      });
  };

  $scope.remove = function() {
    if (!$scope.isReadonly) {
      if ($scope.upload) {
        $scope.upload.abort();
      } else {
        $scope.setViewValue(undefined);
        $scope.setFile(undefined);
      }
    }
  };

  $scope.progress = function(e, data) {
    if (!$scope.upload || $scope.upload.readyState === 4) {
      //DONE
      return;
    }

    var progress = data.progress();
    if (progress && progress.total) {
      //we force our handler to always run asynchronously by using the $timeout service and
      //by providing a timeout period of 0ms, this will occur as soon as possible and
      //$timeout will ensure that the code will be called in a single $apply block
      $timeout(function() {
        $scope.percent = Math.floor(100 * (progress.loaded / progress.total));
      }, 0);
    }
  };
}

FileCtrl.prototype.setNgModelCtrl = function(ngModel, $scope) {
  $scope.setViewValue = function(value) {
    ngModel.$setViewValue(value);
  };

  ngModel.$render = function() {
    $scope.setFile(ngModel.$viewValue);
  };

  $scope.isInvalid = function() {
    return ngModel.$invalid;
  };
};

FileCtrl.$inject = [
  '$q',
  '$exceptionHandler',
  '$scope',
  'l10n',
  '$timeout',
  'scFileConfig',
  'accessToken'
];

export { FileCtrl };
