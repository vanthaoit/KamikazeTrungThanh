/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];
    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true,
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        $scope.addProductCategory = addProductCategory;
        function addProductCategory() {

            apiService.post('api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " được thêm mới thành công !!!");
                $state.go('product_categories');

            }, function () {

                notificationService.displayWarning("Có lỗi xảy ra trong quá trình thêm mới !!!");
            });
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;

            }, function () {
                console.log('Cannot get list from parent Categories');

            });
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.productCategory.Image = fileUrl;
                });

            }
            finder.popup();

        }
        loadParentCategory();
    }


})(angular.module('kamikazeTrungThanh.product_categories'));