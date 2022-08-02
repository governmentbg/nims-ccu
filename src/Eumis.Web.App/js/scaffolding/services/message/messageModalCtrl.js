function MessageModalCtrl($scope, $uibModalInstance, l10nText, buttons) {
  $scope.l10nText = l10nText;
  $scope.buttons = buttons;

  $scope.close = function(result) {
    return $uibModalInstance.close(result);
  };
}

MessageModalCtrl.$inject = ['$scope', '$uibModalInstance', 'l10nText', 'buttons'];

export { MessageModalCtrl };
