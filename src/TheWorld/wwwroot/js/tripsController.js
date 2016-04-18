(function () {
    "use strict";
    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;
        vm.Trips = []
        vm.newTrip = {}
        vm.isBusy = true;
        vm.errorMessage = ""

        $http.get("/api/trips")
            .then(function (response) {
                angular.copy(response.data, vm.Trips);
            }, function (error)
            {
                vm.errorMessage = "Failed to load data: " + error;
            }).finally(function () {
                vm.isBusy = false;
            });
        vm.addTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    vm.Trips.push(response.data);
                    vm.newTrip = {};
                }, function (error) {
                    vm.errorMessage = "Failed in saving new Trip";
                }).finally(function () {
                    vm.isBusy = false;
                });

            vm.Trips.push({ Name: vm.newTrip.name, created: new Date() });
            vm.newTrip = {}
        };
    }
})();