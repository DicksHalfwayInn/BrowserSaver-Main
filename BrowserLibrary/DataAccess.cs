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
    public class DataAccess
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
                FullFilePath = mFilePath + mFileName;
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
                FullFilePath = mFilePath + mFileName;
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

        public List<T> GetObjectsFromXMLFile<T>() where T : new()
        {
            List<T> result = new List<T>();
            if (FileExists())
            {
                //result = Deserialize<T>();
            }
            return result;
        }

        public void SaveObjectsToXMLFile<T>(List<T> currentCollection)
           where T : class, new()
        {
            var L = new T();
           List<T> result = new List<T>(); ;

            if (FileExists())

                
               
            {
                var previousCollection = GetObjectsFromXMLFile<T>();
                // Add each current item to the previous collection
                foreach (T item in currentCollection)
                    previousCollection.Add(item);

                // Set the object to our new collection
                result = previousCollection;
            }

            //Serialize<L>(result);
        }

        //public void SaveObjectsToXMLFile<T>(T obj)
        //    where T : new()
        //{
        //    if (FileExists() &&
        //        // Make sure the the current collection is an enumerable so we can enumerate it
        //        obj is IEnumerable currentCollection &&
        //        // Get the current object and make sure it's an ICollection
        //        GetObjectsFromXMLFile<T>() is ICollection previousCollection &&
        //        // Cast each item to an object to make the collection generic and then attempt to treat it as a generic ICollection so that we can add items to it
        //        previousCollection is IEnumerable<object> previousGenericEnumerable)
        //    {

        //        // Add each current item to the previous collection
        //        foreach (object item in currentCollection)
        //        {
        //            //obj = DataAccessStaticHelpers.Append<T>(obj, item);

        //            //previousGenericEnumerable = DataAccessStaticHelpers.Append(previousGenericEnumerable, item);
        //        }

        //        // Set the object to our new collection
        //        //obj = (T)previousGenericEnumerable;


        //    }

        //    Serialize<T>(obj);

        //    //Serialize<T>(previousGenericEnumerable);

        //}



        //public void SaveObjectsToXMLFile<T>(T obj) where T : new()
        //{


        //T t = new T();
        //if (FileExists())
        //{
        //    t = GetObjectsFromXMLFile<T>();
        //    Type type = t.GetType().GetGenericArguments()[0];
        //    if (t is IList)
        //    {
        //        var newlist = (IList)t;
        //        newlist.Add(obj);
        //    }
        //}

        //Serialize<T>(obj);
        //}

        private List<T> Deserialize<T>() where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<T>));
            var result = new List<T>();
            using (StreamReader file = new StreamReader(mFullFilePath))
            {
                result = (List<T>)xmlSerializer.Deserialize(file);
            }
            return result;
        }

        private void Serialize<T>(List<T> obj) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(List<T>));

            using (FileStream file = File.Create(mFullFilePath))
            {
                xmlSerializer.Serialize(file, obj);
            }
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
