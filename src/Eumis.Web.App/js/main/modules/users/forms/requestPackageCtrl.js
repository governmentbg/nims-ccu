import _ from 'lodash';

function RequestPackageCtrl($scope, scModal, scFormParams) {
  $scope.isNew = scFormParams.isNew;
  $scope.isDirect = scFormParams.isDirect;
  $scope.maxDocumentsAllowed = 5;
  $scope.shownDocumentsIndexes = [];
  $scope.notShownDocumentsIndexes = [];

  function denseFilesDescs() {
    var i, j;
    for (i = 1; i < 5; i++) {
      if (!$scope.model['file' + i]) {
        for (j = i + 1; j < 6; j++) {
          if ($scope.model['file' + j]) {
            $scope.model['file' + i] = $scope.model['file' + j];
            $scope.model['description' + i] = $scope.model['description' + j];
            $scope.model['file' + j] = null;
            $scope.model['description' + j] = null;
            break;
          }
        }
      }
    }
  }

  denseFilesDescs();

  _.forEach([1, 2, 3, 4, 5], function(index) {
    if ($scope.model['file' + index]) {
      $scope.shownDocumentsIndexes.push(index);
    } else {
      $scope.notShownDocumentsIndexes.push(index);
    }
  });

  $scope.addDocument = function() {
    $scope.shownDocumentsIndexes.push($scope.notShownDocumentsIndexes.shift());
  };

  $scope.deleteDocument = function(index) {
    $scope.model['file' + index] = null;
    $scope.model['description' + index] = null;

    if (index !== $scope.shownDocumentsIndexes[$scope.shownDocumentsIndexes.length - 1]) {
      denseFilesDescs();
    }
    $scope.notShownDocumentsIndexes.push($scope.shownDocumentsIndexes.pop());
    $scope.notShownDocumentsIndexes.sort();
  };

  $scope.docFileChange = function(index) {
    $scope.model['description' + index] = null;
  };
}

RequestPackageCtrl.$inject = ['$scope', 'scModal', 'scFormParams'];

export { RequestPackageCtrl };
