using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject
{
    public static class Serializer
    {
        public static void SerializeAndWriteList<T>(string fileName, IEnumerable<T> objList, bool overwrite)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, !overwrite))
            {
                foreach (T obj in objList)
                {
                    file.WriteLine(JsonConvert.SerializeObject(obj));
                }
            }
        }

        public static void SerializeAndWrite<T>(string fileName, T obj, bool overwrite)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, !overwrite))
            {
                file.WriteLine(JsonConvert.SerializeObject(obj));
            }
        }

        public static IEnumerable<T> DeserializeList<T>(string fileName)
        {
            List<T> result = new List<T>();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    result.Add(JsonConvert.DeserializeObject<T>(reader.ReadLine()));
                }
            }
            return result;
        }
    }
}
