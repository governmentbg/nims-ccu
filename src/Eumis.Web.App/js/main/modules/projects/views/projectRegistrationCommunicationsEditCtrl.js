import _ from 'lodash';

function ProjectRegistrationCommunicationsEditCtrl(
  $scope,
  $state,
  $stateParams,
  scConfirm,
  structuredDocument,
  ProjectRegistrationCommunication,
  projectRegistrationCommunication,
  projectCommunicationAnswers
) {
  $scope.editMode = null;
  $scope.projectRegistrationCommunication = projectRegistrationCommunication;
  $scope.projectCommunicationId = projectRegistrationCommunication.projectCommunicationId;

  $scope.projectCommunicationAnswers = _.map(projectCommunicationAnswers, function(item) {
    item.viewXmlUrl = structuredDocument.getParentChildUrl(
      'projectManagingAuthorityCommunicationAnswer',
      'view',
      $scope.projectRegistrationCommunication.xmlGid,
      item.xmlGid
    );
    return item;
  });

  $scope.hasActiveAnswer =
    $scope.projectCommunicationAnswers.filter(a => a.status === 'answer').length > 0;

  $scope.canEditCommunication =
    _.includes(['draftQuestion', 'question'], projectRegistrationCommunication.status) &&
    projectRegistrationCommunication.source === 'managingAuthority';

  $scope.canDeleteCommunication = projectRegistrationCommunication.status === 'draftQuestion';
  $scope.canCancelCommunication =
    projectRegistrationCommunication.status === 'question' &&
    projectRegistrationCommunication.source === 'managingAuthority' &&
    !$scope.hasActiveAnswer;

  $scope.newAnswer = function() {
    let confirmMessage = null;
    if ($scope.hasActiveAnswer) {
      confirmMessage = 'projects_projectRegistrationCommunicationsEdit_existingAnswerMessage';
    }

    return scConfirm({
      resource: 'ProjectRegistrationCommunicationAnswer',
      confirmMessage: confirmMessage,
      validationAction: 'canCreate',
      action: 'save',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.projectRegistrationCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projects.view.communications.edit.answers.edit', {
          id: $stateParams.id,
          ind: $stateParams.ind,
          aid: result.result.projectCommunicationAnswerId
        });
      }
    });
  };

  $scope.edit = function() {
    $scope.editMode = 'edit';
  };

  $scope.save = function() {
    return $scope.editProjectRegistrationCommunicationForm.$validate().then(function() {
      if ($scope.editProjectRegistrationCommunicationForm.$valid) {
        return ProjectRegistrationCommunication.update(
          {
            id: $stateParams.id,
            ind: $stateParams.ind
          },
          $scope.projectRegistrationCommunication
        ).$promise.then(function() {
          $state.partialReload();
        });
      }
    });
  };

  $scope.cancel = function() {
    return $state.partialReload();
  };

  $scope.del = function() {
    return scConfirm({
      confirmMessage: 'common_messages_confirmDelete',
      resource: 'ProjectRegistrationCommunication',
      action: 'remove',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.projectRegistrationCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.go('root.projects.view.communications.search', $stateParams, {
          reload: true
        });
      }
    });
  };

  $scope.docUpdated = function() {
    return $state.partialReload();
  };

  $scope.cancelCommunication = function() {
    return scConfirm({
      confirmMessage: 'projects_projectRegistrationCommunicationsEdit_cancelConfirm',
      noteLabel: 'projects_projectRegistrationCommunicationsEdit_cancelNote',
      resource: 'ProjectRegistrationCommunication',
      action: 'cancelCommunication',
      params: {
        id: $stateParams.id,
        ind: $stateParams.ind,
        version: $scope.projectRegistrationCommunication.version
      }
    }).then(function(result) {
      if (result.executed) {
        return $state.partialReload();
      }
    });
  };
}

ProjectRegistrationCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'scConfirm',
  'structuredDocument',
  'ProjectRegistrationCommunication',
  'projectRegistrationCommunication',
  'projectCommunicationAnswers'
];

ProjectRegistrationCommunicationsEditCtrl.$resolve = {
  projectRegistrationCommunication: [
    'ProjectRegistrationCommunication',
    '$stateParams',
    function(ProjectRegistrationCommunication, $stateParams) {
      return ProjectRegistrationCommunication.get({ id: $stateParams.id, ind: $stateParams.ind })
        .$promise;
    }
  ],
  projectCommunicationAnswers: [
    'ProjectRegistrationCommunicationAnswer',
    '$stateParams',
    function(ProjectRegistrationCommunicationAnswer, $stateParams) {
      return ProjectRegistrationCommunicationAnswer.query({
        id: $stateParams.id,
        ind: $stateParams.ind
      }).$promise;
    }
  ]
};

export { ProjectRegistrationCommunicationsEditCtrl };
