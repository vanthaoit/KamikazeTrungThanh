/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProducts = getProducts;
        $scope.keyword = '';
        $scope.search = search;

        $scope.selectAll = selectAll;
        $scope.deleteProduct = deleteProduct;

        $scope.deleteMulti = deleteMulti;

        function search() {
            getProducts();
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }

        }



        $scope.$watch("products", function (newValue, oldValue) {
            var checked = $filter("filter")(newValue, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteMulti() {

            var listID = [];
            var count = 0;
            $.each($scope.selected, function (i, item) {

                count = count + 1;
                listID.push(item.ID);
            });

            $ngBootbox.confirm("Bạn có chắc muốn xóa " + count + " bản ghi !!!").then(function () {
                var config = {
                    params: {
                        checkedProducts: JSON.stringify(listID)
                    }
                }
                apiService.del("api/product/deleteMulti", config, function (result) {
                    notificationService.displaySuccess("Xóa thành công " + result.data + " bản ghi !!!");
                    search();
                }, function (error) {
                    notificationService.displayWarning("Xóa không thành công !!!");
                });

            });
        }

        function deleteProduct(id, name) {

            $ngBootbox.confirm("Bạn có chắc muốn xóa sản phẩm " + name + " với ID = " + id + " ?").then(function () {

                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del("api/product/delete", config, function (result) {
                    notificationService.displaySuccess("Bạn đã xóa thành công " + result.data.Name + "!!!");
                    search();
                }, function () {
                    notificationService.displayWarning("Xóa không thành công !!!");
                });

            });
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 10
                }
            }
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load product failed.');
            });
        }

        $scope.getProducts();
    }


})(angular.module('kamikazeTrungThanh.products'));