import angular from 'angular';
import _ from 'lodash';

function RequestPackageUsersCtrl(
  $scope,
  $state,
  $stateParams,
  $interpolate,
  l10n,
  scModal,
  scConfirm,
  RequestPackage,
  RequestPackageUser,
  requestPackage,
  requestPackageUsers
) {
  $scope.requestPackage = requestPackage;
  $scope.requestPackageUsers = requestPackageUsers;

  $scope.activeText = l10n.get('users_search_active');
  $scope.inactiveText = l10n.get('users_search_inactive');
  $scope.lockedText = l10n.get('users_search_locked');
  $scope.deletedText = l10n.get('users_search_deleted');

  $scope.chooseUsers = function() {
    var modalInstance,
      previouslyChosenUserIds = _.map($scope.requestPackageUsers, 'userId');

    modalInstance = scModal.open('chooseUsersModal', {
      previouslyChosenUserIds: previouslyChosenUserIds,
      resourceName: 'RequestPackageUser',
      rootId: requestPackage.requestPackageId,
      userOrganizationId: requestPackage.userOrganizationId,
      version: requestPackage.version
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.createAndChooseUser = function() {
    var modalInstance = scModal.open('createAndChooseUserModal', {
      userOrganizationId: requestPackage.userOrganizationId
    });

    modalInstance.result.then(function(user) {
      return RequestPackageUser.save(
        {
          id: requestPackage.requestPackageId,
          version: requestPackage.version
        },
        [user.userId]
      ).$promise.then(function() {
        return $state.partialReload();
      });
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteUser = function(user) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'RequestPackageUser',
      action: 'remove',
      params: {
        id: $scope.requestPackage.requestPackageId,
        ind: user.userId,
        version: $scope.requestPackage.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.regDataRequestHandler = function(user) {
    var modalInstance = scModal.open('regDataRequestModal', {
      user: user,
      version: $scope.requestPackage.version,
      isReadonly: $scope.requestPackage.status !== 'draft'
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deleteRegDataRequest = function(userId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'RegDataRequest',
      action: 'remove',
      params: {
        id: $scope.requestPackage.requestPackageId,
        ind: userId,
        version: $scope.requestPackage.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.permissionRequestHandler = function(user) {
    var modalInstance = scModal.open('permissionRequestModal', {
      user: user,
      version: $scope.requestPackage.version,
      isReadonly: $scope.requestPackage.status !== 'draft'
    });

    modalInstance.result.then(function() {
      return $state.partialReload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.deletePermissionRequest = function(userId) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'PermissionRequest',
      action: 'remove',
      params: {
        id: $scope.requestPackage.requestPackageId,
        ind: userId,
        version: $scope.requestPackage.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };

  $scope.changeRequestStatus = function(user, requestStatus) {
    var noteLbl = null,
      confirmMsg = null;
    if (requestStatus === 'active') {
      confirmMsg = l10n.get('requestPackages_requestPackageUsers_active');
    } else if (requestStatus === 'checked') {
      confirmMsg = l10n.get('requestPackages_requestPackageUsers_checked');
    } else if (requestStatus === 'rejected') {
      confirmMsg = l10n.get('requestPackages_requestPackageUsers_rejected');
      noteLbl = 'requestPackages_requestPackageUsers_rejectionMessage';
    }

    return scConfirm({
      confirmMessage: confirmMsg,
      noteLabel: noteLbl,
      resource: 'RequestPackageUser',
      action: 'changeStatusTo' + requestStatus.charAt(0).toUpperCase() + requestStatus.slice(1),
      params: {
        id: user.requestPackageId,
        userId: user.userId,
        version: $scope.requestPackage.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

RequestPackageUsersCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'l10n',
  'scModal',
  'scConfirm',
  'RequestPackage',
  'RequestPackageUser',
  'requestPackage',
  'requestPackageUsers'
];

RequestPackageUsersCtrl.$resolve = {
  requestPackage: [
    '$stateParams',
    'RequestPackage',
    function($stateParams, RequestPackage) {
      return RequestPackage.get({ id: $stateParams.id }).$promise;
    }
  ],
  requestPackageUsers: [
    '$stateParams',
    'RequestPackageUser',
    function($stateParams, RequestPackageUser) {
      return RequestPackageUser.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { RequestPackageUsersCtrl };
