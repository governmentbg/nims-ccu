import _ from 'lodash';

function GuidancesSearchCtrl($scope, guidances) {
  $scope.guidances = guidances;
}

GuidancesSearchCtrl.$inject = ['$scope', 'guidances'];

GuidancesSearchCtrl.$resolve = {
  guidances: [
    'Guidance',
    'GuidanceFile',
    function(Guidance, GuidanceFile) {
      return Guidance.query().$promise.then(function(guidances) {
        _.map(guidances, function(item) {
          if (item.file) {
            item.file.url = GuidanceFile.getUrl({
              id: item.guidanceId,
              fileKey: item.file.key
            });
          }
          return item;
        });

        return guidances;
      });
    }
  ]
};

export { GuidancesSearchCtrl };
