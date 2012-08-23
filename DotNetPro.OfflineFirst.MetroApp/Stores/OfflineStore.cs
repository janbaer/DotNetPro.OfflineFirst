using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DotNetPro.OfflineFirst.MetroApp.Stores
{
    public class OfflineStore : IOfflineStore
    {
        private readonly StorageFolder _localFolder;

        public OfflineStore()
        {
            _localFolder = ApplicationData.Current.LocalFolder;
        }

        public async Task SaveAsync<T>(string key, T objectToSerialize, List<Type> knownTypes)
        {
            // Get the output stream for the SessionState file.
            StorageFile file = await _localFolder.CreateFileAsync(key + ".xml", CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await file.OpenAsync(FileAccessMode.ReadWrite);

            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                // Serialize the Session State.
                var serializer = new DataContractSerializer(objectToSerialize.GetType(), knownTypes);
                serializer.WriteObject(outStream.AsStreamForWrite(), objectToSerialize);
                await outStream.FlushAsync();
            }

            raStream.Dispose();
        }

        public async Task<T> LoadAsync<T>(string key, List<Type> knownTypes) where T : new()
        {
            try
            {
                StorageFile file = await _localFolder.GetFileAsync(key + ".xml");

                if (file == null)
                {
                    return default(T);
                }

                IInputStream inStream = await file.OpenSequentialReadAsync();
                var serializer = new DataContractSerializer(typeof(T), knownTypes);

                return (T)serializer.ReadObject(inStream.AsStreamForRead());
            }
            catch (Exception e)
            {
                return new T();
            }
        }  
    }
}