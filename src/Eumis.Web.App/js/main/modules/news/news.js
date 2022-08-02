import angular from 'angular';
import UiRouterModule from 'angular-ui-router';
import UiBootstrapModule from 'angular-ui-bootstrap';
import ScaffoldingModule from 'js/scaffolding/scaffolding';
import newsDataTemplateUrl from './forms/newsData.html';
import { NewsDataCtrl } from './forms/newsDataCtrl';
import publishNewsModalTemplateUrl from './modals/publishNewsModal.html';
import { PublishNewsModalCtrl } from './modals/publishNewsModalCtrl';
import { NewsFactory } from './resources/news';
import { NewsFileFactory } from './resources/newsFile';
import newsEditTemplateUrl from './views/newsEdit.html';
import { NewsEditCtrl } from './views/newsEditCtrl';
import newsNewTemplateUrl from './views/newsNew.html';
import { NewsNewCtrl } from './views/newsNewCtrl';
import newsSearchTemplateUrl from './views/newsSearch.html';
import { NewsSearchCtrl } from './views/newsSearchCtrl';

const NewsModule = angular
  .module('main.news', [UiRouterModule, UiBootstrapModule, ScaffoldingModule])
  .config([
    'scaffoldingProvider',
    function(scaffoldingProvider) {
      scaffoldingProvider.form({
        name: 'eumisNewsData',
        templateUrl: newsDataTemplateUrl,
        controller: NewsDataCtrl
      });
    }
  ])
  .config([
    'scModalProvider',
    function(scModalProvider) {
      // prettier-ignore
      scModalProvider
    .modal('publishNewsModal'                       , publishNewsModalTemplateUrl                                         , PublishNewsModalCtrl                       , 'sm' );
    }
  ])
  .config([
    '$stateProvider',
    function($stateProvider) {
      // prettier-ignore
      $stateProvider
    .state(['root.news'                                               , '/news?dateFrom&dateTo&type&status'                                                                                                                                                                             ])
    .state(['root.news.search'                                        , ''                 ,       ['@root'                           , newsSearchTemplateUrl                                                           , NewsSearchCtrl                         ]])
    .state(['root.news.new'                                           , '/new'             ,       ['@root'                           , newsNewTemplateUrl                                                              , NewsNewCtrl                            ]])
    .state(['root.news.edit'                                          , '/:id?rf'          ,       ['@root'                           , newsEditTemplateUrl                                                             , NewsEditCtrl                           ]]);
    }
  ]);

export default NewsModule.name;
NewsModule.factory('News', NewsFactory);
NewsModule.factory('NewsFile', NewsFileFactory);
