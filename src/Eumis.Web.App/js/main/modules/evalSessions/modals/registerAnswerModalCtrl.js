function RegisterAnswerModalCtrl(
  moment,
  $scope,
  $uibModalInstance,
  scModalParams,
  scDateConfig,
  ProjectCommunicationAnswer
) {
  var now = moment(),
    date = moment(now)
      .hours(0)
      .seconds(0)
      .minutes(0)
      .milliseconds(0);

  $scope.errors = [];
  $scope.model = {
    regNumber: scModalParams.regNumber,
    regDate: date.format(scDateConfig.dateModelFormat)
  };

  $scope.register = function() {
    $scope.errors = [];

    return $scope.registerAnswerForm.$validate().then(function() {
      if ($scope.registerAnswerForm.$valid) {
        return ProjectCommunicationAnswer.canRegisterAnswer(
          {
            ind: scModalParams.projectId,
            mid: scModalParams.communicationId,
            aid: scModalParams.answerId,
            version: scModalParams.version
          },
          {
            answerHash: $scope.model.hash,
            regDate: $scope.model.regDate
          }
        ).$promise.then(function(result) {
          if (result.errors.length) {
            $scope.errors = result.errors;
            return;
          }

          return ProjectCommunicationAnswer.registerAnswer(
            {
              ind: scModalParams.projectId,
              mid: scModalParams.communicationId,
              aid: scModalParams.answerId,
              version: scModalParams.version
            },
            {
              answerHash: $scope.model.hash,
              regDate: $scope.model.regDate
            }
          ).$promise.then(function() {
            return $uibModalInstance.close();
          });
        });
      }
    });
  };

  $scope.cancel = function() {
    return $uibModalInstance.dismiss();
  };
}

RegisterAnswerModalCtrl.$inject = [
  'moment',
  '$scope',
  '$uibModalInstance',
  'scModalParams',
  'scDateConfig',
  'ProjectCommunicationAnswer'
];

export { RegisterAnswerModalCtrl };
