export const ContractReportCertCorrectionFactory = [
  '$resource',
  function($resource) {
    return $resource(
      'api/contractReportCertCorrections/:id',
      {},
      {
        newContractReportCertCorrection: {
          method: 'GET',
          url: 'api/contractReportCertCorrections/new'
        },
        getInfo: {
          method: 'GET',
          url: 'api/contractReportCertCorrections/:id/info'
        },
        getBasicData: {
          method: 'GET',
          url: 'api/contractReportCertCorrections/:id/data'
        },
        enter: {
          method: 'POST',
          url: 'api/contractReportCertCorrections/:id/enter'
        },
        canEnter: {
          method: 'POST',
          url: 'api/contractReportCertCorrections/:id/canEnter'
        },
        setToDraft: {
          method: 'POST',
          url: 'api/contractReportCertCorrections/:id/setToDraft'
        },
        canSetToDraft: {
          method: 'POST',
          url: 'api/contractReportCertCorrections/:id/canSetToDraft'
        },
        setToRemoved: {
          method: 'POST',
          url: 'api/contractReportCertCorrections/:id/setToRemoved'
        },
        getCertReportCertCorrectionsContractReportCertCorrection: {
          method: 'GET',
          url: 'api/certReports/:id/certCorrections/:ind/certCorrection'
        }
      }
    );
  }
];
