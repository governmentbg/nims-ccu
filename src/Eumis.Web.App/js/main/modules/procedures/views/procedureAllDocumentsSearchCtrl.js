import _ from 'lodash';

function ProcedureAllDocumentsSearchCtrl(
  $scope,
  $stateParams,
  structuredDocument,
  ProcedureFile,
  documents,
  appGuidelines,
  questions,
  appDocs,
  evalTables,
  procedureDeclarations,
  procedureInfo,
  scModal,
  scConfirm,
  Procedure
) {
  $scope.isProcedureReadonly = $scope.info.status !== 'draft';
  $scope.procedureId = $stateParams.id;
  $scope.procedureInfo = procedureInfo;
  $scope.isProcedureAnnounced =
    $scope.info.activationDate &&
    (procedureInfo.status === 'draft' || procedureInfo.status === 'active');
  $scope.procedureDeclarations = procedureDeclarations;

  $scope.documents = _.map(documents, function(item) {
    if (item.file) {
      item.file.url = ProcedureFile.getUrl({
        id: item.procedureId,
        fileKey: item.file.key
      });
    }
    return item;
  });

  $scope.appGuidelines = _.map(appGuidelines, function(item) {
    if (item.file) {
      item.file.url = ProcedureFile.getUrl({
        id: item.procedureId,
        fileKey: item.file.key
      });
    }
    return item;
  });

  $scope.questions = _.map(questions, function(item) {
    if (item.file) {
      item.file.url = ProcedureFile.getUrl({
        id: item.procedureId,
        fileKey: item.file.key
      });
    }
    return item;
  });

  if (!$scope.isProcedureReadonly && $scope.questions.length > 0) {
    _.last($scope.questions).isLast = true;
  }

  $scope.appDocs = appDocs;

  $scope.evalTables = _.map(evalTables, function(item) {
    item.viewXmlUrl = structuredDocument.getUrl('procedureEvalTable', 'view', item.xmlGid);

    return item;
  });

  $scope.showApplicationForm = function() {
    return scConfirm({
      resource: 'Procedure',
      validationAction: 'validate',
      params: { id: $stateParams.id }
    }).then(function(result) {
      if (result.executed) {
        Procedure.getGid({ id: $stateParams.id }).$promise.then(function(gid) {
          var url = structuredDocument.getUrl('sampleProject', 'view', gid);

          window.open(url, '_blank');
        });
      }
    });
  };
}

ProcedureAllDocumentsSearchCtrl.$inject = [
  '$scope',
  '$stateParams',
  'structuredDocument',
  'ProcedureFile',
  'documents',
  'appGuidelines',
  'questions',
  'appDocs',
  'evalTables',
  'procedureDeclarations',
  'procedureInfo',
  'scModal',
  'scConfirm',
  'Procedure'
];

ProcedureAllDocumentsSearchCtrl.$resolve = {
  documents: [
    '$stateParams',
    'ProcedureDocument',
    function($stateParams, ProcedureDocument) {
      return ProcedureDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  appGuidelines: [
    '$stateParams',
    'ProcedureAppGuideline',
    function($stateParams, ProcedureAppGuideline) {
      return ProcedureAppGuideline.query({ id: $stateParams.id }).$promise;
    }
  ],
  questions: [
    '$stateParams',
    'ProcedureQuestion',
    function($stateParams, ProcedureQuestion) {
      return ProcedureQuestion.query({ id: $stateParams.id }).$promise;
    }
  ],
  appDocs: [
    '$stateParams',
    'ProcedureAppDocument',
    function($stateParams, ProcedureAppDocument) {
      return ProcedureAppDocument.query({ id: $stateParams.id }).$promise;
    }
  ],
  evalTables: [
    '$stateParams',
    'ProcedureEvalTable',
    function($stateParams, ProcedureEvalTable) {
      return ProcedureEvalTable.query({ id: $stateParams.id }).$promise;
    }
  ],
  procedureDeclarations: [
    '$stateParams',
    'ProcedureDeclaration',
    function($stateParams, ProcedureDeclaration) {
      return ProcedureDeclaration.query({ id: $stateParams.id }).$promise;
    }
  ],
  procedureInfo: [
    '$stateParams',
    'Procedure',
    function($stateParams, Procedure) {
      return Procedure.getInfo({ id: $stateParams.id }).$promise;
    }
  ]
};

export { ProcedureAllDocumentsSearchCtrl };
