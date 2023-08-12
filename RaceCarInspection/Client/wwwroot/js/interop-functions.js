//async function isRegisteredForPush() {

//}

//function arrayBufferToBase64(buffer) {
//    var binary = "";
//    var bytes = new Uint8Array(buffer);
//    var len = bytes.byteLength;
//    for (var i = 0; i < len; i++) {
//        binary += String.fromCharCode(bytes[i]);
//    }
//    return window.btoa(binary);
//}

function urlBase64ToUint8Array(base64String) {
    var padding = "=".repeat((4 - (base64String.length % 4)) % 4);
    var base64 = (base64String + padding)
        .replace(/\-/g, "+")
        .replace(/_/g, "/");

    var rawData = window.atob(base64);
    var outputArray = new Uint8Array(rawData.length);

    for (var i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

function registerForPush(componentInstance) {
    let that = this;
    navigator.serviceWorker.ready.then(function (serviceWorkerRegistration) {
        Notification.requestPermission(function (status) {
            that.hasPermission = status;
            serviceWorkerRegistration.pushManager
                .getSubscription()
                .then(function (sub) {
                    serviceWorkerRegistration.pushManager
                        .subscribe({
                            userVisibleOnly: true,
                            applicationServerKey: urlBase64ToUint8Array(
                                "BNxlpTE6rbWwmZsGLdJe3PFPYHTnHOT8C414xciDmLkOSfi5stKDDB8jAjjacDT37lIG5jSJS6-Fj71iFEYTSE8"
                            )
                        })
                        .then(async function (sub) {
                            var s = JSON.parse(JSON.stringify(sub));
                            var subscriptionData = {};
                            subscriptionData.auth = s.keys.auth;
                            subscriptionData.p256dh = s.keys.p256dh;
                            subscriptionData.endpoint = s.endpoint;
                            componentInstance.invokeMethodAsync('RegisterForPushNotificationsCallBackOnSuccess', subscriptionData);
                        })
                        .catch(function (e) {
                            console.log(e);
                            componentInstance.invokeMethodAsync('RegisterForPushNotificationsCallBackOnError', JSON.stringify(e.message));
                        });
                });
        });
    });
}