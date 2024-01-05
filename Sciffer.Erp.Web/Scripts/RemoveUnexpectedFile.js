$('#FileUpload').change(function () {
    var files = $(this).val();
    if (files != '') {
        var arr = files.split('.');
        var lastFile = arr[arr.length - 1];
        if (!(lastFile == "pdf" || lastFile == "PDF" || lastFile == "JPG" || lastFile == "jpg"||lastFile == "PNG" || lastFile == "PNG")) {
            $('#FileUpload').val('');
            alert('select image and pdf file only');
        }
    }
});