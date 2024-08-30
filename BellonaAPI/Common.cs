using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace BellonaAPI
{
    public class Common
    {
        public static string ToXML(Object oObject)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlSerializer xmlSerializer = new XmlSerializer(oObject.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, oObject);
                xmlStream.Position = 0;
                xmlDoc.Load(xmlStream);
                return xmlDoc.InnerXml;
            }
        }

        public static T XMLToObject<T>(string XMLString) where T : class
        {
            XmlSerializer oXmlSerializer = new XmlSerializer(typeof(T));
            return (T) oXmlSerializer.Deserialize(new StringReader(XMLString));
        }

        public static T ProcessSQLOutputData<T>(DataRow row, TypeCode datatype, string fieldName)
        {
            try
            {
                return row.Field<T>(fieldName);
            }
            catch(Exception EX)
            {
                return default(T);
            }
            T result = default(T);
            switch (Type.GetTypeCode(datatype.GetType()))
            {
                case TypeCode.Int32:
                    result = row.Field<int?>(fieldName).HasValue ? default(T) : row.Field<T>(fieldName);
                    break;
                case TypeCode.Decimal:
                    result = row.Field<decimal?>(fieldName).HasValue ? default(T) : row.Field<T>(fieldName);
                    break;
                case TypeCode.Double:
                    result = row.Field<double?>(fieldName).HasValue ? default(T) : row.Field<T>(fieldName);
                    break;
                case TypeCode.Boolean:
                    result = (T)(object)Convert.ChangeType(false, typeof(T));
                    break;
                case TypeCode.DateTime:
                    result = (T)(object)Convert.ChangeType(DateTime.MinValue, typeof(T));
                    break;
                default:
                    result = (T)(object)Convert.ChangeType("", typeof(T));
                    break;
            }

            return result;
        }

    }
}