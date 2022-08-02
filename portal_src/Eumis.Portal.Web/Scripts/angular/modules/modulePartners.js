angular.module('modulePartners', ['scaffolding', 'utils'])
    .controller('controllerPartners',
        ['$scope', '$filter', '$window', '$timeout', 'rfc4122', 'appcontext',
            function ($scope, $filter, $window, $timeout, rfc4122, appcontext) {

                $scope.init = function (globalKey, parentKey) {
                    $scope.globalKey = globalKey;

                    var _partners = $window[globalKey][parentKey].partners;

                    $scope.currentCulture = $window['_eumis_options']['currentCulture'];
                    $scope.partners = _partners;
                    $scope.maxPartners = $window[globalKey]['constants'].PartnersMaxCount;
                    $scope.resourcesObject = $window[globalKey][parentKey].resourcesObject;
                    $scope.hasValidCount = $window[globalKey][parentKey].hasValidCount;
                    $scope.initActivations();
                };

                $scope.initActivations = function () {
                    $scope.$on('partnersActivation', function (event, args) {
                        if (!!args.update) {
                            appcontext.save(args.d, 'UpdatePartners', $scope.partners, {});
                        } else {
                            $scope.$apply(function () {
                                $scope.partners.IsActive = !!args.isActive;
                            });
                        }
                    });
                };

                $scope.addItem = function () {
                    var item = { editTriggerId: rfc4122.newuuid() };
                    item = $scope.upgrade(item);

                    $scope.partners.push(item);

                    $timeout(function () {
                        $("#" + item.editTriggerId).click();
                    }, 50);
                }

                $scope.delItem = function (partner) {
                    $scope.partners.splice($scope.partners.indexOf(partner), 1);
                }

                $scope.upgrade = function (item) {
                    item.IsUinTypeValid = true;
                    item.IsUinValid = true;
                    item.IsNameValid = true;
                    item.IsNameEnValid = true;
                    item.IsCompanyTypeValid = true;
                    item.IsCompanyLegalTypeValid = true;
                    item.IsCompanySizeTypeValid = true;
                    item.IsPrivateLegal = false;

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

                    item.IsEmailValid = true;
                    item.IsPhone1Valid = true;
                    item.IsPhone2Valid = true;
                    item.IsFaxValid = true;
                    item.IsCompanyRepresentativePersonValid = true;
                    item.IsCompanyContactPersonValid = true;
                    item.IsCompanyContactPersonPhoneValid = true;
                    item.IsCompanyContactPersonEmailValid = true;

                    if (!item.Correspondence.Country) {
                        item.Correspondence.Country = {};
                        item.Correspondence.Country.id = $window[$scope.globalKey]['constants'].BulgariaId;
                        item.Correspondence.Country.Code = $window[$scope.globalKey]['constants'].BulgariaId;
                        item.Correspondence.Country.Name = $window[$scope.globalKey]['constants'].BulgariaName;
                        item.Correspondence.Country.NameEN = $window[$scope.globalKey]['constants'].BulgariaNameEN;
                        item.Correspondence.Country.text = $scope.getDisplayName(
                            $window[$scope.globalKey]['constants'].BulgariaName,
                            $window[$scope.globalKey]['constants'].BulgariaNameEN);
                    }

                    if (!item.Seat.Country) {
                        item.Seat.Country = {};
                        item.Seat.Country.id = $window[$scope.globalKey]['constants'].BulgariaId;
                        item.Seat.Country.Code = $window[$scope.globalKey]['constants'].BulgariaId;
                        item.Seat.Country.Name = $window[$scope.globalKey]['constants'].BulgariaName;
                        item.Seat.Country.NameEN = $window[$scope.globalKey]['constants'].BulgariaNameEN;
                        item.Seat.Country.text = $scope.getDisplayName(
                            $window[$scope.globalKey]['constants'].BulgariaName,
                            $window[$scope.globalKey]['constants'].BulgariaNameEN);
                    }

                    return item;
                }

                $scope.getDisplayName = function (name, nameEN) {
                    if ($scope.currentCulture === $window[$scope.globalKey]['constants'].CultureEN)
                    {
                        return nameEN === undefined ? name : nameEN;
                    }

                    return name;
                }

                $scope.$watch('partners', function () {

                    var partners = [];

                    $scope.partners.forEach(function(partner) {
                        if (partner.Name && partner.Uin) {
                            partners.push(
                                {
                                    id: partner.Uin,
                                    text: $scope.getDisplayName(partner.Name, partner.NameEN),
                                    Name: partner.Name,
                                    NameEN: partner.NameEN
                                });
                        }
                    });

                    $window[$scope.globalKey]['_selected_partners'] = partners;

                    refreshProgrammeContractActivities();

                }, true);

                $scope.accordion = function (index, $event) {
                    $scope.partners[index].isOpen = !$scope.partners[index].isOpen;
                    $scope.partners.forEach(function (partner, i) {
                        if (i != index) {
                            partner.isOpen = false;
                        }
                    });
                };

                $scope.$evalAsync(function () {
                    $window['__eumis__queue__'] = ($window['__eumis__queue__'] || 0) + 1;
                });
            }]);