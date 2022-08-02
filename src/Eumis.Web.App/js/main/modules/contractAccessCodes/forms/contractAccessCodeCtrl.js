function ContractAccessCodeCtrl($scope, l10n) {
  $scope.yesText = l10n.get('common_texts_yes');
  $scope.noText = l10n.get('common_texts_no');

  $scope.showAccessCode = $scope.model.showAccessCode;
}

ContractAccessCodeCtrl.$inject = ['$scope', 'l10n'];

export { ContractAccessCodeCtrl };
