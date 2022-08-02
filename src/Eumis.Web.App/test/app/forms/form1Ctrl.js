function Form1Ctrl($q, $scope, $timeout, scMessage) {
  $scope.isValidSync = function(text) {
    var firstChar;

    if (!text) {
      return true;
    }

    firstChar = text[0];
    return firstChar === firstChar.toUpperCase();
  };

  $scope.isValidAsync = function(text) {
    if (!text) {
      return $q.resolve();
    }
    return $timeout(function() {
      return /\d/.test(text) ? $q.resolve() : $q.reject();
    }, 1000);
  };

  $scope.customBtnAction = function() {
    return scMessage('Test message');
  };
}

Form1Ctrl.$inject = ['$q', '$scope', '$timeout', 'scMessage'];

export { Form1Ctrl };
