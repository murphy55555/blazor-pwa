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
async function configureBackgroundSync(enabled) {
    const registration = await navigator.serviceWorker.ready;
    try {
        await registration.periodicSync.register("sync-car-inspections", {
            minInterval: 5 * 1000,
        });
    } catch {
        console.log("Periodic Sync could not be registered!");
    }
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
                                "BMkGVTjSaIl7trQZkVeJmy9tswIjvz4woufuCoDPR37hKTDyLYcgRyi0svlz9bORzdnOOt5HVK5n9uIkU4hee-g"
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
                            componentInstance.invokeMethodAsync('RegisterForPushNotificationsCallBackOnError', JSON.stringify(e.message));
                        });
                });
        });
    });
}

async function getGeoLocation() {
    let getGeoPromise = new Promise((resolve, reject) => {
        navigator.geolocation.getCurrentPosition(position => {
            resolve({
                Latitude: position.coords.latitude,
                Longitude: position.coords.longitude
            });
        })
    });

    return await getGeoPromise;
}

async function getBatteryInfo() {
    console.log("in here");
    let batteryStatus = new Promise((resolve, reject) => {
        navigator.getBattery().then(status => {
            console.log(status);
            resolve({
                Charging: status.charging,
                DischargingTime: isFinite(status.dischargingTime) ? status.dischargingTime : 0,
                Level: status.level
            });
        })
    });

    return await batteryStatus;
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
    mediaRecorder.ondataavailable = (e) => {
        if (e.data.size > 0) {
            chunks.push(e.data);
        }
    };
    mediaRecorder.onstop = async () => {
        const base64VideoFile = await blobToBase64(new Blob(chunks, { type: 'audio/webm' }));
        const base64Thumbnail = canvas.toDataURL("image/jpeg");
        video.pause();
        video.currentTime = 0;
        componentInstance.invokeMethodAsync('OnRecordingDataAvailable', base64Thumbnail, base64VideoFile);
    }
}

async function takePicture(videoPlayerSrcId, canvasSrcId, componentInstance) {
    let video = document.getElementById(videoPlayerSrcId);
    let canvas = document.getElementById(canvasSrcId);
    canvas.getContext('2d').drawImage(video, 0, 0, 480, 300);
    const base64Picture = canvas.toDataURL("image/jpeg");
    // Note: In the real world we'd take a much larger picture. 300x480 is tiny and just an example

    video.pause();
    video.currentTime = 0;
    componentInstance.invokeMethodAsync('OnPictureDataAvailable', base64Picture);
}

function stopRecordingVideo() {
    window.mediaRecorder.stop();
}