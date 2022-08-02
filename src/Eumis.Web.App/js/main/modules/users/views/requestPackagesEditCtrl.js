function RequestPackagesEditCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  l10n,
  scModal,
  scConfirm,
  RequestPackage,
  requestPackage
) {
  $scope.editMode = null;
  $scope.requestPackage = requestPackage;

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editRequestPackageForm.$validate().then(function() {
      if ($scope.editRequestPackageForm.$valid) {
        return RequestPackage.update({ id: $stateParams.id }, $scope.requestPackage).$promise.then(
          function() {
            return $state.partialReload();
          }
        );
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.changeStatus = function(requestPackageStatus) {
    var validationAction = null,
      noteLbl = null,
      confirmMsg = $interpolate(l10n.get('requestPackages_edit_changeStatusConfirm'))({
        status: l10n.get('requestPackages_edit_' + requestPackageStatus)
      });
    if (requestPackageStatus === 'ended') {
      validationAction = 'canChangeStatusToEnded';
      noteLbl = 'requestPackages_edit_endedMessage';
    } else if (requestPackageStatus === 'entered') {
      validationAction = 'canChangeStatusToEntered';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLbl,
      resource: 'RequestPackage',
      validationAction: validationAction,
      action:
        'changeStatusTo' +
        requestPackageStatus.charAt(0).toUpperCase() +
        requestPackageStatus.slice(1),
      params: { id: $stateParams.id, version: $scope.requestPackage.version }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

RequestPackagesEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'l10n',
  'scModal',
  'scConfirm',
  'RequestPackage',
  'requestPackage'
];

RequestPackagesEditCtrl.$resolve = {
  requestPackage: [
    '$stateParams',
    'RequestPackage',
    function($stateParams, RequestPackage) {
      return RequestPackage.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { RequestPackagesEditCtrl };
