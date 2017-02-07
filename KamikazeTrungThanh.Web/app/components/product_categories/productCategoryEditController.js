(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', 'notificationService', '$scope', '$state', '$ngBootbox', '$filter', '$stateParams', 'commonService'];

    function productCategoryEditController(apiService, notificationService, $scope, $state, $ngBootbox, $filter, $stateParams, commonService) {

        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        $scope.UpdateProductCategory = UpdateProductCategory;

        function loadProductCategoryDetail() {
            apiService.get('api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError('Cannot get getbyid of productCategory !!!');

            });
        }
        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data;

            }, function () {
                console.log('Cannot get list from parent Categories');

            });
        }


        function UpdateProductCategory() {
            apiService.put('api/productcategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess('Cập nhật thành công ' + result.data.Name + '!!!');
                $state.go('product_categories');
            }, function (error) {
                notificationService.displayError('Cập nhật thất bại !!!');
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
        loadProductCategoryDetail();
        loadParentCategory();
    }
})(angular.module('kamikazeTrungThanh.product_categories'));