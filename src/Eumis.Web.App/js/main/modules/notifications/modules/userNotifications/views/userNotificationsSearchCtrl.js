import angular from 'angular';
import _ from 'lodash';

function UserNotificationsSearchCtrl(
  $scope,
  $state,
  userNotifications,
  UserNotification,
  scConfirm,
  scModal,
  $stateParams
) {
  $scope.userNotifications = userNotifications;

  $scope.deleteNotification = function(item) {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'UserNotification',
      action: 'remove',
      params: {
        id: item.userNotificationId,
        version: item.version
      }
    }).then(function() {
      return $state.reload();
    });
  };

  $scope.viewNotification = function(notificationId) {
    var modalInstance = scModal.open('viewNotificationModal', {
      notificationSettingId: notificationId
    });

    modalInstance.result.then(function() {
      return $state.reload();
    }, angular.noop);

    return modalInstance.opened;
  };

  $scope.filters = {
    notificationEventId: null,
    isRead: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.userNotifications.search', {
      notificationEventId: $scope.filters.notificationEventId,
      isRead: $scope.filters.isRead
    });
  };
}

UserNotificationsSearchCtrl.$inject = [
  '$scope',
  '$state',
  'userNotifications',
  'UserNotification',
  'scConfirm',
  'scModal',
  '$stateParams'
];

UserNotificationsSearchCtrl.$resolve = {
  userNotifications: [
    '$stateParams',
    'UserNotification',
    function($stateParams, UserNotification) {
      return UserNotification.query($stateParams).$promise;
    }
  ]
};

export { UserNotificationsSearchCtrl };
