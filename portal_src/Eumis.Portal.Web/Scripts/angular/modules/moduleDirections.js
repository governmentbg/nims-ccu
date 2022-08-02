angular.module('moduleDirections', ['scaffolding'])
    .factory('DirectrionSection', ['$window', function ($window) {
        return $window['_eumis_options'].directionSection;
    }])
    .controller('controllerDirections',
        ['$scope', '$filter', '$timeout', '$window', 'rfc4122', 'DirectrionSection',
            function ($scope, $filter, $timeout, $window, rfc4122, DirectrionSection) {

                $scope.toNomenclature = function (i) {
                    if (i) {
                        return {
                            id: i.id,
                            text: i.Name,
                            Name: i.Name,
                            NameEN: i.NameEN
                        }
                    }
                    return undefined;
                }

                if (DirectrionSection.DirectionCollection == undefined) {
                    DirectrionSection.DirectionCollection = [];
                }
                $scope.items = DirectrionSection.DirectionCollection;

                $scope.directionNomenclature = (DirectrionSection.Items || []).map(function (item) {
                    return {
                        direction: $scope.toNomenclature(item.DirectionItem),
                        subDirection: $scope.toNomenclature(item.SubDirection)
                    };
                });


                $scope.isValid = true;


                $scope.addItem = function () {
                    var item = {
                        editTriggerId: rfc4122.newuuid(),
                        IsNameValid: true,

                        gid: rfc4122.newuuid(),
                        isActive: true,
                        isActivated: false,
                        IsDirectionValid: true
                    }

                    $scope.items.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (item) {
                    $scope.items.splice($scope.items.indexOf(item), 1);
                }

                $scope.uniqueDirection = function(item) {
                    return item.id === $scope.currentDirection.id;
                }

                $scope.loadDirectionNomenclature = function (query) {
                    var data = { results: [] };
                    $.each($scope.directionNomenclature, function () {
                        $scope.currentDirection = this.direction;
                        if (data.results.filter($scope.uniqueDirection).length === 0) {
                            if (query.term.length == 0 || this.direction.text.toUpperCase().indexOf(query.term.toUpperCase()) >= 0) {
                                data.results.push(this.direction);
                            }
                        }
                    });
                    query.callback(data);
                };

                $scope.loadSubDirectionNomenclature = function (item) {
                    return {
                        allowClear: true,
                        placeholder: ' ',
                        query: function (query) {
                            var data = { results: [] };

                            if (item.DirectionItem) {
                                filtered = $scope.directionNomenclature.filter(function (x) {
                                    return x.direction.id === item.DirectionItem.id
                                });

                                for (let m = 0; m < filtered.length; m++) {
                                    if (filtered[m].subDirection) {
                                        data.results.push(filtered[m].subDirection);
                                    }
                                }
                            }
                            query.callback(data);
                        }
                    };
                }

                $scope.clearSubDirection = function (item) {
                    item.SubDirection = undefined;
                }

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }
        ]
    )