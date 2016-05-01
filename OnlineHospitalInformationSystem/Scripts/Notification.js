
var notification = $("#notification").kendoNotification({
    button: true,
    position: {
        pinned: true,
        top: 10,
        right: 280
    },
    autoHideAfter: 9000,
    stacking: "down",
    templates: [{
        type: "hospitalInsertion-success",
        template: $("#successTemplate").html()
    }]

}).data("kendoNotification");
if ($("#SuccessNotificationIdBox").val() == 1) {

    notification.show({
        message: "Upload Successful"
    }, "hospitalInsertion-success");
    $("#SuccessNotificationIdBox").attr('value', 0);

};