!function(){"use strict";function i(i){var n=this;n.Trips=[],n.newTrip={},n.isBusy=!0,n.errorMessage="",i.get("/api/trips").then(function(i){angular.copy(i.data,n.Trips)},function(i){n.errorMessage="Failed to load data: "+i})["finally"](function(){n.isBusy=!1}),n.addTrip=function(){n.isBusy=!0,n.errorMessage="",i.post("/api/trips",n.newTrip).then(function(i){n.Trips.push(i.data),n.newTrip={}},function(i){n.errorMessage="Failed in saving new Trip"})["finally"](function(){n.isBusy=!1}),n.Trips.push({Name:n.newTrip.name,created:new Date}),n.newTrip={}}}i.$inject=["$http"],angular.module("app-trips").controller("tripsController",i)}();