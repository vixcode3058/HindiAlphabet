using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HindiAlphabet
{
    public class Storage
    {
        // this method to write the file in storage
        internal static Task<bool> WriteStorageFile<T>(string data, string localFileName)
        {

            try
            {
                File.WriteAllText(GetLocalPath(localFileName), data);
                return Task.FromResult<bool>(true);
            }
            catch (Exception)
            {

                return Task.FromResult<bool>(false);
            }
        }


        // get the loacl path of storage
        public static string GetLocalPath(string localFileName)
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var localPath = Path.Combine(documentPath, localFileName);
            return localPath;
        }

        // to deserialize the xml file
        internal static Task<T> DeserializeXML<T>(string data)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                using (StringReader stringReader = new StringReader(data))
                {
                    T appData = (T)serializer.Deserialize(stringReader);

                    return Task.FromResult<T>(appData);
                }
            }
            catch (Exception x)
            {
                return default(Task<T>);
            }
        }

        //internal static Task<List<Letter>> ReadList<T>(string fileName)
        //{
        //    var xx = File.ReadAllText(GetLocalPath(fileName));


        //    return  Task.FromResult<List<Letter>>(JsonConvert.DeserializeObject<List<Letter>>(xx));
        //}

        internal static Task<string> ReadLocalStorageFile(string fileName)
        {
            try
            {
                var data = File.ReadAllText(GetLocalPath(fileName));
                return Task.FromResult<string>(data);
            }
            catch (Exception)
            {

                return Task.FromResult<string>("Error");
            }



        }


        // This Method is to Read letters from XML file

        internal static Task<List<Letter>> ReadStorageFile(string fileName)
        {
            Assembly assembly = typeof(HindiAlphabet.App).GetTypeInfo().Assembly;

            var resource = assembly.GetManifestResourceNames();
            var filePath = (from f in resource where f.Contains(fileName) select f).FirstOrDefault();

            Stream stream = assembly.GetManifestResourceStream(filePath);

            using (TextReader reader = new StreamReader(stream))
            {

                XmlSerializer serializer = new XmlSerializer(typeof(List<Letter>));
                return Task.FromResult<List<Letter>>((List<Letter>)serializer.Deserialize(reader));

            }

        }

        public static string DeserializeAndReadList(string fileName)
        {

            Assembly assembly = typeof(HindiAlphabet.App).GetTypeInfo().Assembly;

            var resource = assembly.GetManifestResourceNames();
            var filePath = (from f in resource where f.Contains(fileName) select f).FirstOrDefault();

            Stream stream = assembly.GetManifestResourceStream(filePath);
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        // serialize the list into stream

        internal static void SerializeAndWriteList<T>(T letters, string fileName)
        {

            File.WriteAllText(GetLocalPath(fileName), JsonConvert.SerializeObject(letters));


            //using (var stream = File.OpenWrite(fileName))
            //{
            //    // serializer.Serialize(stream, letters);

            //    XmlSerializer serializer = new XmlSerializer(typeof(List<Letter>));
            //    serializer.Serialize(stream, letters);

            //    StreamReader reader = new StreamReader(stream);
            //    File.WriteAllText(GetLocalPath(fileName), reader.ReadToEnd());
            //}
        }

        internal static Task<List<Letter>> ReadList(string fileName)
        {
            var xx = File.ReadAllText(GetLocalPath(fileName));

            
            return Task.FromResult<List<Letter>> ( JsonConvert.DeserializeObject<List<Letter>>(xx));



        }

        
    }


}
