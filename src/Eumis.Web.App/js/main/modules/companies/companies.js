import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import companyDataTemplateUrl from './forms/companyData.html';
import { CompanyDataCtrl } from './forms/companyDataCtrl';
import { CompanyFactory } from './resources/company';
import companiesEditTemplateUrl from './views/companiesEdit.html';
import { CompaniesEditCtrl } from './views/companiesEditCtrl';
import companiesNewTemplateUrl from './views/companiesNew.html';
import { CompaniesNewCtrl } from './views/companiesNewCtrl';
import companiesSearchTemplateUrl from './views/companiesSearch.html';
import { CompaniesSearchCtrl } from './views/companiesSearchCtrl';

const CompaniesModule = angular
  .module('main.companies', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisCompanyData',
        templateUrl: companyDataTemplateUrl,
        controller: CompanyDataCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.companies'                                          , '/companies?name&uinTypeId&uin'                                                                                                                                                                            ])
    .state(['root.companies.search'                                   , ''                 ,       ['@root'                           , companiesSearchTemplateUrl                                                 , CompaniesSearchCtrl                    ]])
    .state(['root.companies.new'                                      , '/new'             ,       ['@root'                           , companiesNewTemplateUrl                                                    , CompaniesNewCtrl                       ]])
    .state(['root.companies.edit'                                     , '/:id?rf'          ,       ['@root'                           , companiesEditTemplateUrl                                                   , CompaniesEditCtrl                      ]])
    }
  ]);

export default CompaniesModule.name;
CompaniesModule.factory('Company', CompanyFactory);
