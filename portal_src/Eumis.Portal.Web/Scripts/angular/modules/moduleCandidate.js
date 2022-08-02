angular.module('moduleCandidate', ['scaffolding'])
    .controller('controllerCandidate',
        ['$scope', '$filter', '$window', '$timeout',
            function ($scope, $filter, $window, $timeout) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;
                    $scope.candidate = $window[globalKey][parentKey].candidate;
                    $scope.resourcesObject = $window[globalKey][parentKey].resourcesObject;
                    $scope.currentCulture = $window['_eumis_options']['currentCulture'];

                    $scope.upgrade($scope.candidate);
                };

                $scope.$watch('candidate', function () {

                    if ($scope.candidate.Name && $scope.candidate.Uin) {
                        $window[$scope.globalKey]['_selected_candidate'] =
                            {
                                id: $scope.candidate.Uin,
                                text: $scope.getDisplayName($scope.candidate.Name, $scope.candidate.NameEN),
                                Name: $scope.candidate.Name, NameEN:
                                $scope.candidate.NameEN
                            };
                    }
                    else {
                        $window[$scope.globalKey]['_selected_candidate'] = null;
                    }

                    refreshProgrammeContractActivities();

                }, true);

                $scope.getDisplayName = function (name, nameEN) {
                    if ($scope.currentCulture === $window[$scope.globalKey]['constants'].CultureEN) {
                        return nameEN === undefined ? name : nameEN;
                    }

                    return name;
                }

                $scope.upgrade = function (item) {
                    if (!item.Seat) {
                        item.Seat = {};
                        item.Seat.IsCountryValid = true;
                        item.Seat.IsSettlementValid = true;
                        item.Seat.IsPostCodeValid = true;
                        item.Seat.IsStreetValid = true;
                        item.Seat.IsFullAddressValid = true;
                    }

                    if (!item.Correspondence) {
                        item.Correspondence = {};
                        item.Correspondence.IsCountryValid = true;
                        item.Correspondence.IsSettlementValid = true;
                        item.Correspondence.IsPostCodeValid = true;
                        item.Correspondence.IsStreetValid = true;
                        item.Correspondence.IsFullAddressValid = true;
                    }
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);