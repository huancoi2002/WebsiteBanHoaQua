<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyUpload.aspx.cs" Inherits="TinymceMyUpload" %>

<%@ Register Src="~/Admin/UserControl/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>TinyUpload</title>
    <link href="skins/lightgray/skin.min.css" rel="stylesheet" type="text/css" />

    <script src="0.jquery.1.9.1.min.js" type="text/javascript"></script>
    <meta http-equiv="refresh" content="1860" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <!--CSS-->
    <link rel="icon" href="/Admin/favicon.ico" type="image/ico" />
    <link rel="stylesheet" href="/Admin/css/full-css-import.css" />
    <link href="/Admin/plugins/CropJS/cropper.min.css" rel="stylesheet" />
    <link href="/Admin/css/mystyle.css" rel="stylesheet" />
    <!--JS-->
    <script src="/Admin/javascript/fullobf.js" type="text/javascript" charset="utf-8"></script>
    <script src="/tinyeditor/1.jquery.tinymce.min.js" type="text/javascript"></script>
    <script src="/tinyeditor/2.tinymce.min.js" type="text/javascript"></script>
    <script src="/tinyeditor/3.tinymce.customfunction.js" type="text/javascript"></script>
    <script src="/Admin/javascript/dataconfig.js" type="text/javascript"></script>
    <script src="/Admin/plugins/CropJS/cropper.min.js" type="text/javascript"></script>
    <script src="/Admin/plugins/CropJS/CustomerCrop.js" type="text/javascript"></script>

    <script type="text/javascript">
       function InsertImage() {
            var _url = $("#input_Thumb").val().replace("~/", "/");
            var imgtag = "<img style='width:750px; height: 420px;' hspace='5px' src='" + _url + "' alt='" + _url + "'";
            imgtag += "/>";
            parent.tinyMCE.activeEditor.selection.setContent(imgtag);
            parent.tinyMCE.activeEditor.windowManager.close();
        }

        $(function () {
            var url = window.location.href;
            var urlForder = getParameterByName("folder", url);
            if (urlForder != "" && urlForder != null) {
                var checkLastCharForder = urlForder.slice(urlForder.length - 1, urlForder.length);
                if (checkLastCharForder != "/") {
                    urlForder += "/";
                }
            }
            $("#tinyeditorCrop").find("input[type='file']").attr("data-forder", urlForder);
        })

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="row-form">
            <div class="clearfix" id="tinyeditorCrop">
                <div class="span2 title">
                    Hình đại diện:
                </div>
                <div class="span10 input-append file-upload-container parent-crop" id="parent">
                    <label>
                        <a href="~/Admin/css/img/no_image.jpg" runat="server"
                            class="fancybox lightbox-preview" rel="group" id="a_Avatar">
                            <img runat="server" clientidmode="static" id="img_Avatar"
                                src="~/Admin/css/img/no_image.jpg" alt="avatar"
                                class="default-image img-polaroid avatar img-preview-crop file-upload-preview" />
                        </a>
                        <br />
                        <input clientidmode="static" runat="server" preview="file-upload-preview" type="file" data-forder="/fileuploads/Product/Content/" class="sr-only skip file-upload-control input_Crop" name="image" accept="image/*" />
                        <br />
                    </label>
                    <span class="btn file-upload-thumb-remove">X</span>
                    <span>Ảnh hỗ trợ: *.jpg, *.jpeg, *.gif, *.png</span>
                    <div class="control-remove">
                        <input runat="server" id="input_Avatar" class="tipb full-file-url result-crop-avatar "
                            type="text" readonly="readonly" visible="false"
                            title="Nhấp đôi chuột để bắt đầu chỉnh sửa" />
                        <a class="hide btn file-upload-url-remove">X</a>
                        <br />
                        <input runat="server" id="input_Thumb" class="tipb full-file-url result-crop-thumb"
                            type="text" readonly="readonly"
                            title="Nhấp đôi chuột để bắt đầu chỉnh sửa" />
                        <a class="btn file-upload-url-remove">X</a>

                    </div>
                </div>
                <div id="mce_89-body" class="mce-container-body mce-abs-layout" style="width: 100%; height: 50px;">
                    <div id="mce_90" class="mce-widget mce-btn mce-primary mce-first mce-abs-layout-item" tabindex="-1" role="button" style="right: 135px; top: 10px; width: 50px; height: 28px;">
                        <input type="submit" name="Button_OK" value="Lưu" onclick="InsertImage(); return false;" id="Button_OK" tabindex="-1" class="button" role="presentation" style="height: 100%; width: 100%;">
                    </div>
                    <div id="mce_91" class="mce-widget mce-btn mce-last mce-abs-layout-item" tabindex="-1" role="button" style="right: 70px; top: 10px; width: 54px; height: 28px;">
                        <input type="submit" name="Button_Cancel" value="Hủy" onclick="parent.tinyMCEClose(); return false;" id="Button_Cancel" tabindex="-1" role="presentation" style="height: 100%; width: 100%; text-align: center; background-color: rgba(128, 128, 128, 0.76)">
                    </div>
                </div>
            </div>
        </div>
        <!-- #region Modal Crop -->
        <div class="modal fade bd-example-modal-lg" id="modal" tabindex="-1" role="dialog" aria-labelledby="modalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h5 class="modal-title" id="modalLabel">Crop image</h5>
                    </div>
                    <div class="modal-body">
                        <div class="img-container">
                            <img runat="server" clientidmode="static" id="image" src="~/Admin/css/img/no_image.jpg" alt="chưa chọn hình" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                        <button type="button" class="btn btn-primary" id="crop">Ok</button>
                    </div>
                </div>

            </div>
        </div>
        <!-- #End region Modal Crop -->

    </form>
</body>
</html>
