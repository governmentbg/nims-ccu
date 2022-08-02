import _ from 'lodash';

function ChooseUsersModalCtrl(
  $scope,
  $uibModalInstance,
  l10n,
  $injector,
  scModalParams,
  User,
  users
) {
  var previouslyChosenUserIds = scModalParams.previouslyChosenUserIds;

  $scope.activeText = l10n.get('users_search_active');
  $scope.inactiveText = l10n.get('users_search_inactive');
  $scope.lockedText = l10n.get('users_search_locked');
  $scope.deletedText = l10n.get('users_search_deleted');

  $scope.form = {};
  $scope.chosenUsersIds = [];
  $scope.users = _.filter(users, function(user) {
    return !_.includes(previouslyChosenUserIds, user.userId);
  });

  $scope.chooseUser = function(user) {
    user.isChosen = true;
    $scope.chosenUsersIds.push(user.userId);
  };

  $scope.removeUser = function(user) {
    user.isChosen = false;
    $scope.chosenUsersIds = _.without($scope.chosenUsersIds, user.userId);
  };

  $scope.ok = function() {
    return $injector
      .get(scModalParams.resourceName)
      .save(
        {
          id: scModalParams.rootId,
          version: scModalParams.version
        },
        $scope.chosenUsersIds
      )
      .$promise.then(function() {
        return $uibModalInstance.close();
      });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };
}

ChooseUsersModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'l10n',
  '$injector',
  'scModalParams',
  'User',
  'users'
];

ChooseUsersModalCtrl.$resolve = {
  users: [
    'User',
    'scModalParams',
    function(User, scModalParams) {
      return User.query({
        userOrganizationId: scModalParams.userOrganizationId
      }).$promise;
    }
  ]
};

export { ChooseUsersModalCtrl };
