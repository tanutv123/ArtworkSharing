"use strict";


var connection = new signalR.HubConnectionBuilder().withUrl("/presensehub").build();
connection.start().then(function () {
    console.log("Connect to presence hub successful");
}).catch(function (err) {
    return console.error(err.toString());
});
connection.on("UserIsOnline", function (email) {
    console.log(email + ' is online');
});
const toastLiveExample = document.getElementById('artistLiveToast');
const toastContent = document.getElementById('toastContent');
const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample);
connection.on("CommissionReceived", function (requestUser) {
    toastContent.innerHTML = '<strong>' + requestUser + '</strong>' + " sent you a commission request";
    toastBootstrap.show();
});
connection.on("ProgressImageRequest", function (id) {
    //toastContent.innerHTML = '<strong>' + requestUser + '</strong>' + ' requested a progress image for <a href="/artist/artistcommissionrequestdetail?id="' + requestId + '">this commission</a>';
    var commissionLink = '<a href="/audience/commissionrequesthistorydetail?id=' + id + '">Check the commission</a>';
    toastContent.innerHTML = 'You have a new progress image request.' + commissionLink;
    toastBootstrap.show();
});
connection.on("CommissionAccept", function (artist, id) {
    var commissionLink = '<a href="/audience/commissionrequesthistorydetail?id=' + id + '">Check the commission</a>';
    toastContent.innerHTML = artist + ' accept your commission. ' + commissionLink;
    toastBootstrap.show();
});
connection.on("CommissionNotAccept", function (artist, id) {
    var commissionLink = '<a href="/audience/commissionrequesthistorydetail?id=' + id + '">Check the commission</a>';
    toastContent.innerHTML = artist + ' denied your commission. ' + commissionLink;
    toastBootstrap.show();
});
connection.on("AddProgressImage", function (artist, id) {
    var commissionLink = '<a href="/audience/commissionrequesthistorydetail?id=' + id + '">Check the commission</a>';
    toastContent.innerHTML = artist + ' added a progress image. ' + commissionLink;
    toastBootstrap.show();
});
connection.on("DoneCommission", function (artist, id) {
    var commissionLink = '<a href="/audience/commissionrequesthistorydetail?id=' + id + '">Check the commission</a>';
    toastContent.innerHTML = artist + ' has done your commission. ' + commissionLink;
    toastBootstrap.show();
});
