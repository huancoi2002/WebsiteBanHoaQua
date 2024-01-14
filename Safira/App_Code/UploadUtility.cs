using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for UploadUtility
/// </summary>
public class UploadUtility
{
    public UploadUtility()
    {
    }

    public FileUpload FileUpload { get; set; }
    public string FolderSave { get; set; }
    public int FullMaxWidth { get; set; }
    public int ThumbMaxWidth { get; set; }
    public int MaxFileSize { get; set; }
    public bool AutoGenerateFileName { get; set; }

    public void UploadImage(ref string avatar, ref string thumb, ref Exception error)
    {
        if (FolderSave == string.Empty)
            FolderSave = "/fileuploads/ImageList/";

        if (!FolderSave.EndsWith("/"))
            FolderSave += "/";

        string folderThumb = FolderSave + "Thumbs/";
        //Upload image
        try
        {
            string imageFull = ResizeAndUploadImage(FolderSave, FullMaxWidth);
            string imageThumb = ResizeAndUploadImage(folderThumb, ThumbMaxWidth);
            avatar = imageFull;
            thumb = imageThumb;
        }
        catch (Exception ex)
        {
            ex = error;
        }
    }



    public void UploadFile(ref string icon, ref Exception error)
    {

        if (FolderSave == string.Empty)
            FolderSave = "/fileuploads/ImageList/";

        if (!FolderSave.EndsWith("/"))
            FolderSave += "/";

        string folderThumb = FolderSave + "Thumbs/";
        //Upload image
        try
        {
            string imageFull = ResizeAndUploadImage(FolderSave, 500);
            icon = imageFull;
        }
        catch (Exception ex)
        {
            ex = error;
        }
    }


    private string ResizeAndUploadImage(string folder, int maxWidth)
    {
        Stream strm = FileUpload.PostedFile.InputStream;
        using (var image = System.Drawing.Image.FromStream(strm))
        {

            System.Drawing.Image objImage = ScaleImage(image, maxWidth);

            FolderUtility.CreateFolder(folder);
            string fileName = folder + Guid.NewGuid() + "_" + FileUpload.FileName;
            var path = HttpContext.Current.Server.MapPath(fileName);
            objImage.Save(path);

            return fileName;
        }

    }


    private static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth)
    {
        var ratio = (double)maxWidth / image.Width;
        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);
        var newImage = new Bitmap(newWidth, newHeight);
        using (var g = Graphics.FromImage(newImage))
        {
            g.DrawImage(image, 0, 0, newWidth, newHeight);
            
        }
        return newImage;
    }
}