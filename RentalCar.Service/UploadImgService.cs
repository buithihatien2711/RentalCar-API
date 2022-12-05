using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;

namespace RentalCar.Service
{
    public class UploadImgService : IUploadImgService
    {
        private static string apiKey = "AIzaSyD9hzJxxnyYkKT2Ju9GkXKav1QGlSScaM0";
        private static string Bucket = "pbl6-b8cad.appspot.com";
        private static string AuthEmail = "buithihatien2711@gmail.com";
        private static string AuthPassword = "BTHTien2711@";
        public async Task<string> UploadImage(string folder,string username,IFormFile model)
        {
            string projectPath = System.Environment.CurrentDirectory;
            string folderName = Path.Combine(projectPath, "Image\\");
            System.IO.Directory.CreateDirectory(folderName);

            using (FileStream fileStream= System.IO.File.Create(folderName + model.FileName))
            {
                model.CopyTo(fileStream);
                fileStream.Flush();
            }

            //upload firebase
            if (model.Length > 0)
            {
                FileStream ms = new FileStream(Path.Combine(folderName, model.FileName), FileMode.Open);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                var cancellation = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child("pbl6")
                    .Child(folder)
                    .Child(username)
                    .Child(DateTime.Now.ToString())
                    .PutAsync(ms, cancellation.Token);
                task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
                try
                {
                    var link = await task;
                    ms.Dispose();
                    if (System.IO.File.Exists(Path.Combine(folderName, model.FileName)))
                    {
                        System.IO.File.Delete(Path.Combine(folderName, model.FileName));
                    }
                    return link;
                }
                catch (Exception ex)
                {
                    throw new BadHttpRequestException($"Exception was thrown: {ex}");
                }
                

            }
            throw new BadHttpRequestException("Exception");
        }
        }
}