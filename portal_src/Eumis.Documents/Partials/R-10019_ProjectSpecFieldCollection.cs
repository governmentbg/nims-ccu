using Eumis.Documents.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace R_10019
{
    public partial class ProjectSpecFieldCollection
    {
        public static ProjectSpecFieldCollection Load(List<ContractSpecField> fields, ProjectSpecFieldCollection collection)
        {
            if (collection == null)
                collection = new ProjectSpecFieldCollection();

            if (fields != null && fields.Count > 0)
            {
                foreach (var field in fields)
                {
                    var collectionField = collection.FirstOrDefault(e => e.Id.Equals(field.gid));

                    if (collectionField != null)
                    {
                        collectionField.Title = field.title;
                        collectionField.TitleEN = field.titleAlt;
                        collectionField.Description = field.description;
                        collectionField.DescriptionEN = field.descriptionAlt;
                        collectionField.IsRequired = field.isRequired;
                        collectionField.IsDeactivated = !field.isActive;
                        collectionField.MaxLength = field.maxLength;
                    }
                    else if(field.isActive)
                    {
                        collection.Add(new R_10017.ProjectSpecField()
                            {
                                Description = field.description,
                                DescriptionEN = field.descriptionAlt,
                                Id = field.gid,
                                IsRequired = field.isRequired,
                                Title = field.title,
                                TitleEN = field.titleAlt,
                                MaxLength = field.maxLength
                            });
                    }
                }
            }

            return collection;
        }
    }
}
