using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace Entity.Util
{
    public class Util
    {
        public static byte[] BitmapToBytes(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, bitmap.RawFormat);
            byte[] bt = ms.ToArray();
            return bt;
        }

        public static Bitmap BtyesToBitmap(byte[] bt)
        {
            MemoryStream ms = new MemoryStream(bt);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }
    }
    public class ImageConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Bitmap);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var m = new MemoryStream(Convert.FromBase64String((string)reader.Value));
            return (Bitmap)Bitmap.FromStream(m);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Bitmap bmp = (Bitmap)value;
            MemoryStream m = new MemoryStream();
            bmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);
            writer.WriteValue(Convert.ToBase64String(m.ToArray()));
        }
    }

    public class ImageHelper
    {
        public ImageHelper()
        {
            string deletePart = string.Format("{0}Server{0}bin{0}Debug", Path.DirectorySeparatorChar);
            StorePath = Path.Combine(Directory.GetCurrentDirectory().Replace(deletePart, ""), "ImageData");
            
        }
        public ImageHelper(string storePath)
        {
            StorePath = storePath;
        }
        public string StorePath { set; get; }

        public void StoreImage(Bitmap bitmap, string username, DateTime dateTime)
        {
            if (!Directory.Exists(StorePath))
            {
                Directory.CreateDirectory(StorePath);
            }
            string filename = string.Format("{0}{1}.jpeg", username, dateTime.ToFileTime());
            string completePath = Path.Combine (StorePath, filename);
            Console.WriteLine("Store in {0}", completePath);
            bitmap.Save(completePath);
        }

        public Bitmap GetImage(string username, DateTime dateTime)
        {
            string filename = string.Format("{0}{1}.jpeg", username, dateTime.ToFileTime());
            string completePath = Path.Combine(StorePath, filename);
            Console.WriteLine("Get from {0}", completePath);
            Bitmap bitmap = new Bitmap(completePath);

            return bitmap;
        }

    }
}
