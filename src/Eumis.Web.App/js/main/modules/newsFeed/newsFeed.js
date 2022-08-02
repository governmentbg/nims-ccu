import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import { NewsFeedFileFactory } from './resources/newsFeedFile';
import allNewsSearchTemplateUrl from './views/allNewsSearch.html';
import { AllNewsSearchCtrl } from './views/allNewsSearchCtrl';
import newsFeedSearchTemplateUrl from './views/newsFeedSearch.html';
import { NewsFeedSearchCtrl } from './views/newsFeedSearchCtrl';
import newsFeedViewTemplateUrl from './views/newsFeedView.html';
import { NewsFeedViewCtrl } from './views/newsFeedViewCtrl';

const NewsFeedModule = angular
  .module('main.newsFeed', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.newsFeed'                                           , '/'                ,       ['@root'                           , newsFeedSearchTemplateUrl                                                   , NewsFeedSearchCtrl                     ]])
    .state(['root.newsFeed.view'                                      , 'newsFeed/:id'     ,       ['@root'                           , newsFeedViewTemplateUrl                                                     , NewsFeedViewCtrl                       ]])
    .state(['root.allNews'                                            , '/allNews?p'                                                                                                                                                                                               ])
    .state(['root.allNews.search'                                     , ''                 ,       ['@root'                           , allNewsSearchTemplateUrl                                                    , AllNewsSearchCtrl                      ]])
    .state(['root.allNews.view'                                       , '/:id'             ,       ['@root'                           , newsFeedViewTemplateUrl                                                     , NewsFeedViewCtrl                       ]]);
    }
  ]);

export default NewsFeedModule.name;
NewsFeedModule.factory('NewsFeedFile', NewsFeedFileFactory);
