import _ from 'lodash';

function ProgrammeDocumentsSearchCtrl(
  $scope,
  $stateParams,
  $interpolate,
  l10n,
  structuredDocument,
  ProgrammeFile,
  documents,
  applicationDocuments,
  declarations
) {
  $scope.programmeId = $stateParams.id;
  $scope.programmeStatus = $scope.info.status;
  $scope.declarations = declarations;

  $scope.applicationDocuments = applicationDocuments;
  $scope.downloadTemplate = l10n.get('programmes_applicationDocumentsSearch_template');

  $scope.documents = _.map(documents, function(item) {
    if (item.file) {
      item.file.url = ProgrammeFile.getUrl({ id: item.mapNodeId, fileKey: item.file.key });
    }
    return item;
  });

  $scope.applicationDocumentsExportUrl = $interpolate(
    'api/programmes/{{id}}/applicationDocuments/excelExport'
  )({
    id: $stateParams.id
  });
}

ProgrammeDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  '$interpolate',
  'l10n',
  'structuredDocument',
  'ProgrammeFile',
  'documents',
  'applicationDocuments',
  'declarations'
];

ProgrammeDocumentsSearchCtrl.$resolve = {
  documents: [
    '$stateParams',
    'ProgrammeDocument',
    function($stateParams, ProgrammeDocument) {
      return ProgrammeDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  applicationDocuments: [
    '$stateParams',
    'ProgrammeApplicationDocument',
    function($stateParams, ProgrammeApplicationDocument) {
      return ProgrammeApplicationDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  declarations: [
    '$stateParams',
    'ProgrammeDeclaration',
    function($stateParams, ProgrammeDeclaration) {
      return ProgrammeDeclaration.query({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProgrammeDocumentsSearchCtrl };
