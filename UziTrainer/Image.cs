using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Drawing = System.Drawing;
using Rectangle = System.Drawing.Rectangle;
namespace UziTrainer
{
    class Image
    {
        private Window window;

        public Image(Window window)
        {
            this.window = window;
        }

        public bool ImageExists(String imagePath, Rectangle area)
        {
            var source = window.CaptureBitmap(area);
            var search = new Drawing.Bitmap(imagePath);
            var point = FindPoint(source, search);
            return point.X != -1;
        }

        public bool ImageExists(String imagePath)
        {
            var source = window.CaptureBitmap();
            var search = new Drawing.Bitmap(imagePath);
            var point = FindPoint(source, search);
            Console.WriteLine(point);
            return point.X != -1;
        }

        private Drawing.Point FindPoint(Drawing.Bitmap sourceBitmap, Drawing.Bitmap serchingBitmap)
        {
            #region Arguments check

            if (sourceBitmap == null || serchingBitmap == null)
                throw new ArgumentNullException();
            if (sourceBitmap.PixelFormat != serchingBitmap.PixelFormat)
                throw new ArgumentException("Pixel formats arn't equal");

            if (sourceBitmap.Width < serchingBitmap.Width || sourceBitmap.Height < serchingBitmap.Height)
                throw new ArgumentException("Size of serchingBitmap bigger then sourceBitmap");

            #endregion

            var pixelFormatSize = System.Drawing.Image.GetPixelFormatSize(sourceBitmap.PixelFormat) / 8;


            // Copy sourceBitmap to byte array
            var sourceBitmapData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                ImageLockMode.ReadOnly, sourceBitmap.PixelFormat);
            var sourceBitmapBytesLength = sourceBitmapData.Stride * sourceBitmap.Height;
            var sourceBytes = new byte[sourceBitmapBytesLength];
            Marshal.Copy(sourceBitmapData.Scan0, sourceBytes, 0, sourceBitmapBytesLength);
            sourceBitmap.UnlockBits(sourceBitmapData);

            // Copy serchingBitmap to byte array
            var serchingBitmapData =
                serchingBitmap.LockBits(new Rectangle(0, 0, serchingBitmap.Width, serchingBitmap.Height),
                    ImageLockMode.ReadOnly, serchingBitmap.PixelFormat);
            var serchingBitmapBytesLength = serchingBitmapData.Stride * serchingBitmap.Height;
            var serchingBytes = new byte[serchingBitmapBytesLength];
            Marshal.Copy(serchingBitmapData.Scan0, serchingBytes, 0, serchingBitmapBytesLength);
            serchingBitmap.UnlockBits(serchingBitmapData);

            // Serching entries
            // minimazing serching zone
            // sourceBitmap.Height - serchingBitmap.Height + 1
            for (var mainY = 0; mainY < sourceBitmap.Height - serchingBitmap.Height + 1; mainY++)
            {
                var sourceY = mainY * sourceBitmapData.Stride;

                for (var mainX = 0; mainX < sourceBitmap.Width - serchingBitmap.Width + 1; mainX++)
                {// mainY & mainX - pixel coordinates of sourceBitmap
                 // sourceY + sourceX = pointer in array sourceBitmap bytes
                    var sourceX = mainX * pixelFormatSize;

                    var isEqual = true;
                    for (var c = 0; c < pixelFormatSize; c++)
                    {// through the bytes in pixel
                        if (sourceBytes[sourceX + sourceY + c] == serchingBytes[c])
                            continue;
                        isEqual = false;
                        break;
                    }

                    if (!isEqual) continue;

                    var isStop = false;

                    // find fist equalation and now we go deeper) 
                    for (var secY = 0; secY < serchingBitmap.Height; secY++)
                    {
                        var serchY = secY * serchingBitmapData.Stride;

                        var sourceSecY = (mainY + secY) * sourceBitmapData.Stride;

                        for (var secX = 0; secX < serchingBitmap.Width; secX++)
                        {// secX & secY - coordinates of serchingBitmap
                         // serchX + serchY = pointer in array serchingBitmap bytes

                            var serchX = secX * pixelFormatSize;

                            var sourceSecX = (mainX + secX) * pixelFormatSize;

                            for (var c = 0; c < pixelFormatSize; c++)
                            {// through the bytes in pixel
                                if (sourceBytes[sourceSecX + sourceSecY + c] == serchingBytes[serchX + serchY + c]) continue;

                                // not equal - abort iteration
                                isStop = true;
                                break;
                            }

                            if (isStop) break;
                        }

                        if (isStop) break;
                    }

                    if (!isStop)
                    {// serching bitmap is founded!!
                        return new Drawing.Point(mainX, mainY);
                    }
                }
            }
            return new Drawing.Point(-1,-1);
        }

    }
}
