angular.module('moduleContractTeamCollection', ['scaffolding', 'utils'])
    .controller('controllerContractTeamCollection',
        ['$scope', '$window', '$timeout', 'rfc4122', 'appcontext',
            function ($scope, $window, $timeout, rfc4122, appcontext) {
                $scope.init = function (globalKey, parentKey) {
                    $scope.items = $window[globalKey][parentKey].items;
                    $scope.areItemsValid = $window[globalKey][parentKey].areItemsValid;
                    $scope.maxContractTeams = $window[globalKey]['constants'].ContractTeamsMaxCount;
                    $scope.initActivations();
                };

                $scope.initActivations = function () {
                    $scope.$on('teamsActivation', function (event, args) {
                        if (!!args.update) {
                            appcontext.save(args.d, 'UpdateTeams', $scope.items, {});
                        } else {
                            $scope.$apply(function () {
                                $scope.items.IsActive = !!args.isActive;
                            });
                        }
                    });
                };

                $scope.addItem = function () {
                    var item = {
                        editTriggerId: rfc4122.newuuid(),
                        IsNameValid: true,
                        IsPositionValid: true,
                        IsResponsibilitiesValid: true,
                        IsPhoneValid: true,
                        IsEmailValid: true,
                        IsFaxValid: true
                    }

                    $scope.items.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (item) {
                    $scope.items.splice($scope.items.indexOf(item), 1);
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);
