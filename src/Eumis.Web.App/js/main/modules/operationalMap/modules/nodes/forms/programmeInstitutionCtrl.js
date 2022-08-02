function ProgrammeInstitutionCtrl($scope, eumisConstants) {
  $scope.emailRegex = eumisConstants.emailRegex;
}

ProgrammeInstitutionCtrl.$inject = ['$scope', 'eumisConstants'];

export { ProgrammeInstitutionCtrl };
