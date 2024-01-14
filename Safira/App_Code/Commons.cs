using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using CodeUtility;
public class Commons
{
    #region URLs
    public static string EmptyAnchorLink
    {
        get
        {
            return "javascript:void(0)";
        }
    }
    #endregion

    #region PriceAndBonus
    public static string GetPriceBonusPecent(double price, double oldPrice)
    {
        if (price <= 0 || oldPrice <= 0 || price >= oldPrice)
            return string.Empty;

        double pecent = Math.Round((price * 100 / oldPrice),0);

        return "-{0}%".StringFormat(pecent);
    }

    public static string GetPrice(double price)
    {
        if (price <= 0)
            return "Giá liên hệ";

        return "{0:0,00 đ}".StringFormat(price);
    }
    #endregion

    #region Others
    public static string[] GetImageListArray(object ImageList)
    {
        return ImageList.ToSafetyString().Split('\n');
    }

    public static int GetGroupItemCount(int total, int col, int row)
    {
        //if (total <= col)
        //    return 1;

        //return Math.Floor(total.ToDouble() / col).RoundToInt();

        return 1;
    }
    #endregion

    #region Uploads
    public static bool UploadImage(FileUpload fileUpload, string folderSave, ref string avatar, ref string thumb, ref Exception error)
    {
        UploadUtility upload        = new UploadUtility();
        upload.FileUpload           = fileUpload;
        upload.FolderSave           = folderSave;
        upload.FullMaxWidth         = 1000;
        upload.ThumbMaxWidth        = 400;
        upload.MaxFileSize          = 3 * 1024 * 1024;
        upload.AutoGenerateFileName = true;

        upload.UploadImage(ref avatar, ref thumb, ref error);

        if (error != null)
            return false;

        return true;
    }

    public static bool UploadIcon(FileUpload fileUpload, string folderSave, ref string icon, ref Exception error)
    {
        UploadUtility upload = new UploadUtility();
        upload.FileUpload = fileUpload;
        upload.FolderSave = folderSave;
        upload.MaxFileSize = 50 * 1024 * 1024;
        upload.AutoGenerateFileName = true;

        upload.UploadFile(ref icon, ref error);

        if (error != null)
            return false;

        return true;
    }

    public static bool UploadAttachmentFile(FileUpload fileUpload, string folderSave, ref string attachmentFile, ref Exception error)
    {
        UploadUtility upload = new UploadUtility();
        upload.FileUpload = fileUpload;
        upload.FolderSave = folderSave;
        upload.MaxFileSize = 50 * 1024 * 1024;
        upload.AutoGenerateFileName = true;

        upload.UploadFile(ref attachmentFile, ref error);

        if (error != null)
            return false;

        return true;
    }
    #endregion

    public static string AdminShalt = "khongaibiet";
}