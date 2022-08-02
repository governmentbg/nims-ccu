using Eumis.Documents.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace R_10019
{
    public partial class ElectronicDeclarationCollection
    {
        internal static ElectronicDeclarationCollection Load(List<ContractProcedureDeclaration> declarations, ElectronicDeclarationCollection electronicDeclarationCollection)
        {
            if (electronicDeclarationCollection == null)
            {
                electronicDeclarationCollection = new ElectronicDeclarationCollection();
            }

            if (declarations != null && declarations.Any())
            {
                foreach (var declaration in declarations.OrderBy(d => d.orderNum))
                {
                    var electronicDeclaration = electronicDeclarationCollection
                        .Where(d => d.Gid == declaration.gid.ToString())
                        .FirstOrDefault();

                    if (electronicDeclaration != null)
                    {
                        electronicDeclaration.IsActive = declaration.isActive;

                        if (electronicDeclaration.FieldType == R_10098.FieldType.Nomenclature)
                        {
                            electronicDeclaration.Items = declaration.items.Where(i => i.isActive).OrderBy(i => i.orderNum).ToList();
                        }
                    }
                    else if (declaration.isActive)
                    {
                        var newElectronicDeclaration = R_10098.ElectronicDeclaration.Init(declaration);
                        electronicDeclarationCollection.Add(newElectronicDeclaration);
                    }
                }
            }

            electronicDeclarationCollection = OrderDeclarations(electronicDeclarationCollection);

            return electronicDeclarationCollection;
        }

        private static ElectronicDeclarationCollection OrderDeclarations(ElectronicDeclarationCollection electronicDeclarationCollection)
        {
            if (electronicDeclarationCollection == null)
            {
                electronicDeclarationCollection = new ElectronicDeclarationCollection();
            }

            var areOrderNumsValid = true;

            int parsedOrderNum;
            foreach (var declaration in electronicDeclarationCollection)
            {
                if (!int.TryParse(declaration.OrderNum, out parsedOrderNum))
                {
                    areOrderNumsValid = false;
                    break;
                }
            }

            if (areOrderNumsValid)
            {
                var orderedDeclarations = electronicDeclarationCollection.OrderBy(d => int.Parse(d.OrderNum)).ToList();

                electronicDeclarationCollection = new ElectronicDeclarationCollection();
                electronicDeclarationCollection.AddRange(orderedDeclarations);
            }

            return electronicDeclarationCollection;
        }
    }
}
