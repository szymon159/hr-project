using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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
        private static readonly string containerName = "blobs";
        private static BlobContainerClient containerClient;

        public static void Setup(string connectionString)
        {
            storageConnectionString = connectionString;

            // Create a BlobServiceClient object which will be used to create a container client
            blobServiceClient = new BlobServiceClient(storageConnectionString);

            // Create the container and return a container client object
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        //public static async Task<IFormFile> DownloadCVFileAsync(int cvId)
        //{
        //    if (string.IsNullOrEmpty(storageConnectionString))
        //        throw new ApplicationException("Connection string to Blob Storage not provided");


        //    var blobName = cvId.ToString() + ".pdf";
        //    BlobClient blobClient = containerClient.GetBlobClient(blobName);

        //    BlobDownloadInfo download = await blobClient.DownloadAsync();
        //    if (download.Content == Stream.Null)
        //        throw new ApplicationException("No file found on server");

        //    Stream stream = new Stream();
        //    await download.Content.CopyToAsync(stream);

        //    return new FormFile(download.Content, 0, download.ContentLength, "CV", blobName);
        //}

        public static void UploadCVFileAsync(int cvId, IFormFile file, bool overrideFile = false)
        {
            if (string.IsNullOrEmpty(storageConnectionString))
                throw new ApplicationException("Connection string to Blob Storage not provided");

            if (Path.GetExtension(file.FileName) != ".pdf")
                throw new FormatException("CV-File must be in PDF format");

            var blobName = cvId.ToString() + ".pdf";
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using (Stream uploadFileStream = file.OpenReadStream())
            {
                blobClient.Upload(uploadFileStream, true);
                uploadFileStream.Close();
            }
        }

        public static void ReplaceCVFile(int cvId, IFormFile file)
        {
            UploadCVFileAsync(cvId, file, true);
        }
    }
}
