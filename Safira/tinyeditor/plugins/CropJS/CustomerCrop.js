﻿
window.addEventListener('DOMContentLoaded', function () {
    var image = document.getElementById('image');
    var input = $(".input_Crop");
    var $alert = $('.alert');
    var $modal = $('#modal');

    var inputAvatar, inputThumb, imgPreviewCrop, lightboxPreview, urlForder, dataHeight, dataWidth, typeImage, cropper;

    input.on('change', function (e) {
        var parent = $(this).closest("#parent");
        inputAvatar = parent.find(".result-crop-avatar");
        inputThumb = parent.find(".result-crop-thumb");
        imgPreviewCrop = parent.find(".img-preview-crop");
        lightboxPreview = parent.find(".lightbox-preview");

        urlForder = $(this).data("forder");
        var checkLastCharForder = urlForder.slice(urlForder.length - 1, urlForder.length);
        if (checkLastCharForder != "/") {
            urlForder += "/";
        }

        dataWidth = $(this).data("width");
        dataHeight = $(this).data("height");

        var files = e.target.files;
        var reader,file;

        var done = function (url) {
            input.val('');
            image.src = url;

            $alert.hide();
            $modal.modal('show');
        };


        if (files && files.length > 0) {
            file = files[0];
            typeImage = file.type === 'image/png' ? file.type : 'image/jpeg';
            if (URL) {
                done(URL.createObjectURL(file));
            } else if (FileReader) {
                reader = new FileReader();
                reader.onload = function (e) {

                    done(reader.result);
                };
                reader.readAsDataURL(file);

            }
        }
    });

    $modal.on('shown.bs.modal', function () {
        cropper = new Cropper(image, {
            //aspectRatio: 1, //Dùng để crop theo ý thích
            //viewMode: 3,//Dùng để crop theo ý thích
            dragMode: 'move',
            //aspectRatio: 16 / 15,
            autoCropArea: 1,
            restore: true,
            guides: true,
            center: true,
            highlight: true,
            cropBoxMovable: true,

            cropBoxResizable: true,
            toggleDragModeOnDblclick: false,
        });
    }).on('hidden.bs.modal', function () {
        cropper.destroy();
        cropper = null;
    });

    document.getElementById('crop').addEventListener('click', function () {
        var canvas;
        //Render filename
        var guid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });

        $modal.modal('hide');

        if (cropper) {

            if (urlForder == null || urlForder == "") {
                urlForder = "/fileuploads/Commons/";
            }
            //Upload Avatar
            if (dataWidth == "" || dataWidth == null) {
                dataWidth = 1500;
            }
            if (dataHeight == "" || dataHeight == null) {
                dataHeight = 1500;
            }

            var curentWith = cropper.canvasData.naturalWidth;
            var curentHeight = cropper.canvasData.naturalHeight;



            //Upload Avatar
            canvas = cropper.getCroppedCanvas({
                width: getSize(curentWith, curentHeight, dataWidth, dataHeight).width,
                height: getSize(curentWith, curentHeight, dataWidth, dataHeight).height
            });

            canvas.toBlob(function (blob) {

                var formData = new FormData();

                formData.append('img_Avatar', blob, 'avatar.' + typeImage.split('/')[1]);
                $.ajax({
                    url: "/Admin/plugins/jQuery.filer.1.3.0/asp/Handler.ashx?filename=" + guid + "&folder=" + urlForder + "Avatar/",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (data) {
                        inputAvatar.val(data);
                        //Khi crop ok thì sẽ update lại khi bấm fancybox
                        lightboxPreview.attr("href", data);
                    },

                    error: function () {
                        alert("Lỗi upload hình");
                    }

                });
            }, typeImage);

            //Upload thumb
            dataWidth = 500;
            dataHeight = 500;

            canvas = cropper.getCroppedCanvas({
                width: getSize(curentWith, curentHeight, dataWidth, dataHeight).width,
                height: getSize(curentWith, curentHeight, dataWidth, dataHeight).height
            });


            canvas.toBlob(function (blob) {

                var formData = new FormData();

                formData.append('img_Avatar', blob, 'avatar.' + typeImage.split('/')[1]);
                $.ajax({
                    url: "/Admin/plugins/jQuery.filer.1.3.0/asp/Handler.ashx?filename=" + guid + "&folder=" + urlForder + "Thumb/",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: formData,
                    success: function (data) {

                        inputThumb.val(data);
                        imgPreviewCrop.attr("src", data.replace("~/", "/"));
                    },

                    error: function () {
                        alert("Lỗi upload hình");
                    },

                    complete: function () {
                    }
                });
            }, typeImage);
        }

    });


    var getSize = function (curentWith, curentHeight, dataWidth, dataHeight) {
        if (curentWith > curentHeight) {
            if (curentWith > dataWidth) {
                curentHeight *= dataWidth / curentWith;
                curentWith = dataWidth;
            }
        } else {
            if (curentHeight > dataHeight) {
                curentWith *= dataHeight / curentHeight;
                curentHeight = dataHeight;
            }
        }

        var objCurrentSize =
        {
            width: curentWith,
            height: curentHeight
        };

        return objCurrentSize;

    }
});