import angular from 'angular';
import _ from 'lodash';

function RootCtrl(
  moment,
  $scope,
  $state,
  $timeout,
  $http,
  l10n,
  $window,
  scModal,
  scMessage,
  $exceptionHandler,
  rootViewConfig
) {
  var newMsgRequestProcessing = false,
    newNtfcRequestProcessing = false,
    prevNewMsgRequest,
    prevNewNtfcRequest;

  $http({
    method: 'GET',
    url: 'api/users/current'
  }).then(function(result) {
    $scope.currentUser = result.data;
    $scope.userFullname = result.data.fullname ? result.data.fullname : result.data.username;
    if (typeof dtrum !== 'undefined') {
      // eslint-disable-next-line no-undef
      dtrum.identifyUser(result.data.email);
    }
  });

  $scope.alerts = [];
  $scope.navigationTree = rootViewConfig.navigationTree;

  $scope.fLvlSelectedItemId = null;
  $scope.fLvlSelectedItem = null;
  $scope.sLvlSelectedItemId = null;
  $scope.sLvlSelectedItem = null;
  $scope.showTLevelEmptyElem = true;

  $scope.pendingElement = null;

  _($scope.navigationTree).forEach(function(fLevelItem, id) {
    if (fLevelItem.isActive) {
      $scope.fLvlSelectedItemId = id;
      $scope.fLvlSelectedItem = fLevelItem;
    }
  });

  $scope.getThirdLevelItems = function(fLvlSelectedItemId, sLvlSelectedItemId) {
    if (
      fLvlSelectedItemId === null ||
      fLvlSelectedItemId === undefined ||
      sLvlSelectedItemId === null ||
      sLvlSelectedItemId === undefined
    ) {
      return [];
    }

    return _.filter(
      $scope.navigationTree[fLvlSelectedItemId].items[sLvlSelectedItemId].items,
      function(i) {
        return !i.visibilityProp || $scope.currentUser.userVisibility[i.visibilityProp];
      }
    );
  };

  $scope.clickHandler = function(element, $event) {
    if ($event.which !== 1) {
      // not left click
      return;
    }

    $event.preventDefault();

    if ($state.current.name === element.state && $state.current.params === element.stateParams) {
      $scope.menuHandler();
      return;
    }

    if (element.$pending) {
      return;
    }

    var result = $state.go(element.state, element.stateParams, { inherit: false });

    // check if the result is promise
    if (result && result.then && typeof result.then === 'function') {
      element.$pending = true;
      result['catch'](function(error) {
        $exceptionHandler(error);
      })['finally'](function() {
        delete element.$pending;
        $scope.menuHandler();
      });
    }
  };

  $scope.firstLevelHandler = function(id, fLevelItem) {
    $scope.fLvlSelectedItemId = id;
    $scope.fLvlSelectedItem = fLevelItem;

    $scope.sLvlSelectedItemId = null;
    $scope.sLvlSelectedItem = null;

    $scope.showTLevelEmptyElem = true;

    if (fLevelItem.func) {
      return $scope[fLevelItem.func]();
    }
  };

  $scope.secondLevelHandler = function(id, sLevelItem, $event) {
    $scope.sLvlSelectedItemId = id;
    $scope.sLvlSelectedItem = sLevelItem;

    if (sLevelItem.state) {
      $scope.showTLevelEmptyElem = true;
      $scope.clickHandler(sLevelItem, $event);
    } else if (sLevelItem.func) {
      $scope.menuHandler();
      return $scope[sLevelItem.func]();
    } else {
      $scope.showTLevelEmptyElem = false;
    }
  };

  $scope.thirdLevelHandler = function(tLevelItem, $event) {
    $scope.clickHandler(tLevelItem, $event);
  };

  $scope.menuHandler = function() {
    if ($('.menu-wrapper').is(':visible')) {
      $('.menu-wrapper').slideUp(400, function() {
        $('a.logo, span.username, span.messages, span.notifications, a.logout').fadeIn(200);
        $('header').resize();
        $('.menu-toggle img.opened').hide();
        $('.menu-toggle img.closed').show();
      });
    } else {
      $('a.logo, span.username, span.messages, span.notifications, a.logout').fadeOut(
        200,
        function() {
          $('.menu-wrapper').slideDown(400, function() {
            $('header').resize();
            $('.menu-toggle img.opened').show();
            $('.menu-toggle img.closed').hide();
          });
        }
      );
    }
  };

  $('.menu-toggle').on('click', function() {
    $scope.$apply($scope.menuHandler());
  });

  $scope.logoutUrl = '/logout';

  $scope.logout = function() {
    $window.location.assign($scope.logoutUrl + $window.location.hash);
  };

  $scope.menuLogout = function() {
    return scMessage('common_messages_confirmLogout', [
      {
        name: 'YES',
        l10nLabel: 'common_texts_yes',
        type: 'btn-danger',
        icon: 'glyphicon-ok'
      },
      {
        name: 'NO',
        l10nLabel: 'common_texts_no',
        type: 'btn-success',
        icon: 'glyphicon-remove'
      }
    ]).then(function(result) {
      if (result === 'YES') {
        return $scope.logout();
      }
    });
  };

  $scope.viewUserProfile = function() {
    return $state.go('root.userProfile.view.regData');
  };

  $scope.viewMessages = function() {
    return $state.go('root.messages.inbox');
  };

  $scope.viewNotifications = function() {
    return $state.go('root.userNotifications.search');
  };

  $scope.changePassword = function changePassword() {
    var modalInstance = scModal.open('changePasswordModal');
    modalInstance.result.catch(angular.noop);
    return modalInstance.opened;
  };

  $scope.removeAlert = function(alert) {
    var index = $scope.alerts.indexOf(alert);
    //check if it has already been removed by the user or a timeout
    if (index >= 0) {
      $scope.alerts.splice(index, 1);
    }
  };

  $scope.$on('alert', function(event, msg, type) {
    try {
      var alert = { message: msg, type: type };
      $scope.alerts.push(alert);

      //remove the alert after 60 seconds
      $timeout(function() {
        $scope.removeAlert(alert);
      }, 60 * 1000);
    } catch (e) {
      //swallow all exception so that we don't end up in an infinite loop
    }
  });

  $scope.$on('$stateChangeSuccess', function() {
    if (
      !newMsgRequestProcessing &&
      (!prevNewMsgRequest || moment().diff(prevNewMsgRequest, 'minutes') >= 1)
    ) {
      newMsgRequestProcessing = true;

      $http({
        method: 'GET',
        url: 'api/messages/newMessages'
      }).then(function(result) {
        prevNewMsgRequest = moment();
        newMsgRequestProcessing = false;
        $scope.newMessagesNum = parseInt(result.data, 10);
      });
    }
  });

  $scope.$on('$stateChangeSuccess', function() {
    if (
      !newNtfcRequestProcessing &&
      (!prevNewNtfcRequest || moment().diff(prevNewNtfcRequest, 'minutes') >= 1)
    ) {
      newNtfcRequestProcessing = true;

      $http({
        method: 'GET',
        url: 'api/notifications/newNotifications'
      }).then(function(result) {
        prevNewNtfcRequest = moment();
        newNtfcRequestProcessing = false;
        $scope.newNotificationsNum = parseInt(result.data, 10);
      });
    }
  });
}

RootCtrl.$inject = [
  'moment',
  '$scope',
  '$state',
  '$timeout',
  '$http',
  'l10n',
  '$window',
  'scModal',
  'scMessage',
  '$exceptionHandler',
  'rootViewConfig'
];

RootCtrl.$resolve = {
  rootViewConfig: [
    'rootView',
    function(rootView) {
      return rootView;
    }
  ]
};

export { RootCtrl };

export const rootViewFactory = [
  'Guidance',
  'GuidanceNavFile',
  function rootViewConfig(Guidance, GuidanceNavFile) {
    return Guidance.getNavGuidances().$promise.then(function(navGuidances) {
      var helpModule = _.map(navGuidances, function(item) {
        return {
          text: item.description,
          visibilityProp: 'isHelpSupportVisible',
          href: GuidanceNavFile.getUrl({
            id: item.guidanceId,
            fileKey: item.fileKey
          }),
          class: 'no-children',
          link: true
        };
      });

      helpModule.push({
        text: 'navigation_help_support',
        visibilityProp: 'isHelpSupportVisible',
        textOnly: true
      });

      return {
        navigationTree: [
          {
            text: 'navigation_modules_title',
            class: 'menu-modules',
            isActive: true,
            items: [
              {
                text: 'navigation_modules_systemInformation_title',
                visibilityProp: 'isOperationalMapVisible',
                items: [
                  { state: 'root.map.tree', text: 'navigation_modules_systemInformation_map' },
                  {
                    state: 'root.map.measures.search',
                    text: 'navigation_modules_systemInformation_measures'
                  },
                  {
                    state: 'root.map.indicators.search',
                    text: 'navigation_modules_systemInformation_indicators',
                    visibilityProp: 'areIndicatorsVisible'
                  },
                  {
                    state: 'root.map.indicatorTypes.search',
                    text: 'navigation_modules_systemInformation_indicatorTypes',
                    visibilityProp: 'areIndicatorsVisible'
                  },
                  {
                    state: 'root.map.expenseTypes.search',
                    text: 'navigation_modules_systemInformation_expenseTypes'
                  },
                  {
                    state: 'root.map.declarations.search',
                    text: 'navigation_modules_systemInformation_declarations',
                    visibilityProp: 'areDeclarationsVisible'
                  },
                  {
                    state: 'root.map.directions.search',
                    text: 'navigation_modules_systemInformation_directions'
                  },
                  {
                    state: 'root.procurements.search',
                    text: 'navigation_modules_systemInformation_procurements'
                  }
                ]
              },
              {
                text: 'navigation_modules_procedures',
                visibilityProp: 'areProceduresVisible',
                items: [
                  {
                    state: 'root.procedures.tree',
                    text: 'navigation_modules_procedures_map'
                  },
                  {
                    state: 'root.procedures.search',
                    text: 'navigation_modules_procedures_search'
                  }
                ]
              },
              {
                state: 'root.companies.search',
                text: 'navigation_modules_companies',
                visibilityProp: 'areCompaniesVisible',
                class: 'no-children'
              },
              {
                text: 'navigation_modules_profiles',
                visibilityProp: 'isProfilesModuleVisible',
                items: [
                  {
                    state: 'root.registrations.search',
                    text: 'navigation_modules_profiles_registrations',
                    visibilityProp: 'areRegistratiosVisible'
                  },
                  {
                    state: 'root.contractRegistrations.search',
                    text: 'navigation_modules_profiles_contractRegistrations',
                    visibilityProp: 'areContractRegistratiosVisible'
                  },
                  {
                    state: 'root.contractAccessCodes.search',
                    text: 'navigation_modules_profiles_contractAccessCodes',
                    visibilityProp: 'areContractAccessCodesVisible'
                  }
                ]
              },
              {
                text: 'navigation_modules_projects',
                visibilityProp: 'isProjectsModuleVisible',
                items: [
                  {
                    state: 'root.projects.search',
                    text: 'navigation_modules_projects',
                    visibilityProp: 'areProjectsVisible'
                  },
                  {
                    state: 'root.projectCommunications.search',
                    text: 'navigation_modules_projects_communications',
                    visibilityProp: 'areProjectCommunicationsVisible'
                  },
                  {
                    state: 'root.projectMassManagingAuthorityCommunications.search',
                    text: 'navigation_modules_projects_massCommunications',
                    visibilityProp: 'areProjectCommunicationsVisible'
                  }
                ]
              },
              {
                state: 'root.projectDossier',
                text: 'navigation_modules_projectDossier',
                visibilityProp: 'isProjectDossierVisible',
                class: 'no-children'
              },
              {
                text: 'navigation_modules_contracts',
                visibilityProp: 'isContractModuleVisible',
                items: [
                  {
                    state: 'root.contracts.search',
                    text: 'navigation_modules_contracts_search',
                    visibilityProp: 'areContractsVisible'
                  },
                  {
                    state: 'root.contractCommunications.search',
                    text: 'navigation_modules_contracts_communication',
                    visibilityProp: 'areContractCommunicationsVisible'
                  },
                  {
                    state: 'root.procedureMassCommunications.search',
                    text: 'navigation_modules_procedure_massCommunication',
                    visibilityProp: 'areContractCommunicationsVisible'
                  },
                  {
                    state: 'root.contractReports.search',
                    text: 'navigation_modules_contracts_report',
                    visibilityProp: 'areContractReportsVisible'
                  }
                ]
              },
              {
                text: 'navigation_modules_monitoringFinancialControl',
                visibilityProp: 'isMonitoringFinancialControlModuleVisible',
                items: [
                  {
                    state: 'root.contractReportChecks.search',
                    text: 'navigation_modules_monitoringFinancialControl_contractReportChecks'
                  },
                  {
                    state: 'root.financialCorrections.search',
                    text: 'navigation_modules_monitoringFinancialControl_financialCorrection'
                  },
                  {
                    state: 'root.actuallyPaidAmounts.search',
                    text: 'navigation_modules_monitoringFinancialControl_actuallyPaidAmounts'
                  },
                  {
                    state: 'root.contractReimbursedAmounts.search',
                    text: 'navigation_modules_monitoringFinancialControl_contractReimbursedAmounts'
                  },
                  {
                    state: 'root.contractReportTechnicalCorrections.search',
                    text:
                      'navigation_modules_monitoringFinancialControl_contractReportTechnicalCorrections',
                    visibilityProp: 'false'
                  },
                  {
                    state: 'root.contractReportFinancialCorrections.search',
                    text:
                      'navigation_modules_monitoringFinancialControl_contractReportFinancialCorrections'
                  },
                  {
                    state: 'root.contractReportCorrections.search',
                    text: 'navigation_modules_monitoringFinancialControl_contractReportCorrections'
                  }
                ]
              },
              {
                text: 'navigation_modules_certification',
                visibilityProp: 'isCertificationModuleVisible',
                items: [
                  {
                    state: 'root.certReportChecks.search',
                    text: 'navigation_modules_certification_certReportChecks',
                    visibilityProp: 'areCertReportChecksVisible'
                  },
                  {
                    state: 'root.certAuthorityChecks.search',
                    text: 'navigation_modules_certification_certAuthorityChecks',
                    visibilityProp: 'areCertAuthorityChecksVisible'
                  },
                  {
                    state: 'root.euReimbursedAmounts.search',
                    text: 'navigation_modules_certification_euReimbursedAmounts',
                    visibilityProp: 'areEuReimbursedAmountsVisible'
                  },
                  {
                    state: 'root.contractReportCertAuthorityFinancialCorrections.search',
                    text:
                      'navigation_modules_certification_contractReportCertAuthorityFinancialCorrections',
                    visibilityProp: 'areCertReportChecksVisible'
                  },
                  {
                    state: 'root.contractReportCertAuthorityCorrections.search',
                    text: 'navigation_modules_certification_contractReportCertAuthorityCorrections',
                    visibilityProp: 'areCertReportChecksVisible'
                  },
                  {
                    state:
                      'root.contractReportRevalidationCertAuthorityFinancialCorrections.search',
                    text:
                      'navigation_modules_certification_contractReportRevalidationCertAuthorityFinancialCorrections',
                    visibilityProp: 'areCertReportChecksVisible'
                  },
                  {
                    state: 'root.contractReportRevalidationCertAuthorityCorrections.search',
                    text:
                      'navigation_modules_certification_contractReportRevalidationCertAuthorityCorrections',
                    visibilityProp: 'areCertReportChecksVisible'
                  },
                  {
                    state: 'root.annualAccountReports.search',
                    text: 'navigation_modules_certification_annualAccountReports',
                    visibilityProp: 'areCertAuthorityChecksVisible'
                  },
                  {
                    state: 'root.certAuthorityCommunications.search',
                    text: 'navigation_modules_certification_communication',
                    visibilityProp: 'areCertAuthorityCommunicationsVisible'
                  }
                ]
              },
              {
                text: 'navigation_modules_monitoring',
                visibilityProp: 'isMonitoringVisible',
                items: [
                  {
                    state: 'root.monitoringAdvancePayments',
                    text: 'navigation_modules_monitoring_аdvancePayments'
                  },
                  //{ state: 'root.monitoringProjects'                     , text: 'navigation_modules_monitoring_projects'                     },
                  {
                    state: 'root.monitoringContracts',
                    text: 'navigation_modules_monitoring_contracts'
                  },
                  {
                    state: 'root.monitoringIndicators',
                    text: 'navigation_modules_monitoring_indicators',
                    visibilityProp: 'areIndicatorsVisible'
                  },
                  {
                    state: 'root.monitoringContractReports',
                    text: 'navigation_modules_monitoring_contractReports'
                  },
                  {
                    state: 'root.monitoringBudgetLevels',
                    text: 'navigation_modules_monitoring_budgetLevels'
                  },
                  {
                    state: 'root.monitoringFinancialCorrections',
                    text: 'navigation_modules_monitoring_financialCorrections'
                  },
                  {
                    state: 'root.monitoringConcludedContracts',
                    text: 'navigation_modules_monitoring_concludedContracts'
                  },
                  {
                    state: 'root.monitoringBeneficiaryProjectsContracts',
                    text: 'navigation_modules_monitoring_beneficiaryProjectsContracts'
                  },
                  {
                    state: 'root.monitoringEvaluations',
                    text: 'navigation_modules_monitoring_evaluations'
                  },
                  {
                    state: 'root.monitoringContractReportPayments',
                    text: 'navigation_modules_monitoring_contractReportPayments'
                  },
                  {
                    state: 'root.monitoringExpenseTypes',
                    text: 'navigation_modules_monitoring_expenseTypes'
                  },
                  {
                    state: 'root.monitoringSebra',
                    text: 'navigation_modules_monitoring_sebra'
                  }
                ]
              },
              {
                text: 'navigation_modules_interfaces',
                visibilityProp: 'areAllInterfacesVisible',
                items: [
                  {
                    state: 'root.regix.view.validPerson',
                    text: 'navigation_modules_interfaces_regix',
                    visibilityProp: 'isRegixInterfaceVisible'
                  },
                  {
                    state: 'root.monitorstat.search',
                    text: 'navigation_modules_interfaces_monitorstat',
                    visibilityProp: 'isRegixInterfaceVisible'
                  }
                ]
              },
              {
                text: 'navigation_modules_users_title',
                visibilityProp: 'areUsersVisible',
                items: [
                  {
                    state: 'root.users.search',
                    text: 'navigation_modules_users_users',
                    visibilityProp: 'areUserProfilesVisible'
                  },
                  {
                    state: 'root.pTemplates.search',
                    text: 'navigation_modules_users_pTemplates',
                    visibilityProp: 'arePTemplatesVisible'
                  },
                  {
                    state: 'root.userTypes.search',
                    text: 'navigation_modules_users_userTypes',
                    visibilityProp: 'areUserTypesVisible'
                  },
                  {
                    state: 'root.requestPackages.search',
                    text: 'navigation_modules_users_requestPackages',
                    visibilityProp: 'areRequestPackagesVisible'
                  },
                  {
                    state: 'root.userOrganizations.search',
                    text: 'navigation_modules_users_userOrganizations',
                    visibilityProp: 'areUserOrganizationsVisible'
                  }
                ]
              },
              {
                text: 'navigation_modules_communications_title',
                visibilityProp: 'areComunicationsVisible',
                items: [
                  {
                    state: 'root.news.search',
                    text: 'navigation_modules_communications_news',
                    visibilityProp: 'areNewsVisible'
                  },
                  {
                    state: 'root.guidances.search',
                    text: 'navigation_modules_communications_guidances',
                    visibilityProp: 'areGuidancesVisible'
                  }
                ]
              },
              {
                text: 'navigation_modules_actionLogs_title',
                visibilityProp: 'isActionLogVisible',
                items: [
                  {
                    state: 'root.internalActionLogs.search',
                    text: 'navigation_modules_actionLogs_internalActionLogs',
                    visibilityProp: 'isActionLogVisible'
                  },
                  {
                    state: 'root.procedureActionLogs.search',
                    text: 'navigation_modules_actionLogs_procedureActionLogs',
                    visibilityProp: 'isActionLogVisible'
                  },
                  {
                    state: 'root.portalActionLogs.search',
                    text: 'navigation_modules_actionLogs_portalActionLogs',
                    visibilityProp: 'isActionLogVisible'
                  },
                  {
                    state: 'root.loginActionLogs.search',
                    text: 'navigation_modules_actionLogs_loginActionLogs',
                    visibilityProp: 'isActionLogVisible'
                  }
                ]
              },
              {
                state: 'root.evalSessions.search',
                text: 'navigation_modules_evalSessions',
                visibilityProp: 'areEvalSessionsVisible',
                class: 'no-children'
              }
            ]
          },
          {
            text: 'navigation_profile_title',
            class: 'menu-profile',
            items: [
              {
                state: 'root.userProfile.view.regData',
                text: 'navigation_profile_userProfile',
                class: 'no-children'
              },
              {
                state: 'root.messages.inbox',
                text: 'navigation_profile_messages',
                visibilityProp: 'areMessagesVisible',
                class: 'no-children'
              },
              {
                state: 'root.userNotifications.search',
                text: 'navigation_profile_userNotifications',
                class: 'no-children'
              },
              {
                func: 'changePassword',
                text: 'navigation_profile_changePassword',
                visibilityProp: 'isChangePasswordVisible',
                class: 'no-children'
              },
              {
                state: 'root.notificationSettings.search',
                text: 'navigation_profile_notificationSettings',
                class: 'no-children'
              }
            ]
          },
          {
            text: 'navigation_help_title',
            class: 'menu-help',
            items: helpModule
          }
        ]
      };
    });
  }
];
