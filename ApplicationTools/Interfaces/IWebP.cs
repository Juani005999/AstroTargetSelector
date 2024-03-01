using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ApplicationTools
{
    /// <summary>
    /// Interface du Wrapper libwebp.dll
    /// </summary>
    public interface IWebP : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawWebP"></param>
        /// <returns></returns>
        Bitmap Decode(byte[] rawWebP);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawWebP"></param>
        /// <param name="options"></param>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        Bitmap Decode(byte[] rawWebP, WebPDecoderOptions options, PixelFormat pixelFormat = PixelFormat.Undefined);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        byte[] EncodeLossless(Bitmap bmp);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        byte[] EncodeLossless(Bitmap bmp, int speed);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        byte[] EncodeLossy(Bitmap bmp, int quality = 75);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="quality"></param>
        /// <param name="speed"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        byte[] EncodeLossy(Bitmap bmp, int quality, int speed, bool info = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="quality"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        byte[] EncodeNearLossless(Bitmap bmp, int quality, int speed = 9);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawWebP"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="has_alpha"></param>
        /// <param name="has_animation"></param>
        /// <param name="format"></param>
        void GetInfo(byte[] rawWebP, out int width, out int height, out bool has_alpha, out bool has_animation, out string format);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="reference"></param>
        /// <param name="metric_type"></param>
        /// <returns></returns>
        float[] GetPictureDistortion(Bitmap source, Bitmap reference, int metric_type);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawWebP"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Bitmap GetThumbnailFast(byte[] rawWebP, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawWebP"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Bitmap GetThumbnailQuality(byte[] rawWebP, int width, int height);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetVersion();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathFileName"></param>
        /// <returns></returns>
        Bitmap Load(string pathFileName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="pathFileName"></param>
        /// <param name="quality"></param>
        void Save(Bitmap bmp, string pathFileName, int quality = 75);
    }
}