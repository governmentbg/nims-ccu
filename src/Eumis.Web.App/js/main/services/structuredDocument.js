export const structuredDocumentService = [
  '$interpolate',
  'structuredDocumentConfig',
  'accessToken',
  function($interpolate, structuredDocumentConfig, accessToken) {
    this.getUrl = function(doc, action, xmlGid) {
      if (
        !structuredDocumentConfig[doc] ||
        !structuredDocumentConfig[doc][action] ||
        !structuredDocumentConfig[doc][action].url
      ) {
        throw new Error('Missing doc/action/url from structuredDocumentConfig');
      }
      return (
        $interpolate(structuredDocumentConfig[doc][action].url)({
          xmlGid: xmlGid
        }) +
        '&access_token=' +
        accessToken.get()
      );
    };

    this.getParentChildUrl = function(doc, action, parentGid, childGid) {
      if (
        !structuredDocumentConfig[doc] ||
        !structuredDocumentConfig[doc][action] ||
        !structuredDocumentConfig[doc][action].url
      ) {
        throw new Error('Missing doc/action/url from structuredDocumentConfig');
      }
      return (
        $interpolate(structuredDocumentConfig[doc][action].url)({
          parentGid: parentGid
        }) +
        '&childGid=' +
        childGid +
        '&access_token=' +
        accessToken.get()
      );
    };

    this.getPortalTitle = function(doc, action) {
      return structuredDocumentConfig[doc][action].title;
    };
  }
];

export const structuredDocumentConfigConstant = {
  sampleProject: {
    view: { url: window.eumisConfiguration.portalApplicationViewUrl }
  },
  procedureEvalTable: {
    view: {
      url: window.eumisConfiguration.portalEvalTableViewUrl,
      title: 'common_documentDirective_portalEvalTableView'
    },
    edit: {
      url: window.eumisConfiguration.portalEvalTableEditUrl,
      title: 'common_documentDirective_portalEvalTableEdit'
    }
  },
  evalSessionSheet: {
    view: {
      url: window.eumisConfiguration.portalEvalSessionSheetViewUrl,
      title: 'common_documentDirective_portalEvalSessionSheetView'
    },
    edit: {
      url: window.eumisConfiguration.portalEvalSessionSheetEditUrl,
      title: 'common_documentDirective_portalEvalSessionSheetEdit'
    }
  },
  evalSessionStandpoint: {
    view: {
      url: window.eumisConfiguration.portalEvalSessionStandpointViewUrl,
      title: 'common_documentDirective_portalEvalSessionStandpointView'
    },
    edit: {
      url: window.eumisConfiguration.portalEvalSessionStandpointEditUrl,
      title: 'common_documentDirective_portalEvalSessionStandpointEdit'
    }
  },
  projectVersion: {
    view: {
      url: window.eumisConfiguration.portalProjectViewUrl,
      title: 'common_documentDirective_portalProjectView'
    },
    edit: {
      url: window.eumisConfiguration.portalProjectEditUrl,
      title: 'common_documentDirective_portalProjectEdit'
    }
  },
  projectCommunication: {
    view: {
      url: window.eumisConfiguration.portalProjectCommunicationViewUrl,
      title: 'common_documentDirective_portalProjectCommunicationView'
    }
  },
  projectCommunicationQuestion: {
    view: {
      url: window.eumisConfiguration.portalProjectCommunicationViewUrl + '&type=message',
      title: 'common_documentDirective_portalProjectQuestionView'
    },
    edit: {
      url: window.eumisConfiguration.portalProjectCommunicationEditUrl + '&type=message',
      title: 'common_documentDirective_portalProjectQuestionEdit'
    }
  },
  projectCommunicationAnswer: {
    view: {
      url: window.eumisConfiguration.portalProjectCommunicationAnswerViewUrl + '&type=reply',
      title: 'common_documentDirective_portalProjectAnswerView'
    }
  },
  projectManagingAuthorityCommunication: {
    view: {
      url: window.eumisConfiguration.portalProjectManagingAuthorityCommunicationViewUrl,
      title: 'common_documentDirective_portalProjectManagingAuthorityCommunicationView'
    }
  },
  projectManagingAuthorityCommunicationQuestion: {
    view: {
      url: window.eumisConfiguration.portalProjectManagingAuthorityCommunicationViewUrl,
      title: 'common_documentDirective_portalProjectManagingAuthorityCommunicationView'
    },
    edit: {
      url: window.eumisConfiguration.portalProjectManagingAuthorityCommunicationEditUrl,
      title: 'common_documentDirective_portalProjectManagingAuthorityCommunicationEdit'
    }
  },
  projectManagingAuthorityCommunicationAnswer: {
    view: {
      url: window.eumisConfiguration.portalProjectManagingAuthorityCommunicationAnswerViewUrl,
      title: 'common_documentDirective_portalProjectAnswerView'
    },
    edit: {
      url: window.eumisConfiguration.portalProjectManagingAuthorityCommunicationAnswerEditUrl,
      title: 'common_documentDirective_portalProjectAnswerEdit'
    }
  },
  contractVersion: {
    view: {
      url: window.eumisConfiguration.portalContractViewUrl,
      title: 'common_documentDirective_portalContractView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractEditUrl,
      title: 'common_documentDirective_portalContractEdit'
    },
    editPartial: {
      url: window.eumisConfiguration.portalContractEditPartialUrl,
      title: 'common_documentDirective_portalContractEditPartial'
    }
  },
  contractOffer: {
    view: {
      url: window.eumisConfiguration.portalContractOfferViewUrl,
      title: 'common_documentDirective_portalContractOfferView'
    }
  },
  contractProcurement: {
    view: {
      url: window.eumisConfiguration.portalContractProcurementViewUrl,
      title: 'common_documentDirective_portalContractProcurementView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractProcurementEditUrl,
      title: 'common_documentDirective_portalContractProcurementEdit'
    }
  },
  contractCommunication: {
    view: {
      url: window.eumisConfiguration.portalContractCommunicationViewUrl,
      title: 'common_documentDirective_portalContractCommunicationView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractCommunicationEditUrl,
      title: 'common_documentDirective_portalContractCommunicationEdit'
    }
  },
  contractSpendingPlan: {
    view: {
      url: window.eumisConfiguration.portalContractSpendingPlanViewUrl,
      title: 'common_documentDirective_portalContractSpendingPlanView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractSpendingPlanEditUrl,
      title: 'common_documentDirective_portalContractSpendingPlanEdit'
    }
  },
  contractReportTechnical: {
    view: {
      url: window.eumisConfiguration.portalContractReportTechnicalViewUrl,
      title: 'common_documentDirective_portalContractReportTechnicalView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractReportTechnicalEditUrl,
      title: 'common_documentDirective_portalContractReportTechnicalEdit'
    }
  },
  contractReportPayment: {
    view: {
      url: window.eumisConfiguration.portalContractReportPaymentViewUrl,
      title: 'common_documentDirective_portalContractReportPaymentView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractReportPaymentEditUrl,
      title: 'common_documentDirective_portalContractReportPaymentEdit'
    }
  },
  contractReportFinancial: {
    view: {
      url: window.eumisConfiguration.portalContractReportFinancialViewUrl,
      title: 'common_documentDirective_portalContractReportFinancialView'
    },
    edit: {
      url: window.eumisConfiguration.portalContractReportFinancialEditUrl,
      title: 'common_documentDirective_portalContractReportFinancialEdit'
    }
  },
  contractReportMicroType1: {
    view: {
      url: window.eumisConfiguration.portalContractReportMicroType1ViewUrl,
      title: 'common_documentDirective_portalContractReportMicroView'
    }
  },
  contractReportMicroType2: {
    view: {
      url: window.eumisConfiguration.portalContractReportMicroType2ViewUrl,
      title: 'common_documentDirective_portalContractReportMicroView'
    }
  },
  contractReportMicroType3: {
    view: {
      url: window.eumisConfiguration.portalContractReportMicroType3ViewUrl,
      title: 'common_documentDirective_portalContractReportMicroView'
    }
  },
  contractReportMicroType4: {
    view: {
      url: window.eumisConfiguration.portalContractReportMicroType4ViewUrl,
      title: 'common_documentDirective_portalContractReportMicroView'
    }
  }
};
