/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('kamikazeTrungThanh', [
                                            'kamikazeTrungThanh.product_categories',
                                            'kamikazeTrungThanh.products',
                                            'kamikazeTrungThanh.slides',
                                            'kamikazeTrungThanh.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('base', {
            url: '',
            templateUrl: '/app/shared/views/baseView.html',
            abstract: true
        })
        .state(
            "home", {
                url: "/admin",
                parent: "base",
                templateUrl: "/app/components/home/homeView.html",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise("/admin");
    }
})();