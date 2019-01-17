$(document).ready(function () {
    $("#UploadFile").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }

    });

});
var ReadImage = function (file) {
    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {
        image.src = _file.target.result;
        image.onload = function () {
            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = ~~(file.size / 1024) + ' kb ';
            $("#tergetimg").attr('src', _file.target.result);
            $("#description").text(" Size " + size + ", " + height + " X " + width + "");
            // $("#description").text(" Size " + size + "");
            $("#imgPreview").show();

        }
    }
}

var clearPreview = function () {
    $("#UploadFile").val('');
    ("#description").text('');
    $("#imgPreview").hide();
}
