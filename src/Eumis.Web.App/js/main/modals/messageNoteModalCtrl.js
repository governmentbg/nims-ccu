function MessageNoteModalCtrl($scope, $uibModalInstance, scModalParams, l10n) {
  $scope.model = {};
  $scope.l10nConfirmMessage = l10n.get(scModalParams.confirmMessage);
  $scope.l10nNoteLabel = l10n.get(scModalParams.noteLabel);

  $scope.ok = function() {
    return $scope.messageNoteForm.$validate().then(function() {
      if ($scope.messageNoteForm.$valid) {
        return $uibModalInstance.close({
          confirm: true,
          note: $scope.model.note
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.close({
      confirm: false
    });
  };
}

MessageNoteModalCtrl.$inject = ['$scope', '$uibModalInstance', 'scModalParams', 'l10n'];

export { MessageNoteModalCtrl };
