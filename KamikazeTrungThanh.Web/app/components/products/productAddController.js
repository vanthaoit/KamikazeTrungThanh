/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.addProduct = addProduct;

        function addProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post("api/product/create", $scope.product, function (result) {
                notificationService.displaySuccess("Bạn đã thêm thành công " + result.data.Name + " !!!");
                $state.go("products");
            }, function (error) {
                notificationService.displayWarning("Thêm mới không thành công !!!");
            });
        }
        function productCategories() {
            apiService.get("api/productcategory/getallparents", null, function (result) {
                $scope.productCategories = result.data;
            }, function (error) {
                console.log("cannot get list from productCategories of getallparents");
            });
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                });

            }
            finder.popup();

        }
        $scope.moreImages = [];
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }
        $scope.RemoveMoreImage = function (img) {
            $scope.moreImages.splice(img, 1);
        }
        productCategories();
    }
})(angular.module('kamikazeTrungThanh.products'));