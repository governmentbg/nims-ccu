(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('nutsAddressAsync', ['$window', function ($window) {
        return {
            restrict: 'E',
            scope: {
                model: '=ngModel',
                nuts: '=',
                globalKey: '=',
                path: '@',
                resources: '=',
                isPartialReadOnly: '@'
            },
            templateUrl: '/Scripts/angular/directives/nutsAddressAsync/nutsAddressAsync.html',
            link: function (scope, element, attrs) {
                scope.countryCode = 'country';
                scope.protectedZoneCode = 'protectedZone';
                scope.nuts1Code = 'regionNUTS1';
                scope.nuts2Code = 'regionNUTS2';
                scope.districtCode = 'district';
                scope.municipalityCode = 'municipality';
                scope.settlementCode = 'settlement';

                scope.items = scope.model.NutsAddressContentCollection;
                scope.selectedNut = scope.model.NutsLevel;

                scope.$watch('items', function () {
                    if (scope.items.length == 0) {
                        scope.addItem();
                    }
                    scope.updateNutsAddresses(false);
                }, true);

                scope.$watch('selectedNut', function (oldVal, newVal) {
                    if (oldVal.id != newVal.id) {
                        scope.updateNutsAddresses(true);
                    }
                }, true);

                scope.updateNutsAddresses = function (isSwitch) {
                    var propertyName;
                    switch (scope.selectedNut.id) {
                        case scope.countryCode:
                            propertyName = 'Country';
                            break;
                        case scope.protectedZoneCode:
                            propertyName = 'ProtectedZone';
                            break;
                        case scope.nuts1Code:
                            propertyName = 'Nuts1';
                            break;
                        case scope.nuts2Code:
                            propertyName = 'Nuts2';
                            break;
                        case scope.districtCode:
                            propertyName = 'District';
                            break;
                        case scope.municipalityCode:
                            propertyName = 'Municipality';
                            break;
                        case scope.settlementCode:
                            propertyName = 'Settlement';
                            break;
                        default:
                    }

                    scope.model.NutsLevel = scope.selectedNut;

                    var initCount = $.map(scope.items, function (val) {
                        if (angular.isDefined(val[propertyName])) {
                            return val;
                        }
                    }).length;

                    var nutsAddresses = scope.initNutsAddresses(propertyName);
                    var areSelecteditemsInited = scope.areSelecteditemsInited(propertyName);

                    // Check areSelecteditemsInited: if all items are loaded
                    if (areSelecteditemsInited || initCount == 1 || isSwitch) {
                        $window[scope.globalKey]['_selected_nuts_addresses'] = nutsAddresses;
                    }
                };

                scope.initNutsAddresses = function (propertyName) {
                    var nutsAddresses = [];
                    scope.items.forEach(function (val) {
                        if (val[propertyName] != null &&
                                angular.isDefined(val[propertyName].id) &&
                                nutsAddresses.map(function (e) { return e.id; }).indexOf(val[propertyName].id) == -1 &&
                                val[propertyName].id !== '') {

                            nutsAddresses.push({
                                id: val[propertyName].id,
                                text: val[propertyName].text,
                                Code: val[propertyName].id,
                                Name: val[propertyName].text,
                                FullPath: val[propertyName].FullPath,
                                FullPathName: val[propertyName].FullPathName
                            });
                        }
                    });

                    return nutsAddresses;
                };

                scope.areSelecteditemsInited = function (propertyName) {
                    var result = true;
                    scope.items.forEach(function (val) {
                        if (angular.isUndefined(val[propertyName])) {
                            result = false;
                        }
                    });
                    return result;
                };

                scope.addItem = function () {
                    var si = { id: '', text: '' };
                    scope.items.push(
                        {
                            Country: si,
                            ProtectedZone: si,
                            Nuts1: si,
                            Nuts2: si,
                            District: si,
                            Municipality: si,
                            Settlement: si,
                            IsCountryValid: true,
                            IsProtectedZoneValid: true,
                            IsNuts1Valid: true,
                            IsNuts2Valid: true,
                            IsDistrictValid: true,
                            IsMunicipalityValid: true,
                            IsSettlementValid: true
                        }
                    );
                };

                scope.delItem = function (item) {
                    scope.items.splice(scope.items.indexOf(item), 1);
                };

                scope.nutClick = function (nut) {
                    scope.selectedNut = nut;
                };
            }
        }
    }]);
}(angular));