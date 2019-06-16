var fileUpload = function ($q, $timeout, $localstorage, ApiHelper) {
    return {
        restrict: 'A',
        transclude: true,

        scope: {
            root: "=",
            isMultipleFile: "=",
            isLoading: "=",
            accept: "="
        },
        template: '<ng-transclude></ng-transclude>',

        link: function (scope, element, attrs) {
            function objFile() {
                this.IsDeleted = false,
                    this.Base64 = '',
                    this.Src = '',
                    this.DataType = '',
                    this.Http = new XMLHttpRequest(),
                    this.FileInput = {},
                    this.Percent = 0,
                    this.FileName = ''
            };
            if (!scope.root) {
                scope.root = {};
            }
            if (!scope.root.LstFile) {
                scope.root.LstFile = [];
            }
            scope.btnOpenDialog = element[0].querySelector('.btnOpenDialog');
            scope.input = element[0].querySelector('.files');

            if (!scope.root.ActionUpload) {
                scope.root.ActionUpload = "/UploadFiles/UploadImage/";
            }
            if (!scope.root.PathUpload) {
                scope.root.PathUpload = "/";
            }

            if (scope.btnOpenDialog) {
                scope.btnOpenDialog.addEventListener('click', function (e) {
                    $timeout(function () {
                        angular.element(scope.input).trigger('click');
                    });
                });
            }

            scope.input.addEventListener('change', function (e) {
                $timeout(function () {
                    var files = e.target.files;
                    if (files.length == 0) {
                        return;
                    }
                    var size = files[0].size / 1024 / 1024;//MB
                    if (size > 2) {
                        sys.Alert(false, "Đính kèm không vượt quá 2 MB.");
                        return;
                    }
                    if (!scope.root.IsMultipleFile) {
                        scope.root.LstFile = [];
                        if (files.length > 1) {
                            sys.Alert(false, "Vui lòng up 1 file");
                            return;
                        }
                        if (scope.root.accept != undefined && (!UtilJS.String.IsContain(scope.root.accept, files[0].type)) || files[0].type == '') {
                            sys.Alert(false, "Định dạng file không đúng.");
                            return;
                        }
                    }

                    for (var i = 0; i < files.length; i++) {
                        var objFileNew = new objFile();
                        objFileNew.FileInput = files[i];

                        objFileNew.FileName = files[i].name.normalize();
                        //objFileNew.FileName = UtilJS.String.RemoveUnicode(objFileNew.FileName);

                        if (scope.root.IsListenProgress) {
                            objFileNew.AlertNotFoundTimeout = false;
                            ListenProgress(objFileNew);
                            ListenStateChange(objFileNew);
                        }
                        scope.root.LstFile.push(objFileNew);

                        if (!scope.root.IsMultipleFile) {
                            //scope.strToken = $localstorage.getObject("UserPricinpal").Token;
                            //if (!scope.strToken) {
                            //    sys.Alert(false, "Mã xác thực chưa được cấp, bạn vui lòng đăng nhập lại", 'Thông báo');
                            //    scope.root.API.ResetItem(objFileNew);
                            //    return;
                            //}
                            let strName = objFileNew.FileInput.name;
                            strName = strName.normalize(); 
                            strName = strName.toLowerCase();

                            objFileNew.Http.open("POST", scope.root.ActionUpload, true);
                            objFileNew.Http.setRequestHeader("Content-Type", "multipart/form-data");
                            objFileNew.Http.setRequestHeader("FileName", UtilJS.String.RemoveUnicode(strName));
                            objFileNew.Http.setRequestHeader("FileSize", objFileNew.FileInput.size);
                            objFileNew.Http.setRequestHeader("FileType", objFileNew.FileInput.type);
                            objFileNew.Http.setRequestHeader("SaveTo", scope.root.SaveTo);
                            //objFileNew.Http.setRequestHeader("Authorization", 'Bearer ' + scope.strToken);

                            objFileNew.Http.send(objFileNew.FileInput);
                        }
                    }
                }).then(function () {
                    angular.element(scope.input).val(null);
                });
            });

            scope.root.API = {};
            scope.root.API.ClearLstItem = function () {
                scope.root.LstFile = [];
            };

            scope.root.API.ResetItem = function (objFileNew) {
                objFileNew.Percent = 0;
                objFileNew.FileName = '';
                objFileNew.FilePathSaved = '';
            };

            var ListenProgress = function (objFileNew) {
                objFileNew.Http.upload.addEventListener('progress', function (event) {
                    $timeout(function () {
                        fileLoaded = event.loaded; //Đã load được bao nhiêu
                        fileTotal = event.total; //Tổng cộng dung lượng cần load
                        fileProgress = parseInt((fileLoaded / fileTotal) * 100) || 0;
                        objFileNew.Percent = fileProgress;
                    }, 50);
                }, false);
            };
            var ListenStateChange = function (objFileNew) {
                objFileNew.Http.onreadystatechange = function (event) {
                    if (objFileNew.Http.readyState == 4 && objFileNew.Http.status == 200) {
                        $timeout(function () {
                            var response = JSON.parse(objFileNew.Http.responseText);
                            if (response.Code != 200) {
                                objFileNew.Percent = 0;
                                objFileNew.FileName = '';
                                sys.Alert(false, "Upload file thất bại");
                                return;
                            }
                            if (response.Code == 200 && response.Success) {
                                objFileNew.Percent = 100;
                                objFileNew.FilePathSaved = response.Data.PathFile;
                                objFileNew.FilePathRelative = response.FilePathRelative;
                                //jAlert.Success("Upload file thành công.", 'Thông báo');
                            }

                            //objFileNew.Http.removeEventListener('progress', false);
                        }, 200);
                    }
                    else if (objFileNew.Http.readyState == 4 && objFileNew.Http.status != 200) {
                        $timeout.cancel(objFileNew.AlertNotFoundTimeout);
                        objFileNew.AlertNotFoundTimeout = $timeout(function () {
                            if (objFileNew.Http.status == 404) {
                                sys.Alert(false, "Định dạng file không đúng.");
                            }
                            else if (objFileNew.Http.status == 401) {
                                //CommonFactory.ConfirmRedirectLogin(); 
                            }
                            else if (objFileNew.Http.status == 403) {
                                sys.Alert(false, "Bạn không có quyền");
                            }
                            else {
                                sys.Alert(false, "Lỗi status " + objFileNew.Http.status);
                                console.log(objFileNew.Http);
                            }
                            objFileNew.Percent = 0;
                            objFileNew.FileName = '';
                            return;
                        }, 200);
                    }
                    scope.$apply();
                }
            };

            scope.root.IsReady = true;
        } //link
    }; //return
};

fileUpload.$inject = ["$q", "$timeout", "$localstorage", "ApiHelper"];
