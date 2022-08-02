// Usage: 

/*global angular*/
(function (angular) {
    'use strict';

    angular.module('scaffolding').directive('eumisCompanySearch', ['$resource', '$timeout', '$window', function ($resource, $timeout, $window) {
        return {
            restrict: 'E',
            scope: {
                company: '=',
                path: '@',
                bgCode: '@',
                resources: '=',
                isPartialReadOnly: '@'
            },
            templateUrl: '/Scripts/angular/directives/eumisCompanySearch/eumisCompanySearch.html',
            link:
            {
                pre: function (scope, element, attrs) {
                    var EmptyItem = { id: '', text: '', Code: '', Name: '' };

                    var refreshSearchState = function () {
                        $timeout(function () {
                            switchSearchDivs(1); // search
                        }, 4000);
                    };

                    var loadcompany = function (result) {
                        scope.company.Name = result.Name;
                        scope.company.NameEN = result.NameAlt;

                        scope.company.CompanyType = EmptyItem;
                        if (result.CompanyType) {
                            scope.company.CompanyType = {
                                id: result.CompanyType.id,
                                text: result.CompanyType.text,
                                Name: result.CompanyType.Name
                            };
                        }

                        scope.company.CompanyLegalType = EmptyItem;
                        if (result.CompanyLegalType) {
                            scope.company.CompanyLegalType = {
                                id: result.CompanyLegalType.id,
                                text: result.CompanyLegalType.text,
                                Name: result.CompanyLegalType.Name,
                                parentId: result.CompanyType.id
                            };
                        }

                        // Seat
                        scope.company.Seat.Country = result.SeatCountry;

                        scope.company.Seat.Settlement = result.SeatSettlement;

                        $.extend(scope.company.Seat, {
                            PostCode: result.SeatPostCode
                        });

                        if (scope.company.Seat.Country !== null && scope.company.Seat.Country.id === scope.bgCode) {
                            $.extend(scope.company.Seat, {
                                Street: result.SeatFullAddress
                            });
                        }
                        else {
                            $.extend(scope.company.Seat, {
                                FullAddress: result.SeatFullAddress
                            });
                        }


                        // Correspondence
                        if (!scope.company.Correspondence) {
                            scope.company.Correspondence = {};
                        }
                        scope.company.Correspondence.Country = result.CorrespondenceCountry;

                        scope.company.Correspondence.Settlement = result.CorrespondenceSettlement;

                        $.extend(scope.company.Correspondence, {
                            PostCode: result.CorrespondencePostCode
                        });

                        if (scope.company.Correspondence.Country !== null && scope.company.Correspondence.Country.id === scope.bgCode) {
                            $.extend(scope.company.Correspondence, {
                                Street: result.CorrespondenceFullAddress
                            });
                        }
                        else {
                            $.extend(scope.company.Correspondence, {
                                FullAddress: result.CorrespondenceFullAddress
                            });
                        }

                        scope.company.Email = result.Email;

                        scope.company.Phone1 = result.Phone1;
                        scope.company.Fax = result.Fax;
                    };

                    scope.company.isSearch = true;
                    scope.company.isPleaseWait = false;
                    scope.company.isOK = false;
                    scope.company.isNoResult = false;
                    scope.company.isValidationError = false;

                    // state: 1: search; 2: pleaseWait; 3: OK; 4: noResult; 5: validationError
                    var switchSearchDivs = function (state) {
                        scope.company.isSearch = false;
                        scope.company.isPleaseWait = false;
                        scope.company.isOK = false;
                        scope.company.isNoResult = false;
                        scope.company.isValidationError = false;

                        $timeout(function () {
                            switch (state) {
                                case 1:
                                    scope.company.isSearch = true;
                                    break;
                                case 2:
                                    scope.company.isPleaseWait = true;
                                    break;
                                case 3:
                                    scope.company.isOK = true;
                                    break;
                                case 4:
                                    scope.company.isNoResult = true;
                                    break;
                                case 5:
                                    scope.company.isValidationError = true;
                                    break;
                            }
                        }, 50);

                    };

                    scope.searchCompany = function () {
                        if (scope.company.Uin && scope.company.UinType && scope.company.UinType.id) {
                            switchSearchDivs(2); // pleaseWait

                            $timeout(function () {
                                var route = $window['_eumis_options']['session'];
                                $resource('/api/' + route + '/companies/SearchCompany/:params')
                                    .save({
                                        uin: scope.company.Uin,
                                        uinType: scope.company.UinType.id,
                                        token: $window.reCaptchaToken
                                    })
                                    .$promise
                                    .then(function (result) {
                                        if (!result) {
                                            switchSearchDivs(4); // noResult
                                        } else if (result.validation === false) {
                                            switchSearchDivs(5); // validationError
                                        } else {
                                            loadcompany(result);
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
                }
            }
        };
    }]);
}(angular));