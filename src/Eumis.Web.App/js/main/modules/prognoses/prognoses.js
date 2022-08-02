import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import prognosisDataTemplateUrl from './forms/prognosisData.html';
import { PrognosisDataCtrl } from './forms/prognosisDataCtrl';
import PrognosesProcedureModule from './modules/procedure/procedure';
import PrognosesProgrammeModule from './modules/programme/programme';
import PrognosesProgrammePriorityModule from './modules/programmePriority/programmePriority';
import { PrognosisReportFactory } from './resources/prognosisReport';
import prognosesMonthlyReportTemplateUrl from './views/prognosesMonthlyReport.html';
import { PrognosesMonthlyReportCtrl } from './views/prognosesMonthlyReportCtrl';
import prognosesProgrammePriorityReportTemplateUrl from './views/prognosesProgrammePriorityReport.html';
import { PrognosesProgrammePriorityReportCtrl } from './views/prognosesProgrammePriorityReportCtrl';
import prognosesProgrammeReportTemplateUrl from './views/prognosesProgrammeReport.html';
import { PrognosesProgrammeReportCtrl } from './views/prognosesProgrammeReportCtrl';
import prognosesSummaryReportTemplateUrl from './views/prognosesSummaryReport.html';
import { PrognosesSummaryReportCtrl } from './views/prognosesSummaryReportCtrl';
import prognosesYearlyReportTemplateUrl from './views/prognosesYearlyReport.html';
import { PrognosesYearlyReportCtrl } from './views/prognosesYearlyReportCtrl';

const PrognosesModule = angular
  .module('main.prognoses', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule,

    //submodules
    PrognosesProcedureModule,
    PrognosesProgrammeModule,
    PrognosesProgrammePriorityModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisPrognosisData',
        templateUrl: prognosisDataTemplateUrl,
        controller: PrognosisDataCtrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.yearlyPrognosesReport'                                           , '/yearlyPrognoses'  ,       ['@root'                                            , prognosesYearlyReportTemplateUrl                                                          , PrognosesYearlyReportCtrl                                     ]])
    .state(['root.monthlyPrognosesReport'                                          , '/monthlyPrognoses' ,       ['@root'                                            , prognosesMonthlyReportTemplateUrl                                                         , PrognosesMonthlyReportCtrl                                    ]])
    .state(['root.programmePriorityPrognosesReport'                                , '/ppPrognoses'      ,       ['@root'                                            , prognosesProgrammePriorityReportTemplateUrl                                               , PrognosesProgrammePriorityReportCtrl                          ]])
    .state(['root.programmePrognosesReport'                                        , '/pPrognoses'       ,       ['@root'                                            , prognosesProgrammeReportTemplateUrl                                                       , PrognosesProgrammeReportCtrl                                  ]])
    .state(['root.prognosesSummaryReport'                                          , '/prognosesSummary' ,       ['@root'                                            , prognosesSummaryReportTemplateUrl                                                         , PrognosesSummaryReportCtrl                                    ]]);
    }
  ]);

export default PrognosesModule.name;
PrognosesModule.factory('PrognosisReport', PrognosisReportFactory);
