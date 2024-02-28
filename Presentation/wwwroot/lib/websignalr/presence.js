"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/presensehub").build();
connection.start().then(function () {
    console.log("Connect to presence hub successful");
}).catch(function (err) {
    return console.error(err.toString());
});
connection.on("UserIsOnline", function (email) {
    console.log(email + 'is online');
});
const toastLiveExample = document.getElementById('artistLiveToast');
const toastContent = document.getElementById('toastContent');
connection.on("CommissionReceived", function (requestUser) {
    toastContent.innerHTML = '<strong>' + requestUser + '</strong>' + " sent you a commission request";
    const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
    toastBootstrap.show();
});

