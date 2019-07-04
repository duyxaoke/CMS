var CommonHelper = function ($rootScope, $localstorage, $timeout, $q, $http) {
    let urlApi = "/api/";
    let service = {};

    service.ConfigUrl = urlApi + "Configs/";
    service.MenuUrl = urlApi + "Menus/";
    service.RoleUrl = urlApi + "Roles/";
    service.UserUrl = urlApi + "Users/";
    service.CategoryUrl = urlApi + "Categories/";
    service.PartnerUrl = urlApi + "Partners/";
    service.ContentUrl = urlApi + "Contents/";
    service.LanguageUrl = urlApi + "Languages/";

    service.DEFAULT_IMG = "/Content/admin/img/NO_IMG.png";

    return service;
}
CommonHelper.$inject = ["$rootScope", "$localstorage", "$timeout", "$q", "$http"];
