var customSelect2 = function ($q) {
    return {
        restrict: 'E',
        scope: {
            myroot: "="
        },
        templateUrl: "/Content/assets/app/directive/CustomSelect2/customSelect2.html",

        link: function (scope, element, attrs) {
            //#region function
            scope.myroot.UpdateValueSelected = function () {
                clearInterval(scope.myroot.myTimer);
                let defer = $q.defer();
                scope.myroot.myTimer = setInterval(() => {
                    if (scope.myroot.IsFinishRender) {
                        if (scope.myroot.IsDebug) {
                            debugger;
                        }
                        clearInterval(scope.myroot.myTimer);
                        scope.SetValid(scope.myroot.Value);

                        scope.myControl.val(scope.myroot.Value);

                        scope.myControl_s2id = $(element[0]).find('.select2-container');
                        //scope.myControl_s2id.select2("val", scope.myroot.Value);
                        scope.myControl.val(scope.myroot.Value).trigger('change');
                        defer.resolve(scope.myroot.Value);
                    }
                }, 100);
                return defer.promise;
            }
            scope.SetValid = function (ValueClient, IsValid) {
                if (ValueClient === "") {
                    $("input[name=" + scope.myroot.ID + "]").val('');
                }
                else {
                    $("input[name=" + scope.myroot.ID + "]").val(ValueClient);
                }
                try {
                    IsValid && ($("input[name=" + scope.myroot.ID + "]").valid());
                } catch (e) { }
            };
            //#endregion

            scope.myControl = $(element[0]).find('.myControl');
            //required 

            //default
            //myroot.Core.ValidType == "Required"

            //result
            //scope.myroot.Value => scope.myroot.Value 
            if (scope.myroot.IsDebug) {
                debugger;
            }
            if (!scope.myroot.Core.Label) {
                scope.myroot.Core.label = attrs.label;
                scope.myroot.Core.label2 = "--- " + attrs.label.charAt(0).toUpperCase() + attrs.label.slice(1) + " ---";
            }
            else {
                scope.myroot.Core.label2 = scope.myroot.Core.Label;
            }

            scope.myroot.ID = attrs.myroot.replace(".", "_");
            scope.myroot.IsFinishRender = !scope.myroot.Lst || scope.myroot.Lst && scope.myroot.Lst.length == 0;

            !scope.myroot.API && (scope.myroot.API = {});
            !scope.myroot.CallBack && (scope.myroot.CallBack = {});

            !scope.myroot.Core.ValueDefault && (scope.myroot.Core.ValueDefault = "");

            scope.myroot.ValueOrginal = '';
            if (scope.myroot.Value) {
                scope.myroot.ValueOrginal = scope.myroot.Value;
                scope.myroot.Value = scope.myroot.Value.toString();
            }
            else {
                scope.myroot.Value = scope.myroot.Core.ValueDefault;
            }

            scope.myroot.Onchange = function () {
                if (scope.myroot.IsDebug) {
                    debugger;
                }
                scope.SetValid(scope.myroot.Value, true);
                scope.myroot.CallBack.OnValuechanged && (scope.myroot.CallBack.OnValuechanged(scope.myroot.Value));
                scope.myroot.CallBack.Onchanged && (scope.myroot.CallBack.Onchanged());
            };
            scope.myroot.API.DiscardChange = function () {
                scope.myroot.API.SetValue(scope.myroot.ValueOrginal, true);
            };
            scope.myroot.API.SetValue = function (Value, IsNotFireChangedEvent) {
                try {
                    if (scope.myroot.IsDebug) {
                        debugger;
                    }
                    if (Value === null || Value === undefined) {
                        Value = scope.myroot.Core.ValueDefault;
                    }
                    scope.myroot.ValueOrginal = Value;
                    scope.myroot.Value = Value.toString();
                    scope.myroot.UpdateValueSelected().then((x) => {
                        if (scope.myroot.IsDebug) {
                            debugger;
                        }
                        if (!IsNotFireChangedEvent) {
                            scope.myroot.CallBack.OnValuechanged && (scope.myroot.CallBack.OnValuechanged(x));
                        }
                    });
                } catch (e) {
                    debugger;
                    throw e;
                }
            };
            scope.myroot.API.DataSource = function (Lst) {
                if (scope.myroot.IsDebug) {
                    debugger;
                }
                scope.myroot.Lst = Lst;
                scope.myroot.UpdateValueSelected();
            };
            scope.myroot.API.OnStart = function () {
                if (scope.myroot.IsDebug) {
                    debugger;
                }
                scope.myroot.IsFinishRender = false;
            };
            scope.myroot.API.OnEnd = function () {
                if (scope.myroot.IsDebug) {
                    debugger;
                }
                scope.myroot.IsFinishRender = true;
            };
            //ready
            scope.myroot.UpdateValueSelected();
            scope.myControl.select2();

            scope.myroot.IsReady = true;
        }
    };
};
customSelect2.$inject = ["$q"];
var formatMoney = function ($filter, $timeout) {
    return {
        require: '?ngModel',
        restrict: "A",
        scope: {
            myModel: "=",
            precision: "=",
            formatMoneyNoInput: "="
        },
        link: function (scope, elem, attrs, ctrl) {
            if (scope.precision == null || scope.precision == undefined)
                scope.precision = 0;
            elem.maskMoney({
                allowNegative: true, thousands: ',', decimal: '.', affixesStay: false, allowZero: true, precision: scope.precision
            });
            elem.keydown(function (event) {
                var c = String.fromCharCode(event.which);
                if (_.contains(scope.formatMoneyNoInput, c)) {
                    event.preventDefault();
                    return;
                }
                $timeout(function () {
                    scope.myModel = parseFloat(elem.val().replace(new RegExp(",", 'g'), ""));
                    elem.trigger("change");
                });
            });
            scope.$watch('myModel', function () {
                if ($.isNumeric(scope.myModel) && scope.myModel.toString().indexOf('.') > 0) {
                    elem.val(scope.myModel.toFixed(scope.precision)).trigger('mask.maskMoney');
                }
                else {
                    elem.val(scope.myModel).trigger('mask.maskMoney');
                }
            });
        }
    }
};
formatMoney.$inject = ['$filter', '$timeout'];
var getWidth = function ($timeout, $interval) {
    return {
        restrict: 'A',

        scope: {
            getWidth: "=",
        },

        link: function (scope, element, attrs) {
            $(function () {
                scope.getWidth = element[0].offsetWidth; 

                $interval(function () {
                    scope.getWidth = element[0].offsetWidth;
                }, 500); 
            });
        }
    };
};

getWidth.$inject = ["$timeout", "$interval"];

var getHeight = function ($timeout, $interval) {
    return {
        restrict: 'A',

        scope: {
            getHeight: "=",
        },

        link: function (scope, element, attrs) {
            $(function () { 
                scope.getHeight = element[0].offsetHeight; 

                $interval(function () {
                    scope.getHeight = element[0].offsetHeight;
                }, 500);
            });
        }
    };
};

getHeight.$inject = ["$timeout", "$interval"];
var lazyLoad = function ($timeout, $window) {
    return {
        restrict: 'A',
        scope: {
            fncallback: "&lazyLoad"
        },

        link: function (scope, element, attrs) {
            scope.IsLoaded = false;
            scope.raw = element[0];  
            angular.element($window).bind("scroll", function (e) {
                var IsVisible = $(scope.raw).is(':visible');
                if (!scope.IsLoaded && IsVisible) {
                    var PositionYofElement = $(scope.raw).position().top;
                    if (this.pageYOffset + this.innerHeight >= PositionYofElement) {
                        scope.fncallback();
                        scope.IsLoaded = true; 
                        scope.$apply();
                    }

                }
            });
        }
    };
};

lazyLoad.$inject = ["$timeout", "$window"];
 
var noInput = function () {
    return {
        restrict: 'A',

        scope: {
            noInput: "="
        }, 

        link: function (scope, element, attrs) {  
            element.bind("keydown keypress", function (event) { 
                var c = String.fromCharCode(event.which);
                if (_.contains(scope.noInput, c)) { 
                    event.preventDefault();
                } 
            });

            //scope.KeyCode = [];
            //scope.noInput.forEach(function (item) {
            //    scope.KeyCode.push(item.charCodeAt(0));
            //});

            //element.bind("keydown keypress", function (event) {
            //    if (_.contains(scope.KeyCode, event.which)) {
            //        event.preventDefault();
            //    }
            //});
        }
    }; 
};
noInput.$inject = [];
var whenEnter = function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.whenEnter);
                });

                event.preventDefault();
            }
        });
    };
};
whenEnter.$inject = [];
var compile = function ($compile) {
    return function (scope, element, attrs) {
        scope.$watch(
          function (scope) {
              // watch the 'compile' expression for changes
              return scope.$eval(attrs.compile);
          },
          function (value) {
              // when the 'compile' expression changes
              // assign it into the current DOM
              element.html(value);

              // compile the new DOM and link it to the current
              // scope.
              // NOTE: we only compile .childNodes so that
              // we don't get into infinite loop compiling ourselves
              $compile(element.contents())(scope);
          }
      );
    };
};
compile.$inject = ["$compile"];
var inputFormat = function ($filter) {
    return {
        require: '?ngModel',
        restrict: "A",
        link: function (scope, elem, attrs, ctrl) {
            function isFloat(n) {
                return Number(n) === n && n % 1 !== 0;
            }
            var allowdecimal = (attrs["allowDecimal"] == 'true') ? true : false;
            scope.allowdecimal = allowdecimal;
            scope.defaultValue = attrs["defaultValue"] ? attrs["defaultValue"] : false;
            if (!ctrl) return;

            elem.bind("keypress", function (event) {
                var keyCode = event.which || event.keyCode;
                var allowdecimal = (attrs["allowDecimal"] == 'true') ? true : false;
                if (((keyCode > 47) && (keyCode < 58)) || (keyCode == 8) || (keyCode == 9) || (keyCode == 190) || (keyCode == 39) || (keyCode == 37) || (keyCode == 43) || (allowdecimal && keyCode == 46))
                    return true;
                else
                    event.preventDefault();
            });

            ctrl.$formatters.unshift(function (a) {
                return $filter(attrs.inputFormat)(ctrl.$modelValue ? ctrl.$modelValue : 0);
            });

            ctrl.$parsers.unshift(function (viewValue) {
                var allowdecimal = (attrs["allowDecimal"] == 'true') ? true : false;
                if (scope.defaultValue && !parseInt(viewValue)) {
                    viewValue = scope.defaultValue;
                }
                else if (!allowdecimal && isFloat(parseFloat(viewValue))) {
                    viewValue = scope.defaultValue;
                }
                var plainNumber = viewValue.replace(/[^\d|\.|\-]/g, '');
                plainNumber = plainNumber || 0;
                if (plainNumber == '') return;
                var dots = plainNumber.match(/\./g);
                var dotAF = plainNumber.match(/\.$/g);
                dots = (dots != null && dots.length == 1 && dotAF != null) ? '.' : '';
                var temp = $filter(attrs.inputFormat)(plainNumber);

                elem.val(temp + dots).trigger('change');

                return parseFloat(plainNumber);
            });
        }
    }
};
inputFormat.$inject = ['$filter'];
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