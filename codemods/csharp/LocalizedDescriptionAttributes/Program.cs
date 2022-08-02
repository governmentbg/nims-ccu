using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace LocalizedDescriptionAttributes
{
    class Program
    {
        private static readonly Regex filenameRegex = new Regex("^.*\\.cs$", RegexOptions.Compiled);

        static void Main(string[] args)
        {
            var projects = new List<(string projectDir, string resourceType, string resourceFile)>()
            {
                (projectDir: @"C:\Users\angel\Git\Eumis\src\Eumis.Domain", resourceType: "DomainEnumTexts", resourceFile: "DomainEnumTexts.resx"),
                (projectDir: @"C:\Users\angel\Git\Eumis\src\Eumis.Data", resourceType: "DataEnumTexts", resourceFile: "DataEnumTexts.resx"),
            };

            foreach ((string projectDir, string resourceType, string resourceFile) in projects)
            {
                Dictionary<string, string> texts = new Dictionary<string, string>();
                var transformer = new RecursiveDirectoryTransformer(projectDir, (f, c) => Transform(f, c, resourceType, texts));
                transformer.Process();

                WriteResourceFile(projectDir, resourceFile, texts);

                Console.WriteLine("Succeeded");
            }
        }

        static string Transform(string fileName, string contents, string resourceType, Dictionary<string, string> texts)
        {
            if (!filenameRegex.IsMatch(fileName))
            {
                return null; // skip
            }

            SyntaxTree tree = CSharpSyntaxTree.ParseText(contents);
            var root = (CompilationUnitSyntax)tree.GetRoot();

            var oldDescriptionNamespaceUsing = root.Usings
                .Where(u => u.Name.ToString() == "System.ComponentModel")
                .SingleOrDefault();

            if (oldDescriptionNamespaceUsing == null)
            {
                return null; // skip
            }

            var hasEumisCommonJSONUsing = root.Usings
                .Where(u => u.Name.ToString() == "Eumis.Common.Json")
                .Any();

            if (hasEumisCommonJSONUsing)
            {
                root = root.RemoveNode(oldDescriptionNamespaceUsing, SyntaxRemoveOptions.KeepNoTrivia);
            }
            else
            {
                var newDescriptionNamespaceUsing = oldDescriptionNamespaceUsing.WithName(SyntaxFactory.ParseName("Eumis.Common.Json"));
                root = root.ReplaceNode(oldDescriptionNamespaceUsing, newDescriptionNamespaceUsing);
            }

            var replaced = new Dictionary<SyntaxNode, SyntaxNode>();
            foreach (var ns in root.Members.OfType<NamespaceDeclarationSyntax>())
            {
                foreach (var e in ns.Members.OfType<EnumDeclarationSyntax>())
                {
                    foreach (var al in e.AttributeLists)
                    {
                        transformAttributeList(al, $"{e.Identifier.ValueText}");
                    }

                    foreach (var em in e.Members)
                    {
                        foreach (var al in em.AttributeLists)
                        {
                            transformAttributeList(al, $"{e.Identifier.ValueText}_{em.Identifier.ValueText}");
                        }
                    }
                }
            }

            if (!replaced.Any())
            {
                return null; // skip
            }

            root = root.ReplaceNodes(replaced.Keys, (origNode, newNode) => replaced[origNode]);

            return root.ToFullString();

            void transformAttributeList(AttributeListSyntax al, string descriptionKey)
            {
                // look for attributes like - [Description("Some text")]
                if (al.Attributes.Count() == 1
                    && al.Attributes.All(a =>
                        a.Name is IdentifierNameSyntax
                        && ((IdentifierNameSyntax)a.Name).Identifier.ValueText == "Description"
                        && a.ArgumentList.Arguments.Count() == 1
                        && a.ArgumentList.Arguments.All(arg => arg.Expression.IsKind(SyntaxKind.StringLiteralExpression))))
                {
                    var oldAttribute = al.Attributes[0];
                    var newArgumentsList = SyntaxFactory.ParseAttributeArgumentList($"(Description = nameof({resourceType}.{descriptionKey}), ResourceType = typeof({resourceType}))");
                    var newAttribute = oldAttribute
                        .WithArgumentList(newArgumentsList);

                    replaced.Add(oldAttribute, newAttribute);

                    var text = ((LiteralExpressionSyntax)oldAttribute.ArgumentList.Arguments[0].Expression).Token.ValueText;
                    texts.Add(descriptionKey, text);
                }
            }
        }

        static void WriteResourceFile(string projectDir, string resourceFile, Dictionary<string, string> texts)
        {
            try
            {
                var resourceFilePath = Path.Combine(projectDir, resourceFile);
                XDocument doc;
                using (var fs = new FileStream(resourceFilePath, FileMode.Open))
                {
                    doc = XDocument.Load(fs, LoadOptions.PreserveWhitespace);
                }

                foreach (var kvp in texts)
                {
                    doc.Root.Add(
                        new XElement(
                            "data",
                            new XElement("value", kvp.Value),
                            new XAttribute("name", kvp.Key),
                            new XAttribute(XNamespace.Xml + "space", "preserve")));
                }

                using (var fs = new FileStream(resourceFilePath, FileMode.Truncate))
                using (var xw = new XmlTextWriter(fs, Encoding.UTF8))
                {
                    doc.Save(xw);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while writing resource file.\n{e.Message}");
            }
        }
    }
}
