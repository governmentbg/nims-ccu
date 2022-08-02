function FilledCtrl($scope) {
  $scope.someData = {
    text: 'some text',
    textArea: 'some\nmulti-line\ntext',
    date: new Date(2000, 1, 29),
    brandId: 'vw',
    modelId: 'golf',
    file: {
      name: 'existing.txt',
      key: 'existingfilekey'
    },
    syncValidationText: 'Asd',
    asyncValidationText: 'testtest1',
    fieldGroupField: 100
  };
}

FilledCtrl.$inject = ['$scope'];

export { FilledCtrl };
