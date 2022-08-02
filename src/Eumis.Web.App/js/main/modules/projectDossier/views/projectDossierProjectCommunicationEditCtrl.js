import _ from 'lodash';

function ProjectDossierProjectCommunicationEditCtrl(
  $scope,
  $state,
  $stateParams,
  projectCommunication
) {
  $scope.projectCommunication = projectCommunication;
  $scope.canViewAnswer = _.includes(['answer', 'applied', 'rejected'], projectCommunication.status);

  $scope.back = function() {
    return $state.go('root.projectDossier.view.project', {
      id: $stateParams.id
    });
  };
}

ProjectDossierProjectCommunicationEditCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'projectCommunication'
];

ProjectDossierProjectCommunicationEditCtrl.$resolve = {
  projectCommunication: [
    'ProjectCommunication',
    'ProjectCommunicationFile',
    '$stateParams',
    function(ProjectCommunication, ProjectCommunicationFile, $stateParams) {
      return ProjectCommunication.get({
        id: $stateParams.id,
        mid: $stateParams.mid
      }).$promise.then(function(communication) {
        if (communication.projectCommunicationFile) {
          communication.projectCommunicationFile.url = ProjectCommunicationFile.getUrl({
            id: $stateParams.id,
            ind: $stateParams.mid,
            projectCommunicationFileId: communication.projectCommunicationFile.id
          });

          _(communication.projectCommunicationFileSignatures).forEach(function(pcfs) {
            pcfs.url = ProjectCommunicationFile.getUrl({
              id: $stateParams.in,
              ind: $stateParams.mid,
              projectCommunicationFileId: communication.projectCommunicationFile.id,
              projectCommunicationFileSignatureId: pcfs.id
            });
          });
        }

        return communication;
      });
    }
  ]
};

export { ProjectDossierProjectCommunicationEditCtrl };
