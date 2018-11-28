using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;

namespace DemoApplication.Models
{
    public class ImageUpload
    {
        public int Width { get; set; }

        public int Height { get; set; }
        public string filename(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);

            string finalFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string ImageName = "/Uploads/PImage/" + Path.GetFileName(finalFileName);

            return ImageName;
        }
        public string finalfileName = "";

        public ImageResult RenameUploadFile(HttpPostedFileBase file, Int32 counter = 0)
        {
            //var fileName = Path.GetFileName(file.FileName);

            //string finalFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

           string finalfileName = filename(file);
            return UploadFile(file, finalfileName);
        }
       
       public PImage fileDetails = new PImage();
        private ImageResult UploadFile(HttpPostedFileBase file, string fileName)
        {
            ImageResult imageResult = new ImageResult { Success = true, ErrorMessage = null };
            

            var path = finalfileName;
            string extension = Path.GetExtension(file.FileName);

            if (!ValidateExtension(extension))
            {
                imageResult.Success = false;
                imageResult.ErrorMessage = "Invalid Extension";
                return imageResult;
            }

            try
            {
                string filename3 = HttpContext.Current.Server.MapPath(fileName);


                file.SaveAs(filename3);
                PImage Pimage = new PImage()
                {
                    ImageName = fileName,
                    Path = Path.GetExtension(fileName),
                };
                fileDetails=Pimage;
              
                System.Drawing.Image imgOriginal =System.Drawing.Image.FromFile(filename3);

                //pass in whatever value you want
                System.Drawing.Image imgActual = Scale(imgOriginal);
                imgOriginal.Dispose();
                imgActual.Save(filename3);
                imgActual.Dispose();

                imageResult.ImageName = fileName;

                return imageResult;
            }
            catch (Exception ex)
            {
                // you might NOT want to show the exception error for the user
                // this is generally logging or testing

                imageResult.Success = false;
                imageResult.ErrorMessage = ex.Message;
                return imageResult;
            }
        }
       public PImage filedetails()
        {
            return fileDetails;
        }

        private bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        private System.Drawing.Image Scale(System.Drawing.Image imgPhoto)
        {
            float sourceWidth = imgPhoto.Width;
            float sourceHeight = imgPhoto.Height;
            float destHeight = 0;
            float destWidth = 0;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            // force resize, might distort image
            if (Width != 0 && Height != 0)
            {
                destWidth = Width;
                destHeight = Height;
            }
            // change size proportially depending on width or height
            else if (Height != 0)
            {
                destWidth = (float)(Height * sourceWidth) / sourceHeight;
                destHeight = Height;
            }
            else
            {
                destWidth = Width;
                destHeight = (float)(sourceHeight * Width / sourceWidth);
            }

            Bitmap bmPhoto = new Bitmap((int)destWidth, (int)destHeight,
                                        System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, (int)destWidth, (int)destHeight),
                new Rectangle(sourceX, sourceY, (int)sourceWidth, (int)sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();

            return bmPhoto;
        }
    }

    public class ImageResult
    {
        public bool Success { get; set; }
        public string ImageName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
