using Newtonsoft.Json.Linq;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Eumis.Job.Host.Jobs.Email
{
    public static class RazorEngineRenderer
    {
        private static ITemplateService templateService = new TemplateService();
        private static HashSet<string> compiledTemplates = new HashSet<string>();

        public static string RenderTemplate(string templateFileName, JObject context)
        {
            if (!RazorEngineRenderer.compiledTemplates.Contains(templateFileName))
            {
                string templatePath = GetTemplatePath(templateFileName);
                string razorTemplate = File.ReadAllText(templatePath);
                RazorEngineRenderer.templateService.Compile(razorTemplate, typeof(JObject), templateFileName);
                RazorEngineRenderer.compiledTemplates.Add(templateFileName);
            }

            return RazorEngineRenderer.templateService.Run(templateFileName, context, null);
        }

        private static string GetTemplatePath(string templateName)
        {
            string assemblyPath = new System.Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            string binPath = System.IO.Path.GetDirectoryName(assemblyPath);
            string templateFullPath = string.Format(@"{0}\Jobs\Email\Templates\{1}", binPath, templateName);

            return templateFullPath;
        }
    }
}