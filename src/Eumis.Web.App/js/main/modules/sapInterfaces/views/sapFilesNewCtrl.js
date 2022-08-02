import angular from 'angular';

function SapFilesNewCtrl($scope, $state, l10n, scModal, SapFile) {
  $scope.save = function() {
    return $scope.newSapFileForm.$validate().then(function() {
      if ($scope.newSapFileForm.$valid) {
        return SapFile.save($scope.model).$promise.then(function(result) {
          if (!!result.sapFileId) {
            return $state.go('root.sapFiles.view.edit', {
              id: result.sapFileId
            });
          } else {
            var modalInstance = scModal.open('validationErrorsModal', {
              errors: [l10n.get('sapFiles_newFile_fileInvalid')]
            });
            modalInstance.result.catch(angular.noop);
            return modalInstance.opened;
          }
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.go('root.sapFiles.search');
  };
}

SapFilesNewCtrl.$inject = ['$scope', '$state', 'l10n', 'scModal', 'SapFile'];

export { SapFilesNewCtrl };
