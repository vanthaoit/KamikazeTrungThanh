(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return 'Kích hoạt';
            else
                return 'Đã khóa';
        }
    });
})(angular.module('kamikazeTrungThanh.common'));