import _ from 'lodash';

function UsersSearchCtrl($scope, $state, $stateParams, $interpolate, l10n, User, users) {
  $scope.users = users;
  $scope.activeText = l10n.get('users_search_active');
  $scope.inactiveText = l10n.get('users_search_inactive');
  $scope.lockedText = l10n.get('users_search_locked');
  $scope.deletedText = l10n.get('users_search_deleted');

  $scope.usersExportUrl = $interpolate(
    'api/users/excelExport?' +
      'username={{username}}&' +
      'fullname={{fullname}}&' +
      'userOrganizationId={{userOrganizationId}}&' +
      'active={{active}}&' +
      'deleted={{deleted}}&' +
      'locked={{locked}}&' +
      'hasAcceptedGDPRDeclaration={{hasAcceptedGDPRDeclaration}}'
  )({
    username: $stateParams.username,
    fullname: $stateParams.fullname,
    userOrganizationId: $stateParams.userOrganizationId,
    active: $stateParams.active,
    deleted: $stateParams.deleted,
    hasAcceptedGDPRDeclaration: $stateParams.hasAcceptedGDPRDeclaration
  });

  $scope.filters = {
    username: null,
    fullname: null,
    userOrganizationId: null,
    active: null,
    deleted: null,
    locked: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined) {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.users.search', $scope.filters);
  };
}

UsersSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  '$interpolate',
  'l10n',
  'User',
  'users'
];

UsersSearchCtrl.$resolve = {
  users: [
    '$stateParams',
    'User',
    function($stateParams, User) {
      return User.query($stateParams).$promise;
    }
  ]
};

export { UsersSearchCtrl };
