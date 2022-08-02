const modules = {
  'ngResource': ['angular-resource', 'NgResourceModule'],
  'ui.bootstrap': ['angular-ui-bootstrap/ui-bootstrap-tpls', 'UiBootstrapModule'],
  'ui.router': ['angular-ui-router', 'UiRouterModule'],
  'ui.sortable': ['angular-ui-sortable/src/sortable', 'UiSortableModule'],
  'angularMoment': ['angular-moment', 'AngularMomentModule'],
  'l10n-tools': ['l10n-angular/build/l10n-with-tools', 'AngularL10NModule'],
  'chart.js': ['angular-chart.js', 'ChartJsModule'],
  'angularBootstrapNavTree': ['angular-bootstrap-nav-tree', 'AngularBootstrapNavTreeModule'],
  'ui.select2': ['angular-ui-select2/src/select2', 'UiSelect2Module'],
  'ui.jq': ['angular-ui-utils/modules/jq/jq', 'UiJqModule'],
  
  'boot': ['js/boot'],
  'authorizer': ['js/authorizer'],
  'app': ['js/app'],
  'app.bg': ['js/app_bg'],

  'main': ['js/main/main'],
  'main.bg': ['js/main/main_bg'],
  'main.actionLogs': ['js/main/modules/actionLogs/actionLogs'],
  'main.actuallyPaidAmounts': ['js/main/modules/actuallyPaidAmounts/actuallyPaidAmounts'],
  'main.audits': ['js/main/modules/audits/audits'],
  'main.certAuthorityChecks': ['js/main/modules/certAuthorityChecks/certAuthorityChecks'],
  'main.certReports': ['js/main/modules/certReports/certReports'],
  'main.certReports.checks': ['./modules/checks/checks'],
  'main.certReports.snapshots': ['./modules/snapshots/snapshots'],
  'main.companies': ['js/main/modules/companies/companies'],
  'main.compensationDocuments': ['js/main/modules/compensationDocuments/compensationDocuments'],
  'main.contractCommunications': ['js/main/modules/contractCommunications/contractCommunications'],
  'main.contractCommunications.adminAuthority': ['./modules/adminAuthority/adminAuthority'],
  'main.contractCommunications.auditAuthority': ['./modules/auditAuthority/auditAuthority'],
  'main.contractCommunications.certAuthority': ['./modules/certAuthority/certAuthority'],
  'main.contractRegistrations': ['js/main/modules/contractRegistrations/contractRegistrations'],
  'main.contractReportCertCorrections': ['js/main/modules/contractReportCertCorrections/contractReportCertCorrections'],
  'main.contractReportChecks': ['js/main/modules/contractReportChecks/contractReportChecks'],
  'main.contractReportCorrections': ['js/main/modules/contractReportCorrections/contractReportCorrections'],
  'main.contractReportFinancialCertCorrections': ['js/main/modules/contractReportFinancialCertCorrections/contractReportFinancialCertCorrections'],
  'main.contractReportFinancialCorrections': ['js/main/modules/contractReportFinancialCorrections/contractReportFinancialCorrections'],
  'main.contractReportFinancialRevalidations': ['js/main/modules/contractReportFinancialRevalidations/contractReportFinancialRevalidations'],
  'main.contractReportRevalidations': ['js/main/modules/contractReportRevalidations/contractReportRevalidations'],
  'main.contractReports': ['js/main/modules/contractReports/contractReports'],
  'main.contracts': ['js/main/modules/contracts/contracts'],
  'main.debts': ['js/main/modules/debts/debts'],
  'main.euReimbursedAmounts': ['js/main/modules/euReimbursedAmounts/euReimbursedAmounts'],
  'main.evalSessions': ['js/main/modules/evalSessions/evalSessions'],
  'main.financialCorrections': ['js/main/modules/financialCorrections/financialCorrections'],
  'main.fiReimbursedAmounts': ['js/main/modules/fiReimbursedAmounts/fiReimbursedAmounts'],
  'main.flatFinancialCorrections': ['js/main/modules/flatFinancialCorrections/flatFinancialCorrections'],
  'main.guidances': ['js/main/modules/guidances/guidances'],
  'main.interfaces': ['js/main/modules/interfaces/interfaces'],
  'main.irregularities': ['js/main/modules/irregularities/irregularities'],
  'main.irregularitySignals': ['js/main/modules/irregularitySignals/irregularitySignals'],
  'main.messages': ['js/main/modules/messages/messages'],
  'main.monitoring': ['js/main/modules/monitoring/monitoring'],
  'main.news': ['js/main/modules/news/news'],
  'main.newsFeed': ['js/main/modules/newsFeed/newsFeed'],
  'main.operationalMap': ['js/main/modules/operationalMap/operationalMap'],
  'main.operationalMap.allowances': ['./modules/allowances/allowances'],
  'main.operationalMap.basicInterestRates': ['./modules/basicInterestRates/basicInterestRates'],
  'main.operationalMap.checkBlankTopics': ['./modules/checkBlankTopics/checkBlankTopics'],
  'main.operationalMap.expenseTypes': ['./modules/expenseTypes/expenseTypes'],
  'main.operationalMap.indicators': ['./modules/indicators/indicators'],
  'main.operationalMap.interestSchemes': ['./modules/interestSchemes/interestSchemes'],
  'main.operationalMap.nodes': ['./modules/nodes/nodes'],
  'main.operationalMap.measures': ['./modules/measures/measures'],
  'main.procedures': ['js/main/modules/procedures/procedures'],
  'main.prognoses': ['js/main/modules/prognoses/prognoses'],
  'main.prognoses.procedure': ['./modules/procedure/procedure'],
  'main.prognoses.programme': ['./modules/programme/programme'],
  'main.prognoses.programmePriority': ['./modules/programmePriority/programmePriority'],
  'main.projectDossier': ['js/main/modules/projectDossier/projectDossier'],
  'main.projects': ['js/main/modules/projects/projects'],
  'main.registrations': ['js/main/modules/registrations/registrations'],
  'main.reimbursedAmounts': ['js/main/modules/reimbursedAmounts/reimbursedAmounts'],
  'main.reimbursedAmounts.contracts': ['./modules/contracts/contracts'],
  'main.reimbursedAmounts.debts': ['./modules/debts/debts'],
  'main.sapInterfaces': ['js/main/modules/sapInterfaces/sapInterfaces'],
  'main.spotChecks': ['js/main/modules/spotChecks/spotChecks'],
  'main.spotChecks.checks': ['./modules/checks/checks'],
  'main.spotChecks.plans': ['./modules/plans/plans'],
  'main.users': ['js/main/modules/users/users'],

  'scaffolding': ['js/scaffolding/scaffolding'],
  'scaffolding.bg': ['js/scaffolding/scaffolding_bg'],

  'testapp': ['./testapp'],
  'testapp.bg': ['./testapp_bg'],
};

const toBeRemoved = [
  'main.templates',
  'scaffolding.templates',
  'test.templates',
];

module.exports = function transformer(file, api) {
  const j = api.jscodeshift;
  const { expression, statement, statements } = j.template;
  const {
    createModuleIdentifier,
    getAngularModuleExpression,
    createAddImport,
  } = require('./utils')(j);

  const root = j(file.source);

  let addImport = createAddImport(root);
  
  root
    .find(j.CallExpression)
    .paths()
    .filter(path =>
      path.scope &&
      path.scope.isGlobal &&
      getAngularModuleExpression(path) === path &&
      path.node.arguments.length === 2
    )
    .forEach(path => {
      const moduleExpr = path.node;
      moduleExpr.arguments[1].elements = 
        moduleExpr.arguments[1].elements
          .filter(node => toBeRemoved.indexOf(node.value) === -1)
          .map(node => {
            let [module, identifier] = modules[node.value];
            if (!identifier) {
              identifier = createModuleIdentifier(node.value);
            }
            addImport(
              statement([
                `import ${identifier} from '${module}';\n`
              ])
            );
            const newNode = j.identifier(identifier);
            newNode.comments = node.comments || [];
            return newNode;
          })
    });
  
    let newSrc = root.toSource({ quote: 'single' });

    // remove extra newlines between imports
    // https://github.com/benjamn/recast/issues/371
    newSrc = newSrc.replace(/(import.*(?:\r?\n))(?:\r?\n)+(import)/g, '$1$2');

    return newSrc;
}
