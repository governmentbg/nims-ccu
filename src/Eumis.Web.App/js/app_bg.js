import angular from 'angular';
import MainModule from 'js/main/main';
import MainBgModule from 'js/main/main_bg';
import ScaffoldingBgModule from 'js/scaffolding/scaffolding_bg';
import 'angular-i18n/angular-locale_bg-bg';
import 'moment/locale/bg';

const AppBgModule = angular.module('app.bg', [MainModule, MainBgModule, ScaffoldingBgModule]);

export default AppBgModule.name;
