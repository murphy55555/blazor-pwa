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

function startVideo(src) {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        navigator.mediaDevices.getUserMedia({ video: true, audio: true }).then(function (stream) {
            let video = document.getElementById(src);
            if ("srcObject" in video) {
                video.srcObject = stream;
            } else {
                video.src = window.URL.createObjectURL(stream);
            }
            video.onloadedmetadata = function (e) {
                video.play();
            };
        });
    }
}
function blobToBase64(blob) {
    return new Promise((resolve, _) => {
        const reader = new FileReader();
        reader.onloadend = () => resolve(reader.result);
        reader.readAsDataURL(blob);
    });
}

async function startRecordingVideo(videoPlayerSrcId, canvasSrcId, componentInstance) {
    const chunks = [];
    let video = document.getElementById(videoPlayerSrcId);
    let canvas = document.getElementById(canvasSrcId);
    canvas.getContext('2d').drawImage(video, 0, 0, 480, 300);
    window.mediaRecorder = new MediaRecorder(video.srcObject, { mimetype: 'audio/webm' });
    mediaRecorder.start();
    console.log(mediaRecorder);
    mediaRecorder.ondataavailable = (e) => {
        if (e.data.size > 0) {
            chunks.push(e.data);
        }
    };
    mediaRecorder.onstop = async () => {
        const base64VideoFile = await blobToBase64(new Blob(chunks, { type: 'audio/webm' }));
        const base64Thumbnail = canvas.toDataURL("image/jpeg");
        componentInstance.invokeMethodAsync('OnRecordingDataAvailable', base64Thumbnail, base64VideoFile);
    }
}

function stopRecordingVideo() {
    window.mediaRecorder.stop();
}