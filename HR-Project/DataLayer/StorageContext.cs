using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HR_Project_Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Project.DataLayer
{
    public static class StorageContext
    {
        private static string storageConnectionString;
        private static BlobServiceClient blobServiceClient;
        private static readonly string cvContainerName = "cv-blobs";
        private static BlobContainerClient cvContainerClient;
        private static readonly string attachmentContainerName = "attachment-blobs";
        private static BlobContainerClient attachmentContainerClient;

        public static void Setup(string connectionString)
        {
            storageConnectionString = connectionString;

            // Create a BlobServiceClient object which will be used to create a container client
            blobServiceClient = new BlobServiceClient(storageConnectionString);

            // Create the container and return a container client object
            cvContainerClient = blobServiceClient.GetBlobContainerClient(cvContainerName);
            attachmentContainerClient = blobServiceClient.GetBlobContainerClient(attachmentContainerName);
        }

        public static async Task<(byte[], string)> DownloadCVFileAsync(Guid cvId)
        {
            if (string.IsNullOrEmpty(storageConnectionString))
                throw new ApplicationException("Connection string to Blob Storage not provided");

            var blobName = cvId.ToString() + ".pdf";

            return await DownloadFileAsync(cvContainerClient, blobName);
        }

        public static async Task<(byte[], string)> DownloadAttachmentAsync(string attachmentPath)
        {
            if (string.IsNullOrEmpty(storageConnectionString))
                throw new ApplicationException("Connection string to Blob Storage not provided");

            return await DownloadFileAsync(attachmentContainerClient, attachmentPath);
        }

        public static void UploadCVFile(Guid cvId, IFormFile file, bool overrideFile = false)
        {
            if (string.IsNullOrEmpty(storageConnectionString))
                throw new ApplicationException("Connection string to Blob Storage not provided");

            if (Path.GetExtension(file.FileName) != ".pdf")
                throw new FormatException("CV-File must be in PDF format");

            var blobName = cvId.ToString() + ".pdf";
            BlobClient blobClient = cvContainerClient.GetBlobClient(blobName);

            using (Stream uploadFileStream = file.OpenReadStream())
            {
                blobClient.Upload(uploadFileStream, overrideFile);
                uploadFileStream.Close();
            }
        }

        public static void ReplaceCVFile(Guid cvId, IFormFile file)
        {
            UploadCVFile(cvId, file, true);
        }

        public static void UploadAttachmentGroup(List<Attachment> attachmentEntities, List<IFormFile> attachmentFiles)
        {
            if (string.IsNullOrEmpty(storageConnectionString))
                throw new ApplicationException("Connection string to Blob Storage not provided");

            if (attachmentEntities.Count() != attachmentFiles.Count())
                throw new ApplicationException("Number of uploaded files is different than in database");

            int i = 0;
            foreach(var attachmentEntity in attachmentEntities)
            {
                var blobName = attachmentEntity.IdAttachment.ToString() + attachmentEntity.Extension;
                BlobClient blobClient = attachmentContainerClient.GetBlobClient(blobName);

                using (Stream uploadFileStream = attachmentFiles[i].OpenReadStream())
                {
                    blobClient.Upload(uploadFileStream);
                    uploadFileStream.Close();
                }
                i++;
            }
        }

        private static async Task<(byte[], string)> DownloadFileAsync(BlobContainerClient containerClient, string blobName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            BlobDownloadInfo download = await blobClient.DownloadAsync();
            if (download.Content == Stream.Null)
                throw new ApplicationException("No file found on server");

            byte[] result;
            using (MemoryStream downloadFileStream = new MemoryStream())
            {
                await download.Content.CopyToAsync(downloadFileStream);
                result = downloadFileStream.ToArray();
                downloadFileStream.Close();
            }

            return (result, download.ContentType);
        }
    }
}
