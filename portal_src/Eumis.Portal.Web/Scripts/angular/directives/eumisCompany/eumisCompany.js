// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('eumisCompany', ['$resource', '$timeout', '$window', function ($resource, $timeout, $window) {
        return {
            restrict: 'E',
            scope: {
                partner: '=',
                path: '@',
                bgCode: '@',
                resources: '=',
                physicalGid: '@'
            },
            templateUrl: '/Scripts/angular/directives/eumisCompany/eumisCompany.html',
            link:
            {
                pre: function (scope, element, attrs) {
                    var EmptyItem = { id: '', text: '', Code: '', Name: '' };

                    var refreshSearchState = function () {
                        $timeout(function () {
                            switchSearchDivs(1); // search
                        }, 4000);
                    };

                    var loadPartner = function (result) {
                        scope.partner.Name = result.Name;
                        scope.partner.NameEN = result.NameAlt;

                        scope.partner.CompanyType = EmptyItem;
                        if (result.CompanyType) {
                            scope.partner.CompanyType = {
                                id: result.CompanyType.id,
                                text: result.CompanyType.text,
                                Name: result.CompanyType.Name
                            };
                        }

                        scope.partner.CompanyLegalType = EmptyItem;
                        if (result.CompanyLegalType) {
                            scope.partner.CompanyLegalType = {
                                id: result.CompanyLegalType.id,
                                text: result.CompanyLegalType.text,
                                Name: result.CompanyLegalType.Name,
                                parentId: result.CompanyType.id
                            };
                        }

                        // Seat
                        scope.partner.Seat.Country = result.SeatCountry;

                        scope.partner.Seat.Settlement = result.SeatSettlement;

                        $.extend(scope.partner.Seat, {
                            PostCode: result.SeatPostCode
                        });

                        if (scope.partner.Seat.Country !== null && scope.partner.Seat.Country.id === scope.bgCode) {
                            $.extend(scope.partner.Seat, {
                                Street: result.SeatFullAddress
                            });
                        }
                        else {
                            $.extend(scope.partner.Seat, {
                                FullAddress: result.SeatFullAddress
                            });
                        }


                        // Correspondence
                        scope.partner.Correspondence.Country = result.CorrespondenceCountry;

                        scope.partner.Correspondence.Settlement = result.CorrespondenceSettlement;

                        $.extend(scope.partner.Correspondence, {
                            PostCode: result.CorrespondencePostCode
                        });

                        if (scope.partner.Correspondence.Country !== null && scope.partner.Correspondence.Country.id === scope.bgCode) {
                            $.extend(scope.partner.Correspondence, {
                                Street: result.CorrespondenceFullAddress
                            });
                        }
                        else {
                            $.extend(scope.partner.Correspondence, {
                                FullAddress: result.CorrespondenceFullAddress
                            });
                        }

                        // contact info
                        if (!scope.partner.isCandidate) {
                            scope.partner.Email = result.Email;
                        }
                        scope.partner.Phone1 = result.Phone1;
                        scope.partner.Fax = result.Fax;
                    };

                    scope.partner.isSearch = true;
                    scope.partner.isPleaseWait = false;
                    scope.partner.isOK = false;
                    scope.partner.isNoResult = false;
                    scope.partner.isValidationError = false;
                    scope.partner.isCandidate = false;
                    if ("candidate" in attrs) {
                        scope.partner.isCandidate = true;
                    }
                    scope.partner.disableSearch = false;
                    if ("disablesearch" in attrs) {
                        scope.partner.disableSearch = true;
                    }

                    // state: 1: search; 2: pleaseWait; 3: OK; 4: noResult; 5: validationError
                    var switchSearchDivs = function (state) {
                        scope.partner.isSearch = false;
                        scope.partner.isPleaseWait = false;
                        scope.partner.isOK = false;
                        scope.partner.isNoResult = false;
                        scope.partner.isValidationError = false;

                        $timeout(function () {
                            switch (state) {
                                case 1:
                                    scope.partner.isSearch = true;
                                    break;
                                case 2:
                                    scope.partner.isPleaseWait = true;
                                    break;
                                case 3:
                                    scope.partner.isOK = true;
                                    break;
                                case 4:
                                    scope.partner.isNoResult = true;
                                    break;
                                case 5:
                                    scope.partner.isValidationError = true;
                                    break;
                            }
                        }, 50);

                    };

                    scope.searchCompany = function () {
                        if (scope.partner.Uin && scope.partner.UinType && scope.partner.UinType.id) {
                            switchSearchDivs(2); // pleaseWait

                            $timeout(function () {
                                var route = $window['_eumis_options']['session'];
                                $resource('/api/' + route + '/companies/SearchCompany/:params')
                                    .save({
                                        uin: scope.partner.Uin,
                                        uinType: scope.partner.UinType.id,
                                        token: $window.reCaptchaToken
                                    })
                                    .$promise
                                    .then(function (result) {
                                        if (!result) {
                                            switchSearchDivs(4); // noResult
                                        } else if (result.validation === false) {
                                            switchSearchDivs(5); // validationError
                                        } else {
                                            loadPartner(result);
                                            $timeout(function () {
                                                switchSearchDivs(3); // OK
                                            }, 500);
                                        }
                                    }, function (error) {
                                        switchSearchDivs(4); // noResult
                                    })
                                    .finally(function () {
                                        // Always execute this on both error and success
                                        refreshSearchState();
                                    });
                            }, 200);
                        } else {
                            switchSearchDivs(5); // validationError
                            refreshSearchState();
                        }
                    };

                    scope.copySeatAddress = function () {
                        scope.partner.Correspondence = {};
                        scope.upgradeCorrespondence(scope.partner.Correspondence, scope.partner.Seat);
                        scope.partner.Correspondence.Country = EmptyItem;
                        scope.partner.Correspondence.Settlement = EmptyItem;

                        if (scope.partner.Seat.Country) {
                            scope.partner.Correspondence.Country = {
                                id: scope.partner.Seat.Country.id,
                                text: scope.partner.Seat.Country.text,
                                Code: scope.partner.Seat.Country.Code,
                                Name: scope.partner.Seat.Country.Name,
                                NameEN: scope.partner.Seat.Country.NameEN
                            };
                        }

                        if (scope.partner.Seat.Settlement) {
                            scope.partner.Correspondence.Settlement = {
                                id: scope.partner.Seat.Settlement.id,
                                text: scope.partner.Seat.Settlement.text,
                                Code: scope.partner.Seat.Settlement.Code,
                                Name: scope.partner.Seat.Settlement.Name,
                                NameEN: scope.partner.Seat.Settlement.NameEN
                            };
                        }

                        if (scope.partner.Seat) {
                            scope.partner.Correspondence.PostCode = scope.partner.Seat.PostCode;
                            scope.partner.Correspondence.Street = scope.partner.Seat.Street;
                            scope.partner.Correspondence.FullAddress = scope.partner.Seat.FullAddress;
                        }
                    };

                    scope.upgradeCorrespondence = function (correspondece, seat) {
                        correspondece.IsCountryValid = seat.IsCountryValid;
                        correspondece.IsSettlementValid = seat.IsSettlementValid;
                        correspondece.IsPostCodeValid = seat.IsPostCodeValid;
                        correspondece.IsStreetValid = seat.IsStreetValid;
                        correspondece.IsFullAddressValid = seat.IsFullAddressValid;
                    };
                }
            }
        };
    }]);
}(angular));