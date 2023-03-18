using Hangfire.Annotations;
using System.Buffers.Text;

namespace FreshersV2.Helpers
{
    public static class ImageHelper
    {
        public static string GetCompressedBase64Image(string base64)
        {
            try
            {
                Chilkat.Compression compress = new();

                Chilkat.BinData binDat = new Chilkat.BinData();
                // Load the base64 data into a BinData object.
                // This decodes the base64. The decoded bytes will be contained in the BinData.
                binDat.AppendEncoded(base64, "base64");

                // Compress the BinData.
                compress.CompressBd(binDat);

                // Get the compressed data in base64 format:
                string compressedBase64 = binDat.GetEncoded("base64");

                return compressedBase64;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }

        public static string GetDecompressedBase64Image(string base64)
        {
            try
            {
                Chilkat.Compression compress = new();

                Chilkat.BinData binDat = new Chilkat.BinData();
                // Load the base64 data into a BinData object.
                // This decodes the base64. The decoded bytes will be contained in the BinData.
                binDat.AppendEncoded(base64, "base64");

                // Compress the BinData.
                compress.DecompressBd(binDat);

                string decompressedBase64 = binDat.GetEncoded("base64");

                return decompressedBase64;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }
}
