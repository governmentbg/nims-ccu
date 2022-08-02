import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import checkBlankTopicTemplateUrl from './forms/checkBlankTopic.html';
import { CheckBlankTopicFactory } from './resources/checkBlankTopic';
import checkBlankTopicsEditTemplateUrl from './views/checkBlankTopicsEdit.html';
import { CheckBlankTopicsEditCtrl } from './views/checkBlankTopicsEditCtrl';
import checkBlankTopicsNewTemplateUrl from './views/checkBlankTopicsNew.html';
import { CheckBlankTopicsNewCtrl } from './views/checkBlankTopicsNewCtrl';
import checkBlankTopicsSearchTemplateUrl from './views/checkBlankTopicsSearch.html';
import { CheckBlankTopicsSearchCtrl } from './views/checkBlankTopicsSearchCtrl';

const OperationalMapCheckBlankTopicsModule = angular
  .module('main.operationalMap.checkBlankTopics', [
    UiRouterModule,
    UiBootstrapModule,
    ScaffoldingModule
  ])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisCheckBlankTopicData',
        templateUrl: checkBlankTopicTemplateUrl
      });
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.map.checkBlankTopics'                               , '/checkBlankTopics'                                                                                                                                                                                        ])
    .state(['root.map.checkBlankTopics.search'                        , ''                 ,       ['@root'                           , checkBlankTopicsSearchTemplateUrl                    , CheckBlankTopicsSearchCtrl             ]])
    .state(['root.map.checkBlankTopics.new'                           , '/new?rf'          ,       ['@root'                           , checkBlankTopicsNewTemplateUrl                       , CheckBlankTopicsNewCtrl                ]])
    .state(['root.map.checkBlankTopics.edit'                          , '/:id?rf'          ,       ['@root'                           , checkBlankTopicsEditTemplateUrl                      , CheckBlankTopicsEditCtrl               ]]);
    }
  ]);

export default OperationalMapCheckBlankTopicsModule.name;
OperationalMapCheckBlankTopicsModule.factory('CheckBlankTopic', CheckBlankTopicFactory);
