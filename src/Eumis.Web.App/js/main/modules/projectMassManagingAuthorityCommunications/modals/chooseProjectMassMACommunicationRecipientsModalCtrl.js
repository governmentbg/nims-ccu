import * as _ from 'lodash';

function ChooseProjectMassMACommunicationRecipientsModalCtrl(
  $scope,
  $uibModalInstance,
  scModalParams,
  ProjectMassManagingAuthorityCommunicationRecipient,
  recipients
) {
  $scope.chosenProjectIds = [];
  $scope.recipients = recipients;
  $scope.hasChosenAll = true;
  $scope.tableControl = {};

  $scope.loadRecipientsErrors = [];
  $scope.uploadMode = null;
  $scope.file = null;

  $scope.chooseAll = function() {
    const filteredRecipients = $scope.tableControl.getFilteredItems();
    _.forEach(filteredRecipients, function(cr) {
      if (_.includes($scope.chosenProjectIds, cr.projectId)) {
        cr.isChosen = true;
      } else {
        $scope.choose(cr);
      }
    });
    $scope.hasChosenAll = false;
  };

  $scope.removeAll = function() {
    _.forEach($scope.recipients, function(cr) {
      $scope.remove(cr);
    });
    $scope.hasChosenAll = true;
  };

  $scope.choose = function(recipient) {
    recipient.isChosen = true;
    $scope.chosenProjectIds.push(recipient.projectId);
  };

  $scope.remove = function(recipient) {
    recipient.isChosen = false;
    $scope.chosenProjectIds = _.without($scope.chosenProjectIds, recipient.projectId);
  };

  $scope.ok = function() {
    return ProjectMassManagingAuthorityCommunicationRecipient.save(
      {
        id: scModalParams.communicationId,
        version: scModalParams.version
      },
      $scope.chosenProjectIds
    ).$promise.then(function() {
      return $uibModalInstance.close();
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss('cancel');
  };

  $scope.uploadFile = function() {
    $scope.uploadMode = 'upload';
  };

  $scope.cancelFileUpload = function() {
    $scope.uploadMode = null;
    $scope.file = null;
    $scope.loadRecipientsErrors = [];
  };

  $scope.loadRecipients = function() {
    return ProjectMassManagingAuthorityCommunicationRecipient.loadRecipientsFromFile({
      id: scModalParams.communicationId,
      fileKey: $scope.file.key
    }).$promise.then(function(result) {
      $scope.loadRecipientsErrors = result.errors;

      if ($scope.loadRecipientsErrors.length === 0) {
        if (result.projectIds.length > 0) {
          _.forEach($scope.recipients, function(recipient) {
            $scope.remove(recipient);

            if (_.includes(result.projectIds, recipient.projectId)) {
              $scope.choose(recipient);
            }
          });
        }

        $scope.uploadMode = null;
        $scope.file = null;
      }
    });
  };
}

ChooseProjectMassMACommunicationRecipientsModalCtrl.$inject = [
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'ProjectMassManagingAuthorityCommunicationRecipient',
  'recipients'
];

ChooseProjectMassMACommunicationRecipientsModalCtrl.$resolve = {
  recipients: [
    'ProjectMassManagingAuthorityCommunicationRecipient',
    'scModalParams',
    function(ProjectMassManagingAuthorityCommunicationRecipient, scModalParams) {
      return ProjectMassManagingAuthorityCommunicationRecipient.getUnattachedRecipients({
        id: scModalParams.communicationId
      }).$promise;
    }
  ]
};

export { ChooseProjectMassMACommunicationRecipientsModalCtrl };
