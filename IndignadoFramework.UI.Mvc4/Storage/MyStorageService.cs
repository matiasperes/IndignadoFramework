using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Configuration;

namespace IndignadoFramework.UI.Mvc4
{
    public class MyStorageService
    {
        public CloudBlobContainer GetStorageContainer(string nameContainer)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["Storage"].ConnectionString);
            CloudBlobClient client = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference(nameContainer);

            if (container.CreateIfNotExist() )
            {
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return container;
        }


        public bool DeleteContainer(string nameContainer) 
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["Storage"].ConnectionString);
                CloudBlobClient client = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = client.GetContainerReference(nameContainer);

                if (container.CreateIfNotExist())
                {
                    return false;
                }
                container.Delete();
                return true;
            }
            catch (StorageClientException ex)
            {
                //if ((int)ex.StatusCode == 409)
                //{
                //    return false;
                //}

                throw ex;
            }
                    
        }

        public void Upload(string nameContainer, HttpPostedFileBase fileBase)
        {
            try
            {
                GetStorageContainer(nameContainer).GetBlobReference(fileBase.FileName).UploadFromStream(fileBase.InputStream);
            }
            catch (StorageClientException ex)
            {
                //if ((int)ex.StatusCode == 404)
                //{
                //    return false;
                //}

                throw ex;
            }
        }


        public bool DeleteBlob(string nameContainer, string nameBlob)
        {
            try
            {
                GetStorageContainer(nameContainer).GetBlobReference(nameBlob).Delete();
                return true;
            }
            catch (StorageClientException ex)
            {
                //if ((int)ex.StatusCode == 404)
                //{
                //    return false;
                //}

                throw;
            }
        }


        public bool ListBlobs(string nameContainer, out List<CloudBlob> blobList)
        {
            blobList = new List<CloudBlob>();

            try
            {
                IEnumerable<IListBlobItem> blobs = GetStorageContainer(nameContainer).ListBlobs();
                if (blobs != null)
                {
                    foreach (CloudBlob blob in blobs)
                    {
                        blobList.Add(blob);
                    }
                }
                return true;
            }
            catch (StorageClientException ex)
            {
                //if ((int)ex.StatusCode == 404)
                //{
                //    return false;
                //}

                throw ex;
            }
        }






    }
}