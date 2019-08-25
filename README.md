# UploadPhotoAnnotationsApp

- Xamarin Forms application for uploading photos with corresponding annotations to Azure Storage
- This application was tested on **Android version 9.0 (Pie)**

### Application logic
  - This app is using plugins such as:
    - Xam.Plugin.Media (Taking and picking a photo)
    - WindowsAzure.Storage (Connecting to an Azure Storage)
    - Plugin.Permissions (Camera, storage and gallery permissions)
  - Links for documentation are in the comments
  - First, user either take or pick a photo from gallery (Permission prompt)
  - Taken/picked photo is shown to the user on next page
  - User then describe what is on the photo (Annotations)
  - Those annotations are converted into comma-seperated format
  - When user clicks on the Upload button, he will be connected to Azure Storage account
  and the photo with corresponding annotations will be uploaded into the storage
  - A unique ID (DateTime) is given to each photo/annotations for future purpose
    
 ### Application View
  - There are two content pages:
    - MainPage:
      - Buttons for navigation and command binding
    - UploadPage:
      - Taken or Picked photo
      - Entry field for annotations
      - Button for uploading the files
      
### Future insight
  - Deep Neural Networks works efficiently on very big data, cause they can learn much more.
  But for supervised learning, the data needs to be annotated for the network to learn.
  Because of that, I came up with this idea of manual photo annotation app. With this I can pretty much easily gather
  important data that I can feed the network with.
  - Next step is to do this photo annotation process automatically, which means that I need to build
  such neural network that can detect, recognize and segment objects in the scene.
  - Now here is the problem:
    - Mobiles are not that computationally efficient, so I will train the model on a remote server.
    After taking or picking the photo I will ask the server for annotations. I will display those annotations to the user and
    ask him, if the annotations are correct. If they are correct, I will just send a reply that it correctly recognized them.
    Otherwise, I will send a reply with corrected annotations and will let the model re-train itself.
  
