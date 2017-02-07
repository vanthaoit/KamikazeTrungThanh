/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideEditController', slideEditController);

    slideEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function slideEditController(apiService, $scope, notificationService, $state, $stateParams) {


        $scope.slide = {};
        $scope.ckeditorOptions = {
            languague: 'vi',
            height: '200px'
        }
        $scope.editSlide = editSlide;

        function loadSlideDetail() {
            apiService.get('api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data;
            }, function (error) {
                notificationService.displayWarning("Load error slide detail " + error);
            });
        }
        function editSlide() {
            apiService.put('api/slide/update', $scope.slide, function (result) {
                notificationService.displaySuccess("Cập nhật thành công " + result.data.Name);
                $state.go("slides");
            }, function (error) {
                notificationService.displayWarning("Có lỗi trong quá trình cập nhật Slide " + error);
            });

        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                })
            }
            finder.popup();
        }

        loadSlideDetail();
    }
})(angular.module('kamikazeTrungThanh.slides'));