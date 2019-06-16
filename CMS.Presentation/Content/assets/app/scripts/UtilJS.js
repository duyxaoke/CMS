var UtilJS = {};
UtilJS.Loading = {};

UtilJS.String = {};
UtilJS.String.IsNullOrEmpty = function (str) {
    if (!str || str == null) {
        return true;
    }
    return false;
};

UtilJS.String.RemoveUnicode = function (str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    return str;
}

UtilJS.String.IsContain = function (strRoot, strRequest, IsSearchWithFormat) {
    if (UtilJS.String.IsNullOrEmpty(strRoot)) {
        return false;
    }
    if (UtilJS.String.IsNullOrEmpty(strRequest)) {
        return true;
    }
    strRoot = strRoot.normalize();
    strRequest = strRequest.normalize();
    strRoot = strRoot.toLowerCase();
    strRequest = strRequest.toLowerCase();

    if (IsSearchWithFormat) {
        strRoot = UtilJS.String.RemoveUnicode(strRoot);
        strRequest = UtilJS.String.RemoveUnicode(strRequest);
    }

    if (strRoot.indexOf(strRequest) < 0) {
        return false;
    }
    return true;
};
UtilJS.String.IsContainUnsigned = function (strRoot) {
    if (UtilJS.String.IsNullOrEmpty(strRoot)) {
        return false;
    }
    strRoot = strRoot.normalize();
    strRoot = strRoot.toLowerCase();
    strRoot = UtilJS.String.RemoveUnicode(strRoot);

    if (/^[a-zA-Z0-9- ]*$/.test(strRoot) == false) {
        return true;
    }
    return false;
};
