import _ from 'lodash';

function ProjectDossierDocumentsSearchCtrl(
  $scope,
  $state,
  $stateParams,
  ProjectDossierFile,
  projectDossierDocuments
) {
  $scope.projectDossierDocuments = _.map(projectDossierDocuments, function(item) {
    item.file.url = ProjectDossierFile.getUrl({
      id: $stateParams.id,
      fileKey: item.file.key
    });

    return item;
  });

  $scope.filters = {
    docTypes: null,
    objDescription: null,
    fileDescription: null
  };

  _.forOwn($stateParams, function(value, param) {
    if (value !== null && value !== undefined && param !== 'ts') {
      $scope.filters[param] = value;
    }
  });

  $scope.search = function() {
    return $state.go('root.projectDossier.view.documents.search', {
      id: $stateParams.id,
      docTypes: $scope.filters.docTypes,
      objDescription: $scope.filters.objDescription,
      fileDescription: $scope.filters.fileDescription
    });
  };
}

ProjectDossierDocumentsSearchCtrl.$inject = [
  '$scope',
  '$state',
  '$stateParams',
  'ProjectDossierFile',
  'projectDossierDocuments'
];

ProjectDossierDocumentsSearchCtrl.$resolve = {
  projectDossierDocuments: [
    'ProjectDossierDcoument',
    '$stateParams',
    function(ProjectDossierDcoument, $stateParams) {
      if ($stateParams.docTypes) {
        $stateParams.docTypes = $stateParams.docTypes.split(',');
      } else {
        $stateParams.docTypes = null;
      }
      return ProjectDossierDcoument.query($stateParams).$promise;
    }
  ]
};

export { ProjectDossierDocumentsSearchCtrl };
