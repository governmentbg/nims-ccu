import _ from 'lodash';

function ProjectManagingAuthorityCommunicationsEditCtrl(
  $scope,
  $state,
  structuredDocument,
  projectManagingAuthorityCommunication,
  projectCommunicationAnswers
) {
  $scope.projectManagingAuthorityCommunication = projectManagingAuthorityCommunication;
  $scope.projectCommunicationId = projectManagingAuthorityCommunication.projectCommunicationId;

  $scope.projectCommunicationAnswers = _.map(projectCommunicationAnswers, function(item) {
    item.viewXmlUrl = structuredDocument.getParentChildUrl(
      'projectManagingAuthorityCommunicationAnswer',
      'view',
      $scope.projectManagingAuthorityCommunication.xmlGid,
      item.xmlGid
    );
    return item;
  });

  $scope.canViewAnswer = _.includes(
    ['answer', 'applied', 'rejected'],
    projectManagingAuthorityCommunication.status
  );

  $scope.back = function() {
    return $state.go('root.projectCommunications.search');
  };
}

ProjectManagingAuthorityCommunicationsEditCtrl.$inject = [
  '$scope',
  '$state',
  'structuredDocument',
  'projectManagingAuthorityCommunication',
  'projectCommunicationAnswers'
];

ProjectManagingAuthorityCommunicationsEditCtrl.$resolve = {
  projectManagingAuthorityCommunication: [
    'ProjectManagingAuthorityCommunication',
    '$stateParams',
    function(ProjectManagingAuthorityCommunication, $stateParams) {
      return ProjectManagingAuthorityCommunication.get({ id: $stateParams.id }).$promise;
    }
  ],
  projectCommunicationAnswers: [
    'ProjectManagingAuthorityCommunicationAnswer',
    '$stateParams',
    function(ProjectManagingAuthorityCommunicationAnswer, $stateParams) {
      return ProjectManagingAuthorityCommunicationAnswer.query({
        id: $stateParams.id
      }).$promise;
    }
  ]
};

export { ProjectManagingAuthorityCommunicationsEditCtrl };
