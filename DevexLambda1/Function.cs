using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using DevExpress.XtraReports.UI;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DevexLambda1
{
    public class Function
    {
        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {

        }


        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            foreach(var message in evnt.Records)
            {
                await ProcessMessageAsync(message, context);
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
        {
            context.Logger.LogLine($"Processed message {message.Body}");

            XtraReport report = new XtraReport1();
            Stream stream = new MemoryStream();
            await report.ExportToPdfAsync(stream);

            // TODO: Do interesting work based on the new message
            await Task.CompletedTask;
        }

        //private static async Task UploadFileAsync()
        //{
        //    try
        //    {
        //        var fileTransferUtility =
        //            new TransferUtility(s3Client);

        //        // Option 1. Upload a file. The file name is used as the object key name.
        //        await fileTransferUtility.UploadAsync(filePath, bucketName);
        //        Console.WriteLine("Upload 1 completed");

        //        // Option 2. Specify object key name explicitly.
        //        await fileTransferUtility.UploadAsync(filePath, bucketName, keyName);
        //        Console.WriteLine("Upload 2 completed");

        //        // Option 3. Upload data from a type of System.IO.Stream.
        //        using (var fileToUpload =
        //            new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //        {
        //            await fileTransferUtility.UploadAsync(fileToUpload,
        //                                       bucketName, keyName);
        //        }
        //        Console.WriteLine("Upload 3 completed");

        //        // Option 4. Specify advanced settings.
        //        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        //        {
        //            BucketName = bucketName,
        //            FilePath = filePath,
        //            StorageClass = S3StorageClass.StandardInfrequentAccess,
        //            PartSize = 6291456, // 6 MB.
        //            Key = keyName,
        //            CannedACL = S3CannedACL.PublicRead
        //        };
        //        fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
        //        fileTransferUtilityRequest.Metadata.Add("param2", "Value2");

        //        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        //        Console.WriteLine("Upload 4 completed");
        //    }
        //    catch (AmazonS3Exception e)
        //    {
        //        Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
        //    }

        //}
    }
}
