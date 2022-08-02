using Microsoft.Owin;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Common.Owin
{

    public class RequestLoggingMiddleware : OwinMiddleware
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public RequestLoggingMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public async override Task Invoke(IOwinContext context)
        {
            if (Configuration.EnableRequestResponseLogging)
            {
                this.LogRequest(context);
                await this.LogResponse(context);
            }
            else 
            {
                await this.Next.Invoke(context);
            }
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            var textWriter = new StringWriter();
            var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(
                    readChunk,
                    0,
                    readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            }
            while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private void LogRequest(IOwinContext context)
        {
            logger.Info( $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString}");
        }

        private async Task LogResponse(IOwinContext context)
        {
            await this.Next.Invoke(context);

            logger.Info($"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} ");
            
        }
    }

}
