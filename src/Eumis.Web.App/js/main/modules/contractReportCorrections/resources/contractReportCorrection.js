export const ContractReportCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportCorrections/:id',
      {},
      {
        newContractReportCorrection: {
          method: 'GET',
          url: 'api/contractReportCorrections/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportCorrections/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/contractReportCorrections/:id/data'
        },
        enter: {
          method: 'POST',
          url: 'api/contractReportCorrections/:id/enter'
        },
        canEnter: {
          method: 'POST',
          url: 'api/contractReportCorrections/:id/canEnter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/contractReportCorrections/:id/setToDraft'
        },
        canSetToDraft: {
          method: 'POST',
          url: 'api/contractReportCorrections/:id/canSetToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/contractReportCorrections/:id/setToRemoved'
        },
        getCertReportCorrectionsContractReportCorrection: {
          method: 'GET',
          url: 'api/certReports/:id/corrections/:ind/correction'
        },
        getAnnualAccountReportCorrectionsContractReportCorrection: {
          method: 'GET',
          url: 'api/annualAccountReports/:id/corrections/:ind/correction'
        },
        certUpdate: {
          method: 'PUT',
          url: 'api/certReports/:id/corrections/:ind/certUpdate'
        },
        changeCertStatusToEnded: {
          method: 'POST',
          url: 'api/certReports/:id/corrections/:ind/changeCertStatusToEnded'
        },
        changeCertStatusToDraft: {
          method: 'POST',
          url: 'api/certReports/:id/corrections/:ind/changeCertStatusToDraft'
        },
        canChangeCertStatusToEnded: {
          method: 'POST',
          url: 'api/certReports/:id/corrections/:ind/canChangeCertStatusToEnded'
        }
      }
    );
  }
];
