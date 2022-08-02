using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Eumis.Portal.Web.Helpers
{
    [Serializable]
    public enum AppStep
    {
        Prepare = 1,

        Display = 2,

        Upload = 3
    }

    [Serializable]
    public class AppContext
    {
        #region Constructor

        public AppContext(string code)
        {
            Code = code;
            WorkingDocument = new WorkingDocumentData();
            SessionKey = Guid.NewGuid().ToString().Split('-').First();
        }

        #endregion

        #region Current

        private static string SessionKey
        {
            get
            {
                return
                    System.Web.HttpContext.Current
                        .Request.RequestContext.RouteData
                            .Values[Constants.SessionKey].ToString();
            }
            set
            {
                System.Web.HttpContext.Current
                    .Request.RequestContext.RouteData
                        .Values[Constants.SessionKey] = value;
                                    
            }
        }

        public static AppContext Current
        {
            get
            {
                return (AppContext)System.Web.HttpContext.Current.Session[DocumentContextSessionKey + SessionKey];
            }
            set
            {
                System.Web.HttpContext.Current.Session[DocumentContextSessionKey + SessionKey] = value;
            }
        }

        #endregion

        #region Properties

        public List<AppStep> ValidatedSteps = new List<AppStep>();

        public string Code { get; private set; }

        public object Document
        {
            get { return _document; }
            set
            {
                if (value is Eumis.Documents.Interfaces.IDocumentNomenclatures)
                {
                    this._nomenclatures = ((Eumis.Documents.Interfaces.IDocumentNomenclatures)value).Nomenclatures;
                }

                _document = value;
            }
        }

        public string Xml
        {
            get
            {
                //var builder = new Autofac.ContainerBuilder();
                //
                //builder.RegisterModule(new EumisComponentsModule());
                //
                //using (var scope = builder.Build().BeginLifetimeScope())
                //{
                //    IDocumentSerializer documentSerializer = scope.Resolve<IDocumentSerializer>();
                //
                //    _xml = documentSerializer.XmlSerializeObjectToString(this.Document);
                //}

                return _xml;
            }
            set
            {
                _xml = value;
                this._xml = Regex.Replace(this._xml, "[\\x00-\\x08\\x0B\\x0C\\x0E-\\x1F\\x26]", string.Empty);
            }
        }

        public WorkingDocumentData WorkingDocument { get; set; }

        public DateTime? LastSaveDate { get; set; }

        #endregion

        #region Methods

        public void Clear()
        {
            System.Web.HttpContext.Current.Session.Remove(DocumentContextSessionKey);
        }

        public bool IsNomenclatureLoaded(Eumis.Documents.Mappers.NomenclatureType type)
        {
            return _nomenclatures.ContainsKey(type);
        }

        public List<Eumis.Documents.Mappers.Nomenclature> Nomenclature(Eumis.Documents.Mappers.NomenclatureType type)
        {
            return _nomenclatures[type];
        }

        //public List<SelectListItem> GetSelectListItems(Eumis.Documents.Mappers.NomenclatureType type)
        //{
        //    var result = new List<SelectListItem>();
        //
        //    if (_nomenclatures != null)
        //    {
        //        var values = _nomenclatures[type];
        //        if (values != null)
        //        {
        //            return values.Select(e => new SelectListItem() { Value = e.Value, Text = e.Name }).ToList();
        //        }
        //    }
        //
        //    return result;
        //}

        #endregion

        #region Private

        private string _xml;
        private object _document;
        private static readonly string DocumentContextSessionKey = "DocumentContextSessionKey";
        private Dictionary<Eumis.Documents.Mappers.NomenclatureType, List<Eumis.Documents.Mappers.Nomenclature>> _nomenclatures { get; set; }

        #endregion
    }

    [Serializable]
    public class WorkingDocumentData
    {
        public Guid gid { get; set; }

        public Guid parentGid { get; set; }

        public string token { get; set; }
        //public string hash { get; set; }

        public byte[] version { get; set; }

        public Guid packageGid { get; set; }
    }
}