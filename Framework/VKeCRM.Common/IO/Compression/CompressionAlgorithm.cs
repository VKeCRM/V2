using System;
using System.ComponentModel;

namespace VKeCRM.Common.IO.Compression
{
    public enum CompressionAlgorithm
    {
        [Description("GZipStream")]
        GZipStream,

        [Description("GZipStream")]
        DeflateStream,
    }
}