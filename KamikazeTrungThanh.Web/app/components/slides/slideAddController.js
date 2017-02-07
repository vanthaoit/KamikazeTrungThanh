/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideAddController', slideAddController);

    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function slideAddController(apiService, $scope, notificationService, $state) {

        $scope.slide = {
            Status: true
        }
        $scope.addSlide = addSlide;

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        function addSlide() {
            //$scope.slide.Image = JSON.stringify($scope.Image);
            $scope.slide.Url = $scope.slide.Image;
            apiService.post("api/slide/create", $scope.slide, function (result) {
                notificationService.displaySuccess("Bạn đã thêm thành công " + result.data.Name + " !!!");
                $state.go("slides");
            }, function (error) {
                notificationService.displayWarning("Thêm mới không thành công !!!");
            });
        }

        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                });

            }
            finder.popup();

        }
    }


})(angular.module('kamikazeTrungThanh.slides'));