using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UploadImageApp.Models;
using Xamarin.Forms;

namespace UploadImageApp.ViewModels
{
    class UploadPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        //Property ImagePath pro zobrazeni porizene/vybrane fotky
        public string ImagePath { get { return BlobModel.ImagePath; } }

        //Property Annotations pro vstup od uzivatelew
        public string Annotations
        {
            get
            {
                return BlobModel.Annotations;
            }
            set
            {
                if(BlobModel.Annotations != value)
                {
                    BlobModel.Annotations = value/*.Replace(" ", ",")*/;
                    OnPropertyChanged("Annotations");
                }
            }
        }
        public Command UploadCommand { get; }
        public UploadPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            UploadCommand = new Command(async () => await Upload());
        }

        async Task Upload()
        { 
            //Odeslani Image
            await AzureStorageModel.UploadFileAsync(ContainerType.Image, new MemoryStream(PrepareImageData()));

            //Odeslani Annotations
            await AzureStorageModel.UploadFileAsync(ContainerType.Text, new MemoryStream(PrepareAnnotationsData()));

            //Navigace zpet na hlavni stranku a informace uzivateli, ze se usepesne podarilo uploadnout Image/Annotations

            await Navigation.PopToRootAsync();
            await App.Current.MainPage.DisplayAlert("Succes", "Image/Annotations succesfully uploaded into Azure Storage", "OK");
        }

        //Priprava Annotations dat na odeslani
        public byte[] PrepareAnnotationsData()
        {
            //Konverze na comma-seperated format
            string annotationsReplaced = Annotations.Replace(" ", ",");

            //Konverze na byteArray, protoze Azure vyzaduje byteArray k odeslani
            var byteData = Encoding.UTF8.GetBytes(annotationsReplaced);
            return byteData;
        }

        //Priprava Image dat na odeslani
        public byte[] PrepareImageData()
        {
            //Konverze na byte array format
            var byteData = File.ReadAllBytes(BlobModel.ImagePath);
            return byteData;
        }

        #region IPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        }

        #endregion

    }
}
