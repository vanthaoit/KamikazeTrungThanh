(function (app) {
    app.controller('productCategoryListController', productCategoryListController);

    productCategoryListController.$inject = ['apiService', 'notificationService', '$scope', '$ngBootbox', '$filter'];

    function productCategoryListController(apiService, notificationService, $scope, $ngBootbox, $filter) {
        $scope.productCategories = [];

        $scope.page = 0;

        $scope.totalPages = 0;

        $scope.keyword = '';

        $scope.getProductCagories = getProductCagories;

        $scope.deleteProductCategory = deleteProductCategory;

        $scope.selectAll = selectAll;

        $scope.search = search;

        $scope.isAll = false;

        $scope.deleteMulti = deleteMulti;

        function deleteMulti() {

            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    selectedProductCategories: JSON.stringify(listId)
                }
            }

            apiService.del('api/productcategory/deleteMulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công.');
            });
        }

        $scope.$watch("productCategories", function (newValue, oldValue) {
            var checked = $filter("filter")(newValue, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function deleteProductCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa ???').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/productcategory/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công.');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công.');
                });
            });
        }

        function search() {
            getProductCagories();
        }

        function getProductCagories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            };
            apiService.get('api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                else
                    notificationService.displaySuccess('Có ' + result.data.TotalCount + ' bản ghi được tìm thấy.');
                $scope.productCategories = result.data.Items;//
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getProductCagories();
    }
})(angular.module('kamikazeTrungThanh.product_categories'));