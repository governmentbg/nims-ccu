import angular from 'angular';

function UserRequestsSearchCtrl($scope, $state, l10n, scModal, UserRequest, userRequestsWrapper) {
  $scope.userRequestsWrapper = userRequestsWrapper;

  $scope.viewPermissionRequest = function(user) {
    var modalInstance = scModal.open('permissionRequestModal', {
      user: user,
      isReadonly: true
    });
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };

  $scope.viewRegDataRequest = function(user) {
    var modalInstance = scModal.open('regDataRequestModal', {
      user: user,
      isReadonly: true
    });
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };
}

UserRequestsSearchCtrl.$inject = [
  '$scope',
  '$state',
  'l10n',
  'scModal',
  'UserRequest',
  'userRequestsWrapper'
];

UserRequestsSearchCtrl.$resolve = {
  userRequestsWrapper: [
    '$stateParams',
    'UserRequest',
    function($stateParams, UserRequest) {
      return UserRequest.get({ id: $stateParams.id }).$promise;
    }
  ]
};

export { UserRequestsSearchCtrl };
