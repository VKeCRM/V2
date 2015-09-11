using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace VKeCRM.Common.IO.Compression
{
    public static class CompressionExtension
    {
        private static CompressionAlgorithm DefaultAlgorithm = CompressionAlgorithm.DeflateStream;

        #region Public Method
            public static String Compress(this String data)
            {
                return Compress(data, Encoding.UTF8, DefaultAlgorithm);
            }

            public static String Compress(this String data, CompressionAlgorithm algorithm)
            {
                return Compress(data, Encoding.UTF8, algorithm);
            }

            public static String Compress(this String data, Encoding encoder)
            {
                return Compress(data, encoder, DefaultAlgorithm);
            }

            public static String Compress(this String data, Encoding encoder, CompressionAlgorithm algorithm)
            {
                Byte[] dataBytes = data.ToByteArray(encoder);
                return Convert.ToBase64String(Compress(dataBytes, algorithm));
            }

            public static Byte[] Compress(this Byte[] data)
            {
                return Compress(data, DefaultAlgorithm);
            }

            public static Byte[] Compress(this Byte[] data, CompressionAlgorithm algorithm)
            {
                //--- Define Location To Store Compressed Data ---//
                MemoryStream MS = new MemoryStream();

                //--- Create Compression Object ---//
                Stream zipper;
                if (algorithm == CompressionAlgorithm.GZipStream)
                    zipper = new GZipStream(MS, CompressionMode.Compress);
                else
                    zipper = new DeflateStream(MS, CompressionMode.Compress);

                //--- Compress ---//
                zipper.Write(data, 0, data.Length);
                zipper.Flush();
                zipper.Close();
                zipper.Dispose();

                //--- Get Compressed Data ---//
                Byte[] compressedData = MS.ToArray();
                MS.Close();
                MS.Dispose();

                //--- Return Compressed Data ---//
                return compressedData;
            }

            public static String Decompress(this String data)
            {
                return Decompress(data, Encoding.UTF8, DefaultAlgorithm);
            }

            public static String Decompress(this String data, CompressionAlgorithm algorithm)
            {
                return Decompress(data, Encoding.UTF8, algorithm);
            }

            public static String Decompress(this String data, Encoding encoder, CompressionAlgorithm algorithm)
            {
                Byte[] dataBytes = Convert.FromBase64String(data);
                return encoder.GetString(Decompress(dataBytes, algorithm));
            }

            public static Byte[] Decompress(this Byte[] data)
            {
                return Decompress(data, DefaultAlgorithm);
            }

            public static Byte[] Decompress(this Byte[] data, CompressionAlgorithm algorithm)
            {
                if (data == null || data.Length <= 0)
                    return new Byte[0];

                //--- Define Location To Store Compressed n Decompressed Data ---//
                MemoryStream cprMS = new MemoryStream(data);
                MemoryStream dcprMS = new MemoryStream();
                Int32 size = 4096;
                cprMS.Position = 0;

                //--- Create Compression Object ---//
                Stream zipper;
                if (algorithm == CompressionAlgorithm.GZipStream)
                    zipper = new GZipStream(cprMS, CompressionMode.Decompress);
                else
                    zipper = new DeflateStream(cprMS, CompressionMode.Decompress);

                //--- Decompress ---//
                Byte[] dcprData = new Byte[size];
                Int32 Count = 0;
                while (true)
                {
                    Count = zipper.Read(dcprData, 0, dcprData.Length);
                    if (Count > 0)
                        dcprMS.Write(dcprData, 0, Count);
                    else
                        break;
                }

                return dcprMS.ToArray();
            }
        #endregion

        #region Private Method

        #endregion
    }
}
