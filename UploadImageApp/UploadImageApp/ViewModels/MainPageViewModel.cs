using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UploadImageApp.Models;
using Xamarin.Forms;


namespace UploadImageApp.ViewModels
{
    class MainPageViewModel { 
        public INavigation Navigation { get; set; }
        public MainPageViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            CrossMedia.Current.Initialize();

            LoadTakenPhotoCommand = new Command(async () => await LoadTakenPhoto());
            LoadPickedPhotoCommand = new Command(async () => await LoadPickedPhoto());
        }

        public Command LoadTakenPhotoCommand { get; }
        public Command LoadPickedPhotoCommand { get; }

        /*
         * Ulohy pro nacteni porizene/vybrane fotky
         * Pouzivam zde Media Plugin
         * Pro dokumentaci odkaz zde: https://github.com/jamesmontemagno/MediaPlugin
         * Navigace na stranku UploadPage je stejna u obou metod, byl by mozny
         * jen jeden command, pokud bych switchoval typ/name buttonu, na ktery jsem
         * kliknul a podle toho volal danou metodu CrossMedia.Current API
         */

        /// <summary>
        /// Uloha pro:
        ///     ziskani prave porizene fotky
        ///     navigaci na novou stranku UploadPage
        ///     nastaveni cesty k porizene fotce v BlobModelu
        /// </summary>
        /// <returns></returns>
        async Task LoadTakenPhoto()
        {
            //Kod pro zjisteni dostupnosti kamery a porizovani fotek
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            #region Permissions
            /*
             * Kod pro kontrolu a vyzadani permissions
             * Pouzivam zde Permissions Plugin
             * Pro dokumentaci odkaz zde: https://github.com/jamesmontemagno/PermissionsPlugin
             */

            //Kontrola camera&storage permissions
            //var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();
            //var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

            //Vyzadani camera&storage permissions
            //if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            //{
            //    cameraStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
            //    storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            //}

            //Pokud mam permissions, muzu porizovat fotku,
            //if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            //{
            //    var imageFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //    {
            //        Directory = "Sample",
            //        Name = "test.jpg"
            //    });

            //    //Kod pro navigaci na UploadPage a predani imagePath
            //    await Navigation.PushAsync(new UploadPage(imageFile.Path));
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Permissions Denied", "Unable to acces camera or storage", "OK");
            //}

            #endregion

            //Kod pro porizeni a ulozeni fotky
            var imageFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "ImageSamples",
                Name = "image.jpg",
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            });

            BlobModel.ImagePath = imageFile.Path;

            //Kod pro navigaci na UploadPage a predani imagePath
            //await Navigation.PushAsync(new UploadPage(/*imageFile.Path*/));

            //Kod pro navigaci na UploadPage
            await Navigation.PushAsync(new UploadPage(Navigation));

        }

        /// <summary>
        /// Uloha pro:
        ///     ziskani prave vybrane fotky
        ///     navigaci na novou stranku UploadPage
        ///     nastaveni cesty k vybrane fotce v BlobModelu
        /// </summary>
        /// <returns></returns>
        async Task LoadPickedPhoto()
        {
            //Kod pro zjisteni dostupnosti vyberu fotky
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Photos Not Supported", "Permission not granted to photos.", "OK");
                return;
            }

            #region Permissions
            /*
             * Kod pro kontrolu a vyzadani permissions
             * Pouzivam zde Permissions Plugin
             * Pro dokumentaci odkaz zde: https://github.com/jamesmontemagno/PermissionsPlugin
             */

            //Kontrola gallery&storage permissions
            //var galleryStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<PhotosPermission>();
            //var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

            ////Vyzadani gallery&storage permissions
            //if (galleryStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            //{
            //    galleryStatus = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
            //    storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            //}

            ////Pokud mam permissions, muzu vybrat fotku,
            //if (galleryStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            //{
            //    var imageFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //    {
            //        Directory = "Sample",
            //        Name = "test.jpg"
            //    });

            //    //Kod pro navigaci na UploadPage a predani imagePath
            //    await Navigation.PushAsync(new UploadPage(imageFile.Path));
            //}
            //else
            //{
            //    await App.Current.MainPage.DisplayAlert("Permissions Denied", "Unable to acces gallery or storage.", "OK");
            //}
            #endregion

            //Kod pro vybrani a ulozeni fotky
            var imageFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions{
                PhotoSize = PhotoSize.Medium,
                CompressionQuality = 92
            }
            );

            BlobModel.ImagePath = imageFile.Path;

            //Kod pro navigaci na UploadPage a predani imagePath
            //await Navigation.PushAsync(new UploadPage(/*imageFile.Path*/));

            //Kod pro navigaci na UploadPage
            await Navigation.PushAsync(new UploadPage(Navigation));
        }

    }
}
