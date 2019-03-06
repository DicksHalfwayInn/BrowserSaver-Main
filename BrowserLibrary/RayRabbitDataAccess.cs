using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BrowserLibrary
{
    public class RayRabbitDataAccess
    {
        #region                Private Members

       

        private string mFileName;

        private string mFilePath;

        private string mFullFilePath;

        #endregion

        #region               Public Properties

       
        public string FileName
        {
            get
            {
                return mFileName;
            }
            set
            {
                mFileName = value;
                FullFilePath = mFilePath + "\\" + mFileName; ;
            }
        }

        public string FilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                mFilePath = value;
                FullFilePath = mFilePath + "\\" + mFileName;
            }
        }

        public string FullFilePath
        {
            get
            {
                return mFullFilePath;
            }
            set
            {
                mFullFilePath = value;
            }
        }

        #endregion

       
       

        public void SetFileNameToGenericName<T>() where T : new()
        {
            T t = new T();
            string name = t.GetType().Name;

            FileName = "ListOf" + name + "ToXML.xml";
        }

        public T GetObjectsFromXMLFile<T>() where T : new()
        {
            T result = new T();
            if (FileExists())
            {
                result = Deserialize<T>();
            }
            return result;
        }

        public void SaveObjectsToXMLFile<T, L>(T obj)
            where T : new()
        {
            if (FileExists() &&
                // Make sure the the current collection is an enumerable so we can enumerate it
                obj is IEnumerable currentCollection &&
                // Get the current object and make sure it's an ICollection
                GetObjectsFromXMLFile<T>() is ICollection previousCollection /*&&*/
               // Cast each item to an object to make the collection generic and then attempt to treat it as a generic ICollection so that we can add items to it
                /*previousCollection.Cast<object>() is ICollection<object> previousGenericCollection*/
                /* previousCollection.Cast<T>() is ICollection<L> previousGenericCollection*/)
            {

                Type typ = obj.GetType().GetGenericArguments()[0];





                ICollection<L> previousGenericCollection = (ICollection<L>)previousCollection;
                // Add each current item to the previous collection
                foreach (object item in currentCollection)
                {
                    L Litem = (L)item;
                    previousGenericCollection.Add(Litem);
                }

                // Set the object to our new collection
                obj = (T)previousCollection;
            }

            Serialize<T>(obj);
        }


        public T Deserialize<T>()
            where T : new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            var result = new T();
            using (StreamReader file = new StreamReader(FullFilePath))
            {
                result = (T)xmlSerializer.Deserialize(file);
            }
            return result;
        }

        public void Serialize<T>(T obj)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (FileStream file = File.Create(FullFilePath))
                xmlSerializer.Serialize(file, obj);
        }


        public void SetPathToSolutionPath()
        {
            DirectoryInfo pathDI = VisualStudioProvider.TryGetSolutionDirectoryInfo();
            FilePath = pathDI.FullName;
        }

        private bool FileExists()
        {
            if (File.Exists(mFullFilePath))
            {
                return true;
            }
            else return false;
        }

    }
}
