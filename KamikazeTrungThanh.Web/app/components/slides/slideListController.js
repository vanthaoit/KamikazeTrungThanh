/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideListController', slideListController);

    slideListController.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$filter'];

    function slideListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.slides = [];

        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }

        $scope.page = 0;
        $scope.pagesCount = 0;

        $scope.getSlides = getSlides;
        $scope.keyword = '';
        $scope.deleteSlide = deleteSlide;
        $scope.deleteMulti = deleteMulti;

        $scope.selectAll = selectAll;

        $scope.search = search;

        function search() {
            getSlides();
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll == false) {
                angular.forEach($scope.slides, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.slides, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }


        $scope.$watch("slides", function (newValue, oldValue) {
            var checked = $filter("filter")(newValue, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);



        function deleteSlide(id, name) {
            $ngBootbox.confirm("Bạn có chắc muốn xóa Slide " + name + " - ID = " + id).then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/slide/delete', config, function (result) {
                    notificationService.displaySuccess("Đã xóa thành công " + result.data.Name + " !!!!");
                    search();
                }, function (error) {
                    notificationService.displayWarning("Xóa không thành công !!!");
                });

            });
        }

        function deleteMulti() {
            var count = 0;
            var listID = [];
            $.each($scope.selected, function (i, item) {
                count = count + 1;
                listID.push(item.ID)
            });

            $ngBootbox.confirm("Bạn có chắc muốn xóa " + count + "bản ghi !!!").then(function () {
                var config = {
                    params: {
                        checkSlides: JSON.stringify(listID)
                    }
                }
                apiService.del('api/slide/deleteMulti', config, function (result) {
                    notificationService.displaySuccess("Xóa thành công danh sách" + result.data + "slides !!!");
                    search();
                }, function () {
                    notificationService.displayWarning("Xóa danh sách slide thất bại !!!");
                })

            });
        }

        function getSlides(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 5
                }
            }
            apiService.get('api/slide/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
                }
                $scope.slides = result.data.Items;
                $scope.page = result.data.Page;
                $scope.totalPages = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load slide failed.');
            });
        }

        $scope.getSlides();

    }
})(angular.module('kamikazeTrungThanh.slides'));