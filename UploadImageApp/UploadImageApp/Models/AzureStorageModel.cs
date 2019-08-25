using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UploadImageApp.Models
{
    public static class AzureStorageModel
    {
        //String Connection k Azure Storage
        public static string storageConnection = ""; //Doplnit při testování

        //Metoda pro ziskani reference na container v Azure Storage
        //Dokumentace Azure Storage odkaz: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/azure-services/azure-storage
        static CloudBlobContainer GetContainer(ContainerType containerType)
        {
            var account = CloudStorageAccount.Parse(storageConnection);
            var client = account.CreateCloudBlobClient();
            return client.GetContainerReference(containerType.ToString().ToLower());
        }

        //Uloha pro odeslání souboru do Azure Storage
        public static async Task<string> UploadFileAsync(ContainerType containerType, Stream stream)
        {
            var container = GetContainer(containerType);
            await container.CreateIfNotExistsAsync();

            var name = "";
            switch (containerType)
            {
                case ContainerType.Image:
                    name = String.Format("image{0:ddMMyyyyHHmmss}.jpg", DateTime.Now);
                    break;
                case ContainerType.Text:
                    name = String.Format("imageAnnotations{0:ddMMyyyyHHmmss}.txt", DateTime.Now);
                    break;
            }

            var fileBlob = container.GetBlockBlobReference(name);
            await fileBlob.UploadFromStreamAsync(stream);

            return name;
        }
    }

    public enum ContainerType
    {
        Image,
        Text
    }
}
